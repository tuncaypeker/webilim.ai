# Webilim.AI.Api.Ollama

`Webilim.AI.Api.Ollama` is the Ollama provider implementation for `Webilim.AI.Api`.

## NuGet package

- `Webilim.AI.Api.Ollama`

## Install

```bash
dotnet add package Webilim.AI.Api.Ollama
```

## What this package includes

- `OllamaService` implementation of `IApiContentGenerator`

## Usage

```csharp
IApiContentGenerator generator = new Service(
    ipadress: "http://localhost",
    port: 11434
);
var result = await generator.Generate("Give me 3 blog post title ideas about C# performance.");
Console.WriteLine(result.Answer);
```

## Dependency

This package depends on `Webilim.AI.Api` for shared abstractions.
