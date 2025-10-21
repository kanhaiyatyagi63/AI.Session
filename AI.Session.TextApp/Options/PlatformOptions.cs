// -------------------------------------------------------------------
// <copyright file="PlatformOptions.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using AI.Session.TextApp.Enums;

namespace AI.Session.TextApp.Options;

/// <summary>
/// Represents configuration options for a platform platform, including details such as the platform's name, API key,
/// URL, model, and activation status.
/// </summary>
/// <remarks>This class is used to encapsulate the configuration settings required to interact with a specific
/// platform platform.  It includes properties for identifying the platform, authenticating requests, and specifying
/// the operational model.</remarks>
public class PlatformOptions
{
    /// <summary>
    /// Gets or sets the name of the platform.
    /// </summary>
    public PlatformType Name { get; set; }

    /// <summary>
    /// Gets or sets the API key for the platform.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Gets or sets the URL of the platform.
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the model used by the platform.
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the platform is active.
    /// </summary>
    public bool IsActive { get; set; }
}