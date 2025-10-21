// -------------------------------------------------------------------
// <copyright file="IMovieService.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

namespace AI.Session.TextApp.Services.Abstracts;

/// <summary>
/// Provides an abstraction for movie-related operations.
/// </summary>
internal interface IMovieService
{
    /// <summary>
    /// Executes the main movie service logic asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ExecuteAsync();
}
