// -------------------------------------------------------------------
// <copyright file="ChatCompletionService.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using AI.Session.TextApp.Factory.Abstracts;
using AI.Session.TextApp.Services.Abstracts;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace AI.Session.TextApp.Services;

/// <summary>
/// Provides functionality for handling chat-based interactions, including logging user prompts and processing responses
/// from a chat client.
/// </summary>
/// <remarks>This service is designed to facilitate chat-based workflows by integrating with a chat client
/// obtained from an <see cref="IPlatformFactory"/>. It logs user prompts, retrieves responses from the chat client,
/// and logs relevant details such as token usage.</remarks>
internal class ChatCompletionService(ILogger<ChatCompletionService> logger,
    IPlatformFactory platformFactory) : IChatService
{
    private readonly ILogger<ChatCompletionService> _logger = logger;
    private readonly IPlatformFactory _platformFactory = platformFactory;

    /// <inheritdoc/>
    public async Task ExecuteAsync(string prompt)
    {
        if (string.IsNullOrEmpty(prompt))
        {
            throw new ArgumentException("Prompt cannot be null or empty.", nameof(prompt));
        }

        _logger.LogInformation("User ->>> {Prompt}", prompt);

        var chatClient = _platformFactory.GetChatClient();

        var chatResponse = await chatClient.GetResponseAsync(prompt);

        _logger.LogInformation("Chat Response ->>> {ChatResponse}", chatResponse);

        _logger.LogInformation("Tokens Used ->>> Input: {InputTokenCount}, Output: {OutputTokenCount}",
            chatResponse.Usage?.InputTokenCount ?? 0,
            chatResponse.Usage?.OutputTokenCount ?? 0);
    }
}
