// -------------------------------------------------------------------
// <copyright file="ChatSummarizationService.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using AI.Session.TextApp.Factory.Abstracts;
using AI.Session.TextApp.Services.Abstracts;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace AI.Session.TextApp.Services;

/// <summary>
/// Provides functionality to summarize text prompts using a chat-based completion service.
/// </summary>
/// <remarks>This service processes a given text prompt and generates a summarized response using a chat client.
/// If the provided prompt is null or empty, a default summarization prompt is used.</remarks>
internal class ChatSummarizationService(ILogger<ChatSummarizationService> logger,
    IPublisherFactory publisherFactory) : ITextService
{
    private readonly ILogger<ChatSummarizationService> _logger = logger;
    private readonly IPublisherFactory _publisherFactory = publisherFactory;

    /// <inheritdoc/>
    public async Task ExecuteAsync(string prompt)
    {
        if (string.IsNullOrEmpty(prompt))
        {
            // add default classification if prompt is empty
            prompt = "Summarize the following blog into one sentence.\n" +
                     "Microservices architecture is a way of designing software applications as a collection of small, independent services that work together.\r\n\r" +
                     "\nInstead of building one large monolithic application that handles everything, developers break the system into separate pieces." +
                     " Each piece, called a microservice, focuses on a specific function such as payments, inventory, or user management. These services" +
                     " communicate with each other over a network and can be developed, tested, deployed, and scaled independently.\r\n\r\nThis approach" +
                     " is very different from the traditional monolithic architecture, where all features are bundled into a single application.\r\n\r\n" +
                     "While monoliths can be simpler to start with, they become harder to maintain as they grow. Even a small change in one part may require " +
                     "rebuilding and redeploying the entire system. Microservices solve this by allowing teams to work on different services without affecting the " +
                     "rest of the system. They improve scalability, agility, and fault tolerance, which makes them well-suited for modern, large-scale applications.\r\n\r\n" +
                     "At the same time, microservices also introduce complexity. Managing many small services requires strong design patterns, supporting infrastructure, " +
                     "and clear best practices.\r\n\r\nIn this article, we will explore these patterns, the main components of production-ready microservices, and the practices " +
                     "that help organizations succeed with this architecture.\r\n\r\n.";
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