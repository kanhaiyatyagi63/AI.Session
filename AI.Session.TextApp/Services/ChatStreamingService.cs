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
/// platform factory.
/// </summary>
/// <remarks>This service logs the input prompt, retrieves a response from the chat client, and logs the response
/// along with token usage statistics. It ensures that the provided prompt is not null or empty before
/// processing.</remarks>
internal class ChatStreamingService(ILogger<ChatStreamingService> logger,
    IChatClient chatClient) : IChatService
{
    private readonly ILogger<ChatStreamingService> _logger = logger;
    private readonly IChatClient _chatClient = chatClient;

    /// <inheritdoc/>
    public async Task ExecuteAsync(string prompt)
    {
        if (string.IsNullOrEmpty(prompt))
        {
            throw new ArgumentException("Prompt cannot be null or empty.", nameof(prompt));
        }

        _logger.LogInformation("User ->>> {Prompt}", prompt);

        var chatResponse = _chatClient.GetStreamingResponseAsync(prompt);

        _logger.LogInformation("Chat Response ->>>");
        UsageDetails? usageDetails = null;

        await foreach (var chatResponseUpdate in chatResponse)
        {
            Console.Write(chatResponseUpdate);
            
            var usage = chatResponseUpdate.Contents.OfType<UsageContent>().FirstOrDefault()?.Details;
            if (usage != null)
            {
                usageDetails = usage;
            }
        }

        ShowUsageDetails(usageDetails);
    }

    private static void ShowUsageDetails(UsageDetails? usage)
    {
        if (usage != null)
        {
            Console.WriteLine($"   InputTokenCount: {usage.InputTokenCount}");
            Console.WriteLine($"   OutputTokenCount: {usage.OutputTokenCount}");
            Console.WriteLine($"   TotalTokenCount: {usage.TotalTokenCount}");

            if (usage.AdditionalCounts != null)
            {
                foreach (var additionalCount in usage.AdditionalCounts)
                {
                    Console.WriteLine($"  {additionalCount.Key}: {additionalCount.Value}");
                }
            }
        }
    }
}