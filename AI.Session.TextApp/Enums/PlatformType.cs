// -------------------------------------------------------------------
// <copyright file="PlatformType.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

namespace AI.Session.TextApp.Enums;

/// <summary>
/// Specifies the supported platforms and model types for the application.
/// </summary>
public enum PlatformType
{
    /// <summary>
    /// Represents the ollama model type.
    /// </summary>
    Ollama = 1,

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
