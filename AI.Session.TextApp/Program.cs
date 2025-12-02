// -------------------------------------------------------------------
// <copyright file="Program.cs" company="Kanhaya Tyagi">
// Copyright 2025 kanhaiyatyagi63 All rights reserved.
// </copyright>
// -------------------------------------------------------------------

using AI.Session.TextApp;
using AI.Session.TextApp.Factory;
using AI.Session.TextApp.Factory.Abstracts;
using AI.Session.TextApp.Options;
using AI.Session.TextApp.Services;
using AI.Session.TextApp.Services.Abstracts;
using Microsoft.Extensions.AI;
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

        services.AddScoped<IInternalClientFactory, InternalClientFactory>();

        services.AddChatClient(x =>
        {
            return x.GetRequiredService<IInternalClientFactory>().GetChatClient();
        });

        services.AddEmbeddingGenerator(x =>
        {
            return x.GetRequiredService<IInternalClientFactory>().GetEmbeddingGenerator();
        });

        //services.AddScoped<IChatService, ChatCompletionService>();
        //services.AddScoped<IChatService, ChatStreamingService>();
        
        //services.AddScoped<IChatService, ChatClassificationService>();
        //services.AddScoped<IChatService, ChatSummarizationService>();
        //services.AddScoped<IChatService, ChatSentimentAnalysisService>();
        //services.AddScoped<IChatService, ChatStructuredOutputService>();
        //services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IChatService, ChatMcpService>();
        
        //services.AddScoped<IMovieService, MovieService>();
        //services.AddScoped<IChatService, ChatFunctionService>();
        services.AddSingleton<VectorStore>();
    })
    .Build();

var chatService = host.Services.GetRequiredService<IChatService>();
await chatService.ExecuteAsync("What is the current temperature of meerut.");

//var movieService = host.Services.GetRequiredService<IMovieService>();
//await movieService.ExecuteAsync();
await host.RunAsync();