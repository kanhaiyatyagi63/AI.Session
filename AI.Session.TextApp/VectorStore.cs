// -------------------------------------------------------------------
// <copyright file="VectorStore.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using AI.Session.TextApp.Models;
using Microsoft.SemanticKernel.Connectors.InMemory;

namespace AI.Session.TextApp;

/// <summary>
/// Provides functionality for managing and retrieving vector data in memory.
/// </summary>
/// <remarks>The <see cref="VectorStore"/> class is designed for scenarios where vector data needs to be stored
/// and queried in memory. It is suitable for use cases where persistence is not required, and high  performance is a
/// priority. This class encapsulates an instance of <see cref="InMemoryVectorStore"/>  to handle the underlying storage
/// and retrieval operations.</remarks>
internal class VectorStore
{
    /// <summary>
    /// Represents an in-memory store for managing and retrieving vector data.
    /// </summary>
    /// <remarks>This field provides an instance of <see cref="InMemoryVectorStore"/> that can be used to
    /// store and  query vector data in memory. It is suitable for scenarios where persistence is not required and 
    /// performance is a priority.</remarks>
    private readonly InMemoryVectorStore _store = new();

    /// <summary>
    /// Gets the collection of movies stored in memory, keyed by their unique identifiers.
    /// </summary>
    /// <remarks>The collection is retrieved from the underlying data store and provides in-memory access to
    /// movie data.</remarks>
    public InMemoryCollection<int, MovieModel> MovieCollection =>
        _store.GetCollection<int, MovieModel>("movies");

    /// <summary>
    /// Ensures that the movie collection is created and available in the in-memory vector store.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task InitializeStoreAsync()
    {
        await MovieCollection.EnsureCollectionExistsAsync();
    }
}
