// -------------------------------------------------------------------
// <copyright file="ChatFunctionService.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using System.ComponentModel;

using AI.Session.TextApp.Services.Abstracts;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace AI.Session.TextApp.Services;

/// <summary>
/// Provides chat-related functionality for executing prompts asynchronously.
/// </summary>
internal class ChatFunctionService(ILogger<ChatFunctionService> logger, IChatClient chatClient) : IChatService
{
    private readonly IChatClient _chatClient = chatClient;
    private readonly ILogger<ChatFunctionService> _logger = logger;

    /// <inheritdoc/>
    public async Task ExecuteAsync(string prompt)
    {
        if (string.IsNullOrEmpty(prompt))
        {
            throw new ArgumentException("Prompt cannot be null or empty.", nameof(prompt));
        }

        _logger.LogInformation("User ->>> {Prompt}", prompt);

        var chatOptions = new ChatOptions()
        {
            Tools = [AIFunctionFactory.Create(GetTemperature), AIFunctionFactory.Create(GetSerialNumber)],
            ToolMode = ChatToolMode.Auto,
        };

        List<ChatMessage> chatHistory = [];

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

            await foreach (var item in _chatClient.GetStreamingResponseAsync(chatHistory, chatOptions))
            {
                Console.Write(item.Text);
                response += item.Text;
            }

            chatHistory.Add(new ChatMessage(ChatRole.Assistant, response));
            Console.WriteLine();
        }
    }

    [Description("Get the current temperature in Fahrenheit of weather")]
    private async Task<float?> GetTemperature(
        [Description("The temperature of location.")] string location)
    {
        await Task.Delay(500);

        return new Random().Next(-20, 50);
    }

    [Description("Get the mobile serial number from model name")]
    private async Task<float?> GetSerialNumber(
        [Description("Model name of the mobile.")] string model)
    {
        await Task.Delay(500);

        return new Random().Next(20000, 50000);
    }
}
