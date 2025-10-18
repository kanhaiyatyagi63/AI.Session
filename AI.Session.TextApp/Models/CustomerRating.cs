// -------------------------------------------------------------------
// <copyright file="CustomerRating.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace AI.Session.TextApp.Models;

/// <summary>
/// Represents a customer rating for a product, including score, scale, and number of reviews.
/// </summary>
public class CustomerRating
{
    /// <summary>
    /// Gets or sets the average score given by customers.
    /// </summary>
    [JsonPropertyName("score")]
    public double Score { get; set; }

    /// <summary>
    /// Gets or sets the maximum possible score (scale) for the rating.
    /// </summary>
    [JsonPropertyName("scale")]
    public int Scale { get; set; }

    /// <summary>
    /// Gets or sets the number of customer reviews.
    /// </summary>
    [JsonPropertyName("reviews")]
    public int Reviews { get; set; }
}