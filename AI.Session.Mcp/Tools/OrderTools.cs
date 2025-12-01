// -------------------------------------------------------------------
// <copyright file="OrderTools.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using System.ComponentModel;

using AI.Session.Mcp.Models;
using AI.Session.Mcp.Services.Abstracts;
using ModelContextProtocol.Server;

namespace AI.Session.Mcp.Tools;

/// <summary>
/// Provides utility methods for retrieving and managing order information using the specified order service.
/// </summary>
/// <param name="orderService">The order service used to access and manage order data.</param>
[McpServerToolType]
internal class OrderTools(IOrderService orderService)
{
    private readonly IOrderService _orderService = orderService;

    /// <summary>
    /// Retrieves a list of all orders currently stored in the system.
    /// </summary>
    /// <returns>A list of <see cref="OrderModel"/> objects representing all orders. The list will be empty if there are no
    /// orders.</returns>
    [McpServerTool(Name ="get_all_orders", ReadOnly = true, Title = "get_all_orders")]
    [Description("Get all the orders information.")]
    public McpResponseModel<List<OrderModel>> GetAllOrders()
    {
        var orders =  _orderService.GetAll();
        return new McpResponseModel<List<OrderModel>>(orders, "get_all_orders");
    }

    /// <summary>
    /// Retrieves a list of orders associated with the specified user identifier.
    /// </summary>
    /// <param name="userId">The unique identifier of the user whose orders are to be retrieved.</param>
    /// <returns>A list of <see cref="OrderModel"/> objects representing the orders placed by the specified user. Returns an
    /// empty list if the user has no orders.</returns>
    [McpServerTool(Name = "get_orders_by_id", ReadOnly = true, Title = "get_orders_by_id")]
    [Description("Fetch the order details with the help of user id.")]
    public List<OrderModel>? GetOrdersByUserId(
        [Description("User Id of the user.")] int userId)
    {
        if (userId <= 0)
        {
            return [];
        }

        return _orderService.GetOrdersByUserId(userId);
    }
}
