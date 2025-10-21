// -------------------------------------------------------------------
// <copyright file="IPlatformFactory.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using Microsoft.Extensions.AI;

namespace AI.Session.TextApp.Factory.Abstracts;

/// <summary>
/// Defines a factory for creating platform-specific clients and services.
/// </summary>
/// <remarks>This interface provides methods to retrieve instances of platform-specific components, such as chat
/// clients and embedding generator clients. Implementations of this interface are responsible for ensuring that the
/// returned instances are properly configured and ready for use.</remarks>
internal interface IPlatformFactory
{
    /// <summary>
    /// Retrieves an instance of the chat client.
    /// </summary>
    /// <returns>An object implementing the <see cref="IChatClient"/> interface, which can be used to interact with the chat
    /// system.</returns>
    IChatClient GetChatClient();

    /// <summary>
    /// Retrieves an instance of an embedding generator client.
    /// </summary>
    /// <remarks>The embedding generator client is used to generate embeddings for string inputs,  producing
    /// embeddings represented as <see cref="Embedding{T}"/> objects with floating-point values.</remarks>
    /// <returns>An <see cref="IEmbeddingGenerator{TInput, TEmbedding}"/> instance configured to generate embeddings  for string
    /// inputs.</returns>
    IEmbeddingGenerator<string, Embedding<float>> GetEmbeddingGenerator();
}
