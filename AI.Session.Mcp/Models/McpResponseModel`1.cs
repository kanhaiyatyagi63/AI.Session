// -------------------------------------------------------------------
// <copyright file="McpResponseModel`1.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace AI.Session.Mcp.Models;

/// <summary>
/// Represents a response from the MCP server.
/// The generic type parameter <typeparamref name="T"/> specifies the type of the tool call result payload.
/// </summary>
/// <typeparam name="T">The type of the tool call result payload.</typeparam>
public class McpResponseModel<T>(T result, string toolCallId, string role = "tool")
    where T : class
{
    /// <summary>
    /// Gets or sets the role of the response.
    /// </summary>
    [JsonPropertyName("role")]
    public string Role { get; set; } = role;

    /// <summary>
    /// Gets or sets the tool call ID.
    /// </summary>
    [JsonPropertyName("tool_call_id")]
    public string ToolCallId { get; set; } = toolCallId;
    
    /// <summary>
    /// Gets or sets the result of the tool call.
    /// </summary>
    [JsonPropertyName("result")]
    public T Result { get; set; } = result;
}