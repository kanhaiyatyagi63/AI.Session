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
            "C:\\Users\\kanhaya.tyagi\\Desktop\\Session\\AI.Session\\AI.Session.Mcp"
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
                var updates = new List<ChatResponseUpdate>();

                await foreach (var item in _chatClient.GetStreamingResponseAsync(chatHistory, chatOptions))
                {
                    updates.Add(item);

                    // Display streaming text
                    if (!string.IsNullOrEmpty(item.Text))
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(item.Text);
                        assistantResponseBuilder.Append(item.Text);
                    }

                    Console.ResetColor();
                }

                // Collect all function calls from the streaming response
                var functionCalls = updates
                    .SelectMany(u => u.Contents)
                    .OfType<FunctionCallContent>()
                    .ToList();

                if (functionCalls.Count != 0)
                {
                    // Build assistant message with proper structure for OpenAI
                    var assistantContents = new List<AIContent>();
                    
                    // IMPORTANT: Include text content first (if any)
                    if (assistantResponseBuilder.Length > 0)
                    {
                        assistantContents.Add(new TextContent(assistantResponseBuilder.ToString()));
                    }
                    
                    // Then add all function calls
                    assistantContents.AddRange(functionCalls);
                    
                    // Add complete assistant message with both text and tool_calls
                    chatHistory.Add(new ChatMessage(ChatRole.Assistant, assistantContents));

                    // Execute each MCP tool and add results
                    foreach (var functionCall in functionCalls)
                    {
                        var args = functionCall.Arguments as IReadOnlyDictionary<string, object?> 
                            ?? functionCall.Arguments?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value) as IReadOnlyDictionary<string, object?>
                            ?? new Dictionary<string, object?>();

                        var result = await mcpClient.CallToolAsync(functionCall.Name, args);

                        // Add tool result message
                        chatHistory.Add(new ChatMessage(
                            ChatRole.Tool,
                            [new FunctionResultContent(functionCall.CallId, result)]));
                    }

                    // Get AI's final response after tool execution
                    Console.WriteLine();
                    assistantResponseBuilder.Clear();
                    
                    await foreach (var item in _chatClient.GetStreamingResponseAsync(chatHistory, chatOptions))
                    {
                        if (!string.IsNullOrEmpty(item.Text))
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(item.Text);
                            assistantResponseBuilder.Append(item.Text);
                        }
                    }

                    if (assistantResponseBuilder.Length > 0)
                    {
                        chatHistory.Add(new ChatMessage(ChatRole.Assistant, assistantResponseBuilder.ToString()));
                    }
                }
                else if (assistantResponseBuilder.Length > 0)
                {
                    // No tool calls - just add the text response
                    chatHistory.Add(new ChatMessage(ChatRole.Assistant, assistantResponseBuilder.ToString()));
                }

                Console.WriteLine();
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
