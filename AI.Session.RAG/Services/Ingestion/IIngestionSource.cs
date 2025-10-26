// -------------------------------------------------------------------
// <copyright file="IIngestionSource.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

namespace AI.Session.RAG.Services.Ingestion;

/// <summary>
/// Defines a contract for an ingestion source that provides documents and their chunks for processing.
/// </summary>
public interface IIngestionSource
{
    /// <summary>
    /// Gets the unique identifier for the ingestion source.
    /// </summary>
    string SourceId { get; }

    /// <summary>
    /// Retrieves documents that are new or have been modified since the last ingestion.
    /// </summary>
    /// <param name="existingDocuments">A read-only list of documents that have already been ingested.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of new or modified documents.</returns>
    Task<IEnumerable<IngestedDocument>> GetNewOrModifiedDocumentsAsync(IReadOnlyList<IngestedDocument> existingDocuments);

    /// <summary>
    /// Retrieves documents that have been deleted since the last ingestion.
    /// </summary>
    /// <param name="existingDocuments">A read-only list of documents that have already been ingested.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of deleted documents.</returns>
    Task<IEnumerable<IngestedDocument>> GetDeletedDocumentsAsync(IReadOnlyList<IngestedDocument> existingDocuments);

    /// <summary>
    /// Creates chunks for a specified document.
    /// </summary>
    /// <param name="document">The document to chunk.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the collection of chunks for the document.</returns>
    Task<IEnumerable<IngestedChunk>> CreateChunksForDocumentAsync(IngestedDocument document);
}
