// -------------------------------------------------------------------
// <copyright file="OrderModel.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

namespace AI.Session.Mcp.Models;

/// <summary>
/// Represents an order with an identifier and a list of item names.
/// </summary>
internal class OrderModel(int id, int userId, List<string> items)
{
    /// <summary>
    /// Gets or sets the unique identifier for the order.
    /// </summary>
    public int Id { get; set; } = id;

    /// <summary>
    /// Gets or sets the unique identifier for the user.
    /// </summary>
    public int UserId { get; set; } = userId;

    /// <summary>
    /// Gets or sets the list of item names in the order.
    /// </summary>
    public List<string> Items { get; set; } = items;

    /// <summary>
    /// Creates a sample order with predefined values for demonstration or testing purposes.
    /// </summary>
    /// <remarks>The returned order includes a fixed identifier and a set of example items. This method is
    /// intended for use in sample code, unit tests, or as a template for creating new orders.</remarks>
    /// <returns>An instance of <see cref="OrderModel"/> containing sample data.</returns>
    public static OrderModel CreateSampleOrder()
    {
        return new OrderModel(1, 1234, ["ItemA", "ItemB", "ItemC"]);
    }
}
