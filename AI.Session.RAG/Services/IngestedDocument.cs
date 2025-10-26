// -------------------------------------------------------------------
// <copyright file="IngestedDocument.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using Microsoft.Extensions.VectorData;

namespace AI.Session.RAG.Services;

/// <summary>
/// Represents a document that has been ingested into the vector store, including metadata and a placeholder vector.
/// </summary>
public class IngestedDocument
{
    private const int VectorDimensions = 2;
    private const string VectorDistanceFunction = DistanceFunction.CosineDistance;

    /// <summary>
    /// Gets or sets the unique key for the ingested document.
    /// </summary>
    [VectorStoreKey]
    required public string Key { get; set; }

    /// <summary>
    /// Gets or sets the source identifier. This property is indexed.
    /// </summary>
    [VectorStoreData(IsIndexed = true)]
    required public string SourceId { get; set; }

    /// <summary>
    /// Gets or sets the document identifier.
    /// </summary>
    [VectorStoreData]
    required public string DocumentId { get; set; }

    /// <summary>
    /// Gets or sets the document version.
    /// </summary>
    [VectorStoreData]
    required public string DocumentVersion { get; set; }

    /// <summary>
    /// Gets or sets the vector representation of the document.
    /// The vector is not used but required for some vector databases.
    /// </summary>
    [VectorStoreVector(VectorDimensions, DistanceFunction = VectorDistanceFunction)]
    public ReadOnlyMemory<float> Vector { get; set; } = new ReadOnlyMemory<float>([0, 0]);
}
