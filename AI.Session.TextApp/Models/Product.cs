// -------------------------------------------------------------------
// <copyright file="Product.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace AI.Session.TextApp.Models;

/// <summary>
/// Represents a product with detailed information such as title, description, brand, category, pricing, availability, SKU, rating, and tags.
/// </summary>
internal class Product
{
    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the product description.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the product color.
    /// </summary>
    [JsonPropertyName("color")]
    public string? Color { get; set; }

    /// <summary>
    /// Gets or sets the product price.
    /// </summary>
    [JsonPropertyName("price")]
    public Price? Price { get; set; }

    /// <summary>
    /// Gets or sets the product availability status.
    /// </summary>
    [JsonPropertyName("availability")]
    public string? Availability { get; set; }

    /// <summary>
    /// Gets or sets the product code.
    /// </summary>
    [JsonPropertyName("product_code")]
    public string? ProductCode { get; set; }

    /// <summary>
    /// Gets or sets the customer rating for the product.
    /// </summary>
    [JsonPropertyName("customer_rating")]
    public CustomerRating? CustomerRating { get; set; }
}
