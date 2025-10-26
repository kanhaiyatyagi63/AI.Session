// -------------------------------------------------------------------
// <copyright file="IngestedChunk.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using Microsoft.Extensions.VectorData;

namespace AI.Session.RAG.Services;

/// <summary>
/// Represents a chunk of ingested text data with associated metadata and vector embedding information
/// for use with a vector store in a retrieval-augmented generation (RAG) scenario.
/// </summary>
public class IngestedChunk
{
    private const int VectorDimensions = 384; // 384 is the default vector size for the all-minilm embedding model
    private const string VectorDistanceFunction = DistanceFunction.CosineDistance;

    /// <summary>
    /// Gets or sets the unique key for the chunk in the vector store.
    /// </summary>
    [VectorStoreKey]
    required public string Key { get; set; }

    /// <summary>
    /// Gets or sets the document identifier this chunk belongs to.
    /// </summary>
    [VectorStoreData(IsIndexed = true)]
    required public string DocumentId { get; set; }

    /// <summary>
    /// Gets or sets the page number within the document for this chunk.
    /// </summary>
    [VectorStoreData]
    public int PageNumber { get; set; }

    /// <summary>
    /// Gets or sets the text content of the chunk.
    /// </summary>
    [VectorStoreData]
    required public string Text { get; set; }

    /// <summary>
    /// Gets the vector representation of the chunk's text for similarity search.
    /// </summary>
    [VectorStoreVector(VectorDimensions, DistanceFunction = VectorDistanceFunction)]
    public string? Vector => Text;
}
