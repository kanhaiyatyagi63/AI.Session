// -------------------------------------------------------------------
// <copyright file="IOrderService.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using System.Collections.Generic;

using AI.Session.Mcp.Models;

namespace AI.Session.Mcp.Services.Abstracts;

/// <summary>
/// Defines the contract for services that manage and process orders within the application.
/// </summary>
internal interface IOrderService
{
    /// <summary>
    /// Retrieves all orders in the system.
    /// </summary>
    /// <returns>Get all the order details.</returns>
    List<OrderModel> GetAll();

    /// <summary>
    /// Retrieves all orders associated with a specific user by their user ID.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose orders are to be retrieved.</param>
    /// <returns>A list of orders for the specified user, or null if no orders are found.</returns>
    List<OrderModel>? GetOrdersByUserId(int userId);
}
