// -------------------------------------------------------------------
// <copyright file="ITextService.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

namespace AI.Session.TextApp.Services.Abstracts;

/// <summary>
/// Defines a service for processing text-based prompts asynchronously.
/// </summary>
/// <remarks>This interface provides a contract for implementing text processing services that handle
/// user-provided prompts. Implementations are expected to process the input prompt, interact with a chat client, and
/// log the results. Ensure that all dependencies required by the implementation are properly configured before invoking
/// its methods.</remarks>
internal interface ITextService
{
    /// <summary>
    /// Executes an asynchronous operation to process a user-provided prompt and log the response.
    /// </summary>
    /// <remarks>This method logs the user prompt, sends it to a chat client for processing, and logs the
    /// response along with token usage statistics. Ensure that the logger and chat client dependencies are properly
    /// configured before invoking this method.</remarks>
    /// <param name="prompt">The user-provided input string to be processed. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="prompt"/> is null or empty.</exception>
    Task ExecuteAsync(string prompt);
}
