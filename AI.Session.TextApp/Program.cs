// -------------------------------------------------------------------
// <copyright file="Program.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using AI.Session.TextApp.Factory;
using AI.Session.TextApp.Factory.Abstracts;
using AI.Session.TextApp.Options;
using AI.Session.TextApp.Services;
using AI.Session.TextApp.Services.Abstracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<LangModelOptions>(context.Configuration.GetSection(LangModelOptions.SectionName));

        services.AddScoped<IPublisherFactory, PublisherFactory>();

        //services.AddScoped<ITextService, ChatCompletionService>();
        // services.AddScoped<ITextService, ChatStreamingService>();
        // services.AddScoped<ITextService, ChatClassificationService>();
        //services.AddScoped<ITextService, ChatSummarizationService>();
        //services.AddScoped<ITextService, ChatSentimentAnalysisService>();
        //services.AddScoped<ITextService, ChatStructuredOutputService>();
        services.AddScoped<ITextService, ChatService>();
    })
    .Build();

var textService = host.Services.GetRequiredService<ITextService>();

await textService.ExecuteAsync("What is microservice in 200 words.");

await host.RunAsync();