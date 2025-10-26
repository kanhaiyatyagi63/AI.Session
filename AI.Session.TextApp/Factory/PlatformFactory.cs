// -------------------------------------------------------------------
// <copyright file="PlatformFactory.cs" company="Kanhaya Tyagi">
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
public class PlatformFactory(IOptions<LangModelOptions> langModelOptions) : IPlatformFactory
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
        var platform = _langModelOptions.ChatModel.SingleOrDefault(p => p.IsActive)
            ?? throw new InvalidOperationException("No active platform found in the configuration.");

        switch (platform.Name)
        {
            case PlatformType.Ollama:
                return  new OllamaApiClient(new Uri(platform.Url), platform.Model);

            case PlatformType.GitHub:

                var options = new OpenAIClientOptions()
                {
                    Endpoint = new Uri(platform.Url),
                };

                var client = new OpenAIClient(new ApiKeyCredential(platform.ApiKey!), options)
                    .GetChatClient(platform.Model)
                    .AsIChatClient()
                    .AsBuilder()
                    .UseFunctionInvocation()
                    .Build();

                return client;
            case PlatformType.OpenAI:
                // Return an instance of OpenAI chat client
                throw new NotImplementedException("OpenAI chat client is not implemented yet.");
            case PlatformType.Azure:
                // Return an instance of Azure chat client
                throw new NotImplementedException("Azure chat client is not implemented yet.");
            default:
                throw new ArgumentOutOfRangeException(nameof(platform.Name), platform.Name, "Unsupported platform name.");
        }
    }

    /// <summary>
    /// Retrieves an instance of an embedding generator based on the active platform configuration.
    /// </summary>
    /// <remarks>The method determines the active platform from the configuration and returns an appropriate 
    /// embedding generator instance. If no active platform is found, or if the platform type is not  supported, an
    /// exception is thrown.</remarks>
    /// <returns>An instance of <see cref="IEmbeddingGenerator"/> configured for the active platform.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no active platform is found in the configuration.</exception>
    /// <exception cref="NotImplementedException">Thrown if the embedding generator for the active platform type is not yet implemented.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the active platform's name is not supported.</exception>
    public IEmbeddingGenerator<string, Embedding<float>> GetEmbeddingGenerator()
    {
        var platform = _langModelOptions.TextModel.SingleOrDefault(p => p.IsActive)
            ?? throw new InvalidOperationException("No active platform found in the configuration.");
        switch (platform.Name)
        {
            case PlatformType.GitHub:

                var options = new OpenAIClientOptions()
                {
                    Endpoint = new Uri(platform.Url),
                };

                var client = new OpenAIClient(new ApiKeyCredential(platform.ApiKey!), options)
                    .GetEmbeddingClient(platform.Model)
                    .AsIEmbeddingGenerator();

                return client;
            case PlatformType.Ollama:
               return  new OllamaApiClient(new Uri(platform.Url), platform.Model);
            case PlatformType.OpenAI:
                // Return an instance of OpenAI embedding generator
                throw new NotImplementedException("OpenAI embedding generator is not implemented yet.");
            case PlatformType.Azure:
                // Return an instance of Azure embedding generator
                throw new NotImplementedException("Azure embedding generator is not implemented yet.");
            default:
                throw new ArgumentOutOfRangeException(nameof(platform.Name), platform.Name, "Unsupported platform name.");
        }
    }
}
