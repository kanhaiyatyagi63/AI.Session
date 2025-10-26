// -------------------------------------------------------------------
// <copyright file="PDFDirectorySource.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using Microsoft.SemanticKernel.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;

namespace AI.Session.RAG.Services.Ingestion;

/// <summary>
/// Provides an ingestion source that scans a directory for PDF files and exposes them as documents for processing.
/// </summary>
/// <remarks>PDFDirectorySource identifies and tracks PDF files in the specified directory, enabling detection of
/// new, modified, or deleted documents. Each PDF file is treated as a separate document, and changes are detected based
/// on file modification timestamps. This class is not thread-safe; concurrent access to the same instance should be
/// avoided.</remarks>
/// <param name="sourceDirectory">The full path to the directory containing PDF files to be ingested. Must not be null or empty.</param>
public class PDFDirectorySource(string sourceDirectory) : IIngestionSource
{
    /// <inheritdoc/>
    public string SourceId => $"{nameof(PDFDirectorySource)}:{sourceDirectory}";

    /// <summary>
    /// Extracts the file name and extension from the specified file path.
    /// </summary>
    /// <param name="path">The full path of the file. Can be either a relative or absolute path.</param>
    /// <returns>A string containing the file name and extension from the specified path, or the original path if it does not
    /// contain directory information.</returns>
    public static string SourceFileId(string path) => Path.GetFileName(path);

   /// <summary>
   /// Gets the last write time of the specified file, formatted as an ISO 8601 string in UTC.
   /// </summary>
   /// <param name="path">The full path to the file for which to retrieve the last write time. Cannot be null or an empty string.</param>
   /// <returns>A string containing the UTC last write time of the file, formatted in ISO 8601 ("o") format.</returns>
    public static string SourceFileVersion(string path) => File.GetLastWriteTimeUtc(path).ToString("o");

    /// <inheritdoc/>
    public Task<IEnumerable<IngestedDocument>> GetNewOrModifiedDocumentsAsync(IReadOnlyList<IngestedDocument> existingDocuments)
    {
        var results = new List<IngestedDocument>();
        var sourceFiles = Directory.GetFiles(sourceDirectory, "*.pdf");
        var existingDocumentsById = existingDocuments.ToDictionary(d => d.DocumentId);

        foreach (var sourceFile in sourceFiles)
        {
            var sourceFileId = SourceFileId(sourceFile);
            var sourceFileVersion = SourceFileVersion(sourceFile);
            var existingDocumentVersion = existingDocumentsById.TryGetValue(sourceFileId, out var existingDocument) ? existingDocument.DocumentVersion : null;
            if (existingDocumentVersion != sourceFileVersion)
            {
                results.Add(new() { Key = Guid.CreateVersion7().ToString(), SourceId = SourceId, DocumentId = sourceFileId, DocumentVersion = sourceFileVersion });
            }
        }

        return Task.FromResult((IEnumerable<IngestedDocument>)results);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<IngestedDocument>> GetDeletedDocumentsAsync(IReadOnlyList<IngestedDocument> existingDocuments)
    {
        var currentFiles = Directory.GetFiles(sourceDirectory, "*.pdf");
        var currentFileIds = currentFiles.ToLookup(SourceFileId);
        var deletedDocuments = existingDocuments.Where(d => !currentFileIds.Contains(d.DocumentId));
        return Task.FromResult(deletedDocuments);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<IngestedChunk>> CreateChunksForDocumentAsync(IngestedDocument document)
    {
        using var pdf = PdfDocument.Open(Path.Combine(sourceDirectory, document.DocumentId));
        var paragraphs = pdf.GetPages().SelectMany(GetPageParagraphs).ToList();

        return Task.FromResult(paragraphs.Select(p => new IngestedChunk
        {
            Key = Guid.CreateVersion7().ToString(),
            DocumentId = document.DocumentId,
            PageNumber = p.PageNumber,
            Text = p.Text,
        }));
    }

    private static IEnumerable<(int PageNumber, int IndexOnPage, string Text)> GetPageParagraphs(Page pdfPage)
    {
        var letters = pdfPage.Letters;
        var words = NearestNeighbourWordExtractor.Instance.GetWords(letters);
        var textBlocks = DocstrumBoundingBoxes.Instance.GetBlocks(words);
        var pageText = string.Join(Environment.NewLine + Environment.NewLine,
            textBlocks.Select(t => t.Text.ReplaceLineEndings(" ")));

#pragma warning disable SKEXP0050 // Type is for evaluation purposes only
        return TextChunker.SplitPlainTextParagraphs([pageText], 200)
            .Select((text, index) => (pdfPage.Number, index, text));
#pragma warning restore SKEXP0050 // Type is for evaluation purposes only
    }
}
