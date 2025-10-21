// -------------------------------------------------------------------
// <copyright file="ChatStructuredOutputService.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using System.Text.Json;

using AI.Session.TextApp.Factory.Abstracts;
using AI.Session.TextApp.Models;
using AI.Session.TextApp.Services.Abstracts;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging;

namespace AI.Session.TextApp.Services;

/// <summary>
/// Provides functionality for processing and generating structured output from chat-based text input.
/// </summary>
/// <remarks>This service is intended to handle chat-related text processing tasks, ensuring that the output
/// adheres to a structured format. It implements the <see cref="IChatService"/> interface, which defines the contract
/// for text-related operations.</remarks>
internal class ChatStructuredOutputService(ILogger<ChatStructuredOutputService> logger,
    IPlatformFactory platformFactory) : IChatService
{
    private readonly ILogger<ChatStructuredOutputService> _logger = logger;
    private readonly IPlatformFactory _platformFactory = platformFactory;

    /// <inheritdoc/>
    public async Task ExecuteAsync(string prompt)
    {
        if (string.IsNullOrEmpty(prompt))
        {
            prompt = "Provide a structured JSON output for the following products information:\n" +
                "Introducing the Apple Watch Series 9 (GPS, 41mm) in Midnight Aluminum Case with Sport Band.\r\nFeatures advanced health tracking, heart rate monitoring, and Always-On Retina display.\r\nPrice: $399 USD.  \r\nCurrently available for preorder.  \r\nProduct code: APL-WTCH9-MDNT41.  \r\nCustomer rating: 4.8 out of 5 (5,320 reviews)." +
                "Experience the Samsung Galaxy Tab S9 FE with 10.9\" display and 8GB RAM.\r\nComes with S Pen included and 128GB storage.\r\nAvailable in colors: Mint, Gray, and Silver.\r\nPrice: $449.99 USD.  \r\nIn stock now.  \r\nProduct code: SMG-TABS9FE.  \r\nCustomer rating: 4.6 out of 5 (2,875 reviews).\r\n" +
                "Introducing Levi’s 511 Slim Fit Men’s Jeans.\r\nMade from 99% cotton and 1% elastane for extra comfort.\r\nAvailable in waist sizes 28–40 and lengths 30–34.\r\nColors: Indigo Blue, Jet Black, and Charcoal.\r\nPrice: $69.50 USD.  \r\nIn stock.  \r\nProduct code: LEV-511-SLIM.  \r\nCustomer rating: 4.5 out of 5 (4,210 reviews).\r\n" +
                "Dyson Supersonic Hair Dryer with intelligent heat control to protect shine.\r\nIncludes 5 magnetic styling attachments and a powerful digital motor.\r\nColor: Prussian Blue/Copper.\r\nPrice: $429.00 USD.  \r\nCurrently in stock.  \r\nProduct code: DYS-SPSONIC-BLCP.  \r\nCustomer rating: 4.9 out of 5 (8,750 reviews).\r\n" +
                "Logitech MX Master 3S Wireless Performance Mouse.\r\nFeatures ultra-fast scrolling, customizable buttons, and silent clicks.\r\nCompatible with Windows, macOS, and Linux.\r\nColors available: Graphite and Pale Gray.\r\nPrice: $99.99 USD.  \r\nIn stock.  \r\nProduct code: LOG-MXM3S.  \r\nCustomer rating: 4.8 out of 5 (12,430 reviews).\r\n";
        }

        _logger.LogInformation("User ->>> {Prompt}", prompt);

        var chatClient = _platformFactory.GetChatClient();

        var chatResponse = await chatClient.GetResponseAsync<List<Product>>(prompt, new ChatOptions()
        {
            Instructions = "Please provide the output in a well-structured JSON format as  a list same as this" +
           @"{
  ""name"": ""Dyson Supersonic Hair Dryer"",
  ""description"": ""With intelligent heat control to protect shine. Includes 5 magnetic styling attachments and a powerful digital motor."",
  ""color"": ""Prussian Blue/Copper"",
  ""price"": { ""amount"": 429.0, ""currency"": ""USD"" },
  ""availability"": ""In stock"",
  ""product_code"": ""DYS-SPSONIC-BLCP"",
  ""customer_rating"": { ""score"": 4.9, ""scale"": 5, ""reviews"": 8750 }
}",
        });

        _logger.LogInformation("Chat Response ->>>");

        var jso = new JsonSerializerOptions { WriteIndented = true, };

        if (chatResponse.TryGetResult(out var products))
        {
            foreach (var product in products)
            {
                _logger.LogInformation("Product ->>> {Product}", JsonSerializer.Serialize(product, jso));
            }
        }
        else
        {
            _logger.LogWarning("No products found in the chat response.");
        }
    }
}
