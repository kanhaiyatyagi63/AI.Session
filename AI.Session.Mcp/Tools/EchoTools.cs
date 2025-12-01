// -------------------------------------------------------------------
// <copyright file="EchoTools.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using System.ComponentModel;

using ModelContextProtocol.Server;

namespace AI.Session.Mcp.Tools;

/// <summary>
/// Echo tools for responding to ping requests.
/// </summary>
[McpServerToolType]
public class EchoTools
{
    /// <summary>
    /// Responds to a ping request with a fixed reply.
    /// </summary>
    /// <param name="message">The message to send with the ping request. This parameter is not used in the response.</param>
    /// <returns>A string containing the response to the ping request. Always returns "Pong".</returns>
    [McpServerTool(Name = "ping")]
    [Description("ping to the mcp server")]
    public static string Ping(string message)
    {
        return "Custom Pong";
    }
}
