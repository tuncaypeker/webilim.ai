# Webilim.AI.Api.Gemini

`Webilim.AI.Api.Gemini` is the Google Gemini provider implementation for `Webilim.AI.Api`.

## NuGet package

- `Webilim.AI.Api.Gemini`

## Install

```bash
dotnet add package Webilim.AI.Api.Gemini
```

## What this package includes

- `GeminiService` implementation of `IApiContentGenerator`
- Gemini request/response DTOs used by the provider

## Usage

```csharp
IApiContentGenerator generator = new Service("YOUR_GEMINI_API_KEY");
var result = await generator.Generate("Write a short summary about clean architecture.");
Console.WriteLine(result.Answer);
```

## Dependency

This package depends on `Webilim.AI.Api` for shared abstractions.
