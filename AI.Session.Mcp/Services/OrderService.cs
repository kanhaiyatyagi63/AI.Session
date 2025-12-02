// -------------------------------------------------------------------
// <copyright file="OrderService.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using AI.Session.Mcp.Models;
using AI.Session.Mcp.Services.Abstracts;

namespace AI.Session.Mcp.Services;

/// <summary>
/// Provides operations for managing and processing orders within the system.
/// </summary>
internal class OrderService : IOrderService
{
    private readonly List<OrderModel> _orders = [
            new OrderModel(1, 1001, ["Laptop", "Mouse"]),
            new OrderModel(2, 1002, ["Smartphone"]),
            new OrderModel(3, 1001, ["Keyboard", "Monitor"]),
            new OrderModel(4, 1003, ["Tablet", "Stylus"]),
        ];

    /// <inheritdoc/>
    public List<OrderModel> GetAll()
    {
        return [.. _orders];
    }

    /// <inheritdoc/>
    public List<OrderModel>? GetOrdersByUserId(int userId)
    {
        return [.. _orders.Where(order => order.UserId == userId)];
    }
}
