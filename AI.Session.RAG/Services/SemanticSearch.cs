// -------------------------------------------------------------------
// <copyright file="SemanticSearch.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using Microsoft.Extensions.VectorData;

namespace AI.Session.RAG.Services;

/// <summary>
/// Provides semantic search capabilities over a vector store collection of <see cref="IngestedChunk"/> items.
/// </summary>
public class SemanticSearch(
    VectorStoreCollection<string, IngestedChunk> vectorCollection)
{
    /// <summary>
    /// Performs a semantic search for the specified text, optionally filtering by document ID, and returns the top matching chunks.
    /// </summary>
    /// <param name="text">The search query text.</param>
    /// <param name="documentIdFilter">An optional document ID to filter results by.</param>
    /// <param name="maxResults">The maximum number of results to return.</param>
    /// <returns>A list of the top matching <see cref="IngestedChunk"/> items.</returns>
    public async Task<IReadOnlyList<IngestedChunk>> SearchAsync(string text, string? documentIdFilter, int maxResults)
    {
        var nearest = vectorCollection.SearchAsync(text, maxResults, new VectorSearchOptions<IngestedChunk>
        {
            Filter = documentIdFilter is { Length: > 0 } ? record => record.DocumentId == documentIdFilter : null,
        });

        return await nearest.Select(result => result.Record).ToListAsync();
    }
}
