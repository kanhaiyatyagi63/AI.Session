// -------------------------------------------------------------------
// <copyright file="ChatMcpService.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using System.Text;
using System.Text.Json;

using AI.Session.TextApp.Services.Abstracts;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Client;

namespace AI.Session.TextApp.Services;

/// <summary>
/// Provides chat-based vacation recommendations by interacting with users and generating responses using an underlying
/// chat client.
/// </summary>
/// <param name="logger">The logger used to record diagnostic and operational information for the service.</param>
/// <param name="chatClient">The chat client used to generate streaming responses based on the conversation history.</param>
internal class ChatMcpService(ILogger<ChatMcpService> logger,
    IChatClient chatClient) : IChatService
{
    private readonly ILogger<ChatMcpService> _logger = logger;
    private readonly IChatClient _chatClient = chatClient;

    private JsonSerializerOptions ToolCallJsonSerializerOptions { get; set; } = new(JsonSerializerDefaults.Web);

    /// <inheritdoc/>
    public async Task ExecuteAsync(string prompt)
    {
        // Load available tools
        // 🔹 Setup the MCP client once and keep it open
        var clientTransport = new StdioClientTransport(new()
        {
            Name = "AISessionMcp",
            Command = "dotnet",
            Arguments =
            [
                "run",
            "--project",
            "C:\\Users\\kanhaya.tyagi\\Desktop\\Session\\AI.Session\\AI.Session.Mcp\\AI.Session.Mcp.csproj"
            ],
        });

        Console.WriteLine("Setting up stdio transport");

        await using var mcpClient = await McpClient.CreateAsync(clientTransport);
        var tools = await mcpClient.ListToolsAsync();

        Console.WriteLine("Available tools:");
        foreach (var tool in tools)
        {
            Console.WriteLine($" {tool.Name}: {tool.Description}");
        }

        var chatOptions = new ChatOptions()
        {
            Tools = [.. tools],
        };

        // Keep full chat history for context
        var chatHistory = new List<ChatMessage>();

        Console.WriteLine("Chat started. Press Enter on empty line to exit.");

        while (true)
        {
            Console.Write("Enter prompt: ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Exiting chat.");
                break;
            }

            chatHistory.Add(new ChatMessage(ChatRole.User, input));

            try
            {
                var assistantResponseBuilder = new StringBuilder();

                await foreach (var item in _chatClient.GetStreamingResponseAsync(chatHistory, chatOptions))
                {
                    if (string.IsNullOrEmpty(item.Text))
                    {
                        continue;
                    }

                    if (item.Role == ChatRole.Assistant)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(item.Text);
                        assistantResponseBuilder.Append(item.Text);
                        chatHistory.Add(new ChatMessage(ChatRole.Assistant, item.Text));
                    }
                    else if (item.Role == ChatRole.Tool)
                    {
                        foreach (var content in item.Contents)
                        {
                            if (content is FunctionResultContent resultContent)
                            {
                                // Skip if the assistant never requested a tool call
                                if (string.IsNullOrEmpty(resultContent.CallId))
                                {
                                    continue;
                                }

                                // Extract the tool result safely
                                string? result = resultContent.Result as string;
                                if (result is null && resultContent.Result is not null)
                                {
                                    try
                                    {
                                        result = JsonSerializer.Serialize(resultContent.Result, ToolCallJsonSerializerOptions);
                                    }
                                    catch (NotSupportedException)
                                    {
                                    }
                                }

                                // ✅ Build a JSON structure manually that matches OpenAI’s expected format
                                var toolResultPayload = JsonSerializer.Serialize(new
                                {
                                    tool_call_id = resultContent.CallId,
                                    result,
                                }, ToolCallJsonSerializerOptions);

                                // ✅ Add tool result as a normal ChatMessage with ChatRole.Tool
                                chatHistory.Add(new ChatMessage(ChatRole.Tool, toolResultPayload));
                            }
                        }
                    }
                    else
                    {
                        chatHistory.Add(new ChatMessage(ChatRole.Assistant, item.Text));
                    }

                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[Error] {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
