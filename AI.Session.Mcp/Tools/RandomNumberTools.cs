// -------------------------------------------------------------------
// <copyright file="RandomNumberTools.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using System.ComponentModel;

using ModelContextProtocol.Server;

namespace AI.Session.Mcp.Tools;

/// <summary>
/// Sample MCP tools for demonstration purposes.
/// These tools can be invoked by MCP clients to perform various operations.
/// </summary>
[McpServerToolType]
internal class RandomNumberTools
{
    /// <summary>
    /// Generates a random integer within the specified range.
    /// </summary>
    /// <param name="min">The inclusive lower bound of the random number to be generated. Must be less than or equal to <paramref
    /// name="max"/>.</param>
    /// <param name="max">The exclusive upper bound of the random number to be generated. Must be greater than or equal to <paramref
    /// name="min"/>.</param>
    /// <returns>A random integer greater than or equal to <paramref name="min"/> and less than <paramref name="max"/>.</returns>
    [McpServerTool]
    [Description("Generates a random number between the specified minimum and maximum values.")]
    public static int GetRandomNumber(
        [Description("Minimum value (inclusive)")] int min = 0,
        [Description("Maximum value (exclusive)")] int max = 100)
    {
        return Random.Shared.Next(min, max);
    }
}
