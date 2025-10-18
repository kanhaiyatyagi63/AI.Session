// -------------------------------------------------------------------
// <copyright file="LangModelOptions.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

namespace AI.Session.TextApp.Options;

/// <summary>
/// Represents configuration options for language model publishers.
/// </summary>
public class LangModelOptions
{
    /// <summary>
    /// The configuration section name for language model options.
    /// </summary>
    public const string SectionName = "LangModel";

    /// <summary>
    /// Gets or sets the array of publisher options for language models.
    /// </summary>
    public PublisherOptions[] Publishers { get; set; } = [];
}
