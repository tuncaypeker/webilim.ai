# Webilim.AI.Api

`Webilim.AI.Api` provides the core abstraction for AI content generation in the Webilim ecosystem.

## NuGet package

- `Webilim.AI.Api`

## Install

```bash
dotnet add package Webilim.AI.Api
```

## What this package includes

- `IApiContentGenerator` interface
- `ApiContentGeneratorResult` DTO

## Interface

```csharp
public interface IApiContentGenerator
{
    Task<ApiContentGeneratorResult> Generate(string prompt);
}
```

## Intended usage

Install this package in your application and pair it with a provider package, for example:

- `Webilim.AI.Api.Gemini`
- `Webilim.AI.Api.Ollama`
- `Webilim.AI.Api.OpenRouterAI`
