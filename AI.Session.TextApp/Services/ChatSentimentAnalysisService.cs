// -------------------------------------------------------------------
// <copyright file="ChatSentimentAnalysisService.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using AI.Session.TextApp.Factory.Abstracts;
using AI.Session.TextApp.Services.Abstracts;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace AI.Session.TextApp.Services;

/// <summary>
/// Provides functionality for analyzing chat sentiment by processing user prompts and streaming responses from a chat
/// client.
/// </summary>
/// <remarks>This service is designed to handle user prompts, log the input and output, and stream responses from
/// a chat client obtained via the provided <see cref="IPublisherFactory"/>.</remarks>
internal class ChatSentimentAnalysisService(ILogger<ChatSentimentAnalysisService> logger,
    IPublisherFactory publisherFactory) : ITextService
{
    private readonly ILogger<ChatSentimentAnalysisService> _logger = logger;
    private readonly IPublisherFactory _publisherFactory = publisherFactory;

    /// <inheritdoc/>
    public async Task ExecuteAsync(string prompt)
    {
        if (string.IsNullOrEmpty(prompt))
        {
            // add default sentiment analysis if prompt is empty
            prompt = "Analyze the sentiment of the following sentences and classify them as Positive, Negative, or Neutral.\n" +
                     "1. I absolutely love the new design of your website!\n" +
                     "2. The customer service was terrible and unhelpful.\n" +
                     "3. I'm feeling quite indifferent about the recent changes.\n" +
                     "4. The movie was fantastic, I enjoyed every moment of it.\n" +
                     "5. I'm really disappointed with the quality of the product I received.";
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
