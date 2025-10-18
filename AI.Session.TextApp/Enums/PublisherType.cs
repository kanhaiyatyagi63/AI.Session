// -------------------------------------------------------------------
// <copyright file="PublisherType.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

namespace AI.Session.TextApp.Enums;

/// <summary>
/// Represents the types of Large Language Model (LLM) providers supported by the system.
/// </summary>
/// <remarks>This enumeration is used to specify the LLM provider being utilized in various operations. Each value
/// corresponds to a specific provider: <list type="bullet"> <item> <description><see cref="Ollamh"/>: Represents the
/// Ollamh LLM provider.</description> </item> <item> <description><see cref="GitHub"/>: Represents the GitHub LLM
/// provider.</description> </item> <item> <description><see cref="OpenAI"/>: Represents the OpenAI LLM
/// provider.</description> </item> <item> <description><see cref="Azure"/>: Represents the Azure LLM
/// provider.</description> </item> </list></remarks>
public enum PublisherType
{
    /// <summary>
    /// Represents the Ollamh model type.
    /// </summary>
    Ollamh = 1,

    /// <summary>
    /// Represents the GitHub platform in the enumeration.
    /// </summary>
    GitHub = 2,

    /// <summary>
    /// Represents the OpenAI model type.
    /// </summary>
    OpenAI = 3,

    /// <summary>
    /// Represents the Azure cloud environment.
    /// </summary>
    /// <remarks>This value is typically used to specify the Azure environment in configurations or API calls
    /// that support multiple cloud environments.</remarks>
    Azure = 4,
}
