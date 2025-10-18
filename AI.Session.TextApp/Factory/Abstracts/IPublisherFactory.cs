// -------------------------------------------------------------------
// <copyright file="IPublisherFactory.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using Microsoft.Extensions.AI;

namespace AI.Session.TextApp.Factory.Abstracts;

/// <summary>
/// Defines a factory for creating instances of publishers, such as chat clients.
/// </summary>
/// <remarks>This interface provides a method to retrieve an instance of a chat client. Implementations of this
/// interface are responsible for managing the creation  and configuration of the chat client.</remarks>
internal interface IPublisherFactory
{
    /// <summary>
    /// Retrieves an instance of the chat client.
    /// </summary>
    /// <returns>An object implementing the <see cref="IChatClient"/> interface, which can be used to interact with the chat
    /// system.</returns>
    IChatClient GetChatClient();
}
