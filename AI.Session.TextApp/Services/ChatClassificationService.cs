// -------------------------------------------------------------------
// <copyright file="ChatClassificationService.cs" company="Kanhaya Tyagi">
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
/// <remarks>This service logs the input prompt and the streaming response updates for diagnostic purposes. It
/// requires a valid prompt to execute.</remarks>
internal class ChatClassificationService(ILogger<ChatClassificationService> logger,
    IPublisherFactory publisherFactory) : ITextService
{
    private readonly ILogger<ChatClassificationService> _logger = logger;
    private readonly IPublisherFactory _publisherFactory = publisherFactory;

    /// <inheritdoc/>
    public async Task ExecuteAsync(string prompt)
    {
        if (string.IsNullOrEmpty(prompt))
        {
            // add default classification if prompt is empty
            prompt = "Classify the following sentences into categories: Technology, Health, Finance, Education, Entertainment.\n" +
                     "1. The stock market saw a significant increase today.\n" +
                     "2. New advancements in AI are transforming industries.\n" +
                     "3. Regular exercise is essential for maintaining good health.\n" +
                     "4. The latest movie received rave reviews from critics.\n" +
                     "5. Online learning platforms are becoming increasingly popular.";
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
