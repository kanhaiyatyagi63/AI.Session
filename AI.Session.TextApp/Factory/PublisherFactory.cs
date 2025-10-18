// -------------------------------------------------------------------
// <copyright file="PublisherFactory.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using System.ClientModel;

using AI.Session.TextApp.Enums;
using AI.Session.TextApp.Factory.Abstracts;
using AI.Session.TextApp.Options;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;
using OllamaSharp;
using OpenAI;

namespace AI.Session.TextApp.Factory;

/// <summary>
/// Provides a factory for creating instances of large language models (LLMs).
/// </summary>
/// <remarks>This class serves as a central point for constructing and configuring LLM instances.  Use this
/// factory to ensure consistent initialization and management of LLM resources.</remarks>
public class PublisherFactory(IOptions<LangModelOptions> langModelOptions) : IPublisherFactory
{
    private readonly LangModelOptions _langModelOptions = langModelOptions.Value;

    /// <summary>
    /// Retrieves an instance of a chat client based on the specified large language model (LLM) type.
    /// </summary>
    /// <remarks>This method currently does not implement any specific chat client and will throw a  <see
    /// cref="NotImplementedException"/> for all supported LLM types.</remarks>
    /// <returns>An instance of <see cref="IChatClient"/> corresponding to the specified LLM type.</returns>
    public IChatClient GetChatClient()
    {
        var publisher = _langModelOptions.Publishers.SingleOrDefault(p => p.IsActive)
            ?? throw new InvalidOperationException("No active publisher found in the configuration.");

        switch (publisher.Name)
        {
            case PublisherType.Ollamh:
                return new OllamaApiClient(new Uri(publisher.Url), publisher.Model);

            case PublisherType.GitHub:

                var options = new OpenAIClientOptions()
                {
                    Endpoint = new Uri(publisher.Url),
                };

                var client = new OpenAIClient(new ApiKeyCredential(publisher.ApiKey!), options)
                    .GetChatClient(publisher.Model)
                    .AsIChatClient();

                return client;
            case PublisherType.OpenAI:
                // Return an instance of OpenAI chat client
                throw new NotImplementedException("OpenAI chat client is not implemented yet.");
            case PublisherType.Azure:
                // Return an instance of Azure chat client
                throw new NotImplementedException("Azure chat client is not implemented yet.");
            default:
                throw new ArgumentOutOfRangeException(nameof(publisher.Name), publisher.Name, "Unsupported publisher name.");
        }
    }
}
