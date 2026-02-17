# Webilim.AI.Api.OpenRouterAI

`Webilim.AI.Api.OpenRouterAI` is the OpenRouter provider implementation for `Webilim.AI.Api`.

## NuGet package

- `Webilim.AI.Api.OpenRouterAI`

## Install

```bash
dotnet add package Webilim.AI.Api.OpenRouterAI
```

## What this package includes

- `OpenRouterService` implementation of `IApiContentGenerator`

## Usage

```csharp
IApiContentGenerator generator = new Service(
    apiKey: "YOUR_OPENROUTER_API_KEY",
    model: "meta-llama/llama-3.3-8b-instruct:free"
);
var result = await generator.Generate("Create a short product description for a coffee grinder.");
Console.WriteLine(result.Answer);
```

## Dependency

This package depends on `Webilim.AI.Api` for shared abstractions.
