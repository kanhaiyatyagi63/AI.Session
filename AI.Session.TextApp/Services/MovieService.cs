// -------------------------------------------------------------------
// <copyright file="MovieService.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using AI.Session.TextApp.Factory.Abstracts;
using AI.Session.TextApp.Models;
using AI.Session.TextApp.Services.Abstracts;
using Microsoft.Extensions.AI;

namespace AI.Session.TextApp.Services;

/// <summary>
/// Provides functionality for managing and processing movie-related data.
/// </summary>
/// <remarks>This service is designed to handle operations related to movies, such as retrieving, updating, or
/// processing movie information.  It implements the <see cref="IMovieService"/> interface, indicating that it may also
/// provide text-related services.</remarks>
internal class MovieService(IPlatformFactory platformFactory, VectorStore vectorStore) : IMovieService
{
    private readonly VectorStore _vectorStore = vectorStore;
    private readonly IEmbeddingGenerator<string, Embedding<float>> _embeddingGenerator = platformFactory.GetEmbeddingGenerator();

    /// <inheritdoc/>
    public async Task ExecuteAsync()
    {
        await _vectorStore.InitializeStoreAsync();

        foreach (var movie in MovieModel.Seed())
        {
            movie.Vector = await _embeddingGenerator.GenerateVectorAsync(movie.Description);
            await _vectorStore.MovieCollection.UpsertAsync(movie);
        }

        var query = "Give me any sci-fi movie";

        var queryVector = await _embeddingGenerator.GenerateVectorAsync(query);

        var results = _vectorStore.MovieCollection.SearchAsync(queryVector, 3);

        await foreach (var result in results)
        {
            Console.WriteLine($"Movie: {result.Record.Title}, " +
                $"Description: {result.Record.Description}, " +
                $"Score: {result.Score}");
        }
    }
}
