// -------------------------------------------------------------------
// <copyright file="ChatStreamingService.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using AI.Session.TextApp.Factory.Abstracts;
using AI.Session.TextApp.Services.Abstracts;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace AI.Session.TextApp.Services;

/// <summary>
/// Provides functionality for processing chat prompts and streaming responses using a chat client obtained from a
/// publisher factory.
/// </summary>
/// <remarks>This service logs the input prompt, retrieves a response from the chat client, and logs the response
/// along with token usage statistics. It ensures that the provided prompt is not null or empty before
/// processing.</remarks>
internal class ChatStreamingService(ILogger<ChatStreamingService> logger,
    IPublisherFactory publisherFactory) : ITextService
{
    private readonly ILogger<ChatStreamingService> _logger = logger;
    private readonly IPublisherFactory _publisherFactory = publisherFactory;

    /// <inheritdoc/>
    public async Task ExecuteAsync(string prompt)
    {
        if (string.IsNullOrEmpty(prompt))
        {
            throw new ArgumentException("Prompt cannot be null or empty.", nameof(prompt));
        }

        _logger.LogInformation("User ->>> {Prompt}", prompt);

        var chatClient = _publisherFactory.GetChatClient();

        var chatResponse = chatClient.GetStreamingResponseAsync(prompt);

        _logger.LogInformation("Chat Response ->>>");

        await foreach (var chatResponseUpdate in chatResponse)
        {
            Console.Write(chatResponseUpdate);
        }
    }
}