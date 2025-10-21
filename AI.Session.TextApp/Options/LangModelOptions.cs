// -------------------------------------------------------------------
// <copyright file="LangModelOptions.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

namespace AI.Session.TextApp.Options;

/// <summary>
/// Represents configuration options for language model platforms.
/// </summary>
public class LangModelOptions
{
    /// <summary>
    /// The configuration section name for language model options.
    /// </summary>
    public const string SectionName = "LangModel";

    /// <summary>
    /// Gets or sets the array of platform options for chat models.
    /// </summary>
    public PlatformOptions[] ChatModel { get; set; } = [];

    /// <summary>
    /// Gets or sets the array of platform options for text models.
    /// </summary>
    public PlatformOptions[] TextModel { get; set; } = [];
}
