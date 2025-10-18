// -------------------------------------------------------------------
// <copyright file="ChatService.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using AI.Session.TextApp.Factory.Abstracts;
using AI.Session.TextApp.Services.Abstracts;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace AI.Session.TextApp.Services;

/// <summary>
/// Provides functionality for executing chat-based operations using a prompt.
/// </summary>
/// <remarks>This service logs the input prompt, sends it to a chat client for processing, and logs the response
/// along with token usage statistics. It relies on an <see cref="IPublisherFactory"/> to obtain the chat client and an
/// <see cref="ILogger{TCategoryName}"/> for logging.</remarks>
internal class ChatService(ILogger<ChatService> logger,
    IPublisherFactory publisherFactory) : ITextService
{
    private readonly ILogger<ChatService> _logger = logger;
    private readonly IPublisherFactory _publisherFactory = publisherFactory;

    /// <inheritdoc/>
    public async Task ExecuteAsync(string prompt)
    {
        List<ChatMessage> chatHistory =
        [
            new ChatMessage(
                ChatRole.System,
                "You are a friendly vacation finder who helps people discover the best destinations for their holidays. " +
                "When assisting users, you should always ask for the following information before providing recommendations:\n\n" +
                "1. The location or region they want to travel to.\n" +
                "2. The budget they have in mind (e.g., low, medium, high).\n" +
                "3. The duration of their trip (e.g., weekend, one week, two weeks).\n\n" +
                "Once you have this information, provide a detailed vacation recommendation that includes destinations, " +
                "reasons for visiting, ideal travel times, and estimated costs."
            ),
        ];
        var chatClient = _publisherFactory.GetChatClient();

        while (true)
        {
            Console.WriteLine("Enter prompt:");
            var input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Exiting chat.");
                break;
            }

            chatHistory.Add(new ChatMessage(ChatRole.User, input));
            var response = "";

            await foreach (var item in chatClient.GetStreamingResponseAsync(chatHistory))
            {
                Console.Write(item.Text);
                response += item.Text;
            }

            chatHistory.Add(new ChatMessage(ChatRole.Assistant, response));
            Console.WriteLine();
        }
    }
}