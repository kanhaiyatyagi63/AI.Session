// -------------------------------------------------------------------
// <copyright file="DataIngestor.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;

namespace AI.Session.RAG.Services.Ingestion;

/// <summary>
/// Handles the ingestion of documents and their associated chunks from a specified <see cref="IIngestionSource"/>.
/// Ensures that the vector store collections for documents and chunks are up-to-date by processing new, modified, and deleted documents.
/// </summary>
public class DataIngestor(
    ILogger<DataIngestor> logger,
    VectorStoreCollection<string, IngestedChunk> chunksCollection,
    VectorStoreCollection<string, IngestedDocument> documentsCollection)
{
    /// <summary>
    /// Creates a new scope, resolves a <see cref="DataIngestor"/>, and performs ingestion from the specified source.
    /// </summary>
    /// <param name="services">The service provider for dependency resolution.</param>
    /// <param name="source">The ingestion source to process.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task IngestDataAsync(IServiceProvider services, IIngestionSource source)
    {
        using var scope = services.CreateScope();
        var ingestor = scope.ServiceProvider.GetRequiredService<DataIngestor>();
        await ingestor.IngestDataAsync(source);
    }

    /// <summary>
    /// Ingests data from the specified <see cref="IIngestionSource"/>, updating the vector store collections for documents and chunks.
    /// Handles new, modified, and deleted documents accordingly.
    /// </summary>
    /// <param name="source">The ingestion source to process.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task IngestDataAsync(IIngestionSource source)
    {
        await chunksCollection.EnsureCollectionExistsAsync();
        await documentsCollection.EnsureCollectionExistsAsync();

        var sourceId = source.SourceId;
        var documentsForSource = await documentsCollection.GetAsync(doc => doc.SourceId == sourceId, top: int.MaxValue).ToListAsync();

        var deletedDocuments = await source.GetDeletedDocumentsAsync(documentsForSource);
        foreach (var deletedDocument in deletedDocuments)
        {
            logger.LogInformation("Removing ingested data for {DocumentId}", deletedDocument.DocumentId);
            await DeleteChunksForDocumentAsync(deletedDocument);
            await documentsCollection.DeleteAsync(deletedDocument.Key);
        }

        var modifiedDocuments = await source.GetNewOrModifiedDocumentsAsync(documentsForSource);
        foreach (var modifiedDocument in modifiedDocuments)
        {
            logger.LogInformation("Processing {DocumentId}", modifiedDocument.DocumentId);
            await DeleteChunksForDocumentAsync(modifiedDocument);

            await documentsCollection.UpsertAsync(modifiedDocument);

            var newRecords = await source.CreateChunksForDocumentAsync(modifiedDocument);
            await chunksCollection.UpsertAsync(newRecords);
        }

        logger.LogInformation("Ingestion is up-to-date");

        async Task DeleteChunksForDocumentAsync(IngestedDocument document)
        {
            var documentId = document.DocumentId;
            var chunksToDelete = await chunksCollection.GetAsync(record => record.DocumentId == documentId, int.MaxValue).ToListAsync();
            if (chunksToDelete.Count != 0)
            {
                await chunksCollection.DeleteAsync(chunksToDelete.Select(r => r.Key));
            }
        }
    }
}
