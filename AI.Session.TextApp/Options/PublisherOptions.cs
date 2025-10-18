// -------------------------------------------------------------------
// <copyright file="PublisherOptions.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using AI.Session.TextApp.Enums;

namespace AI.Session.TextApp.Options;

/// <summary>
/// Represents configuration options for a publisher, including name, API key, URL, model, and activation status.
/// </summary>
public class PublisherOptions
{
    /// <summary>
    /// Gets or sets the name of the publisher.
    /// </summary>
    public PublisherType Name { get; set; }

    /// <summary>
    /// Gets or sets the API key for the publisher.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Gets or sets the URL of the publisher.
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the model used by the publisher.
    /// </summary>
    public string Model { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the publisher is active.
    /// </summary>
    public bool IsActive { get; set; }
}