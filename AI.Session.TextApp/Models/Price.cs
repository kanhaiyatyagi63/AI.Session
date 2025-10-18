// -------------------------------------------------------------------
// <copyright file="Price.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace AI.Session.TextApp.Models;

/// <summary>
/// Represents a price with an amount and currency.
/// </summary>
public class Price
{
    /// <summary>
    /// Gets or sets the monetary amount.
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// Gets or sets the currency code (e.g., USD, EUR).
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }
}
