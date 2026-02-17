# Webilim.AI.Api

Webilim.AI.Api is a minimal content-generation abstraction for multiple model providers. It exposes a single interface and provides ready-to-use services for Gemini, Ollama (local), and OpenRouter.

**What this library does**
- One interface for generating text content from different providers.
- Provider-specific implementations that you can swap via dependency injection.
- Lightweight DTOs and minimal dependencies.

**Included providers**
- Gemini: `Webilim.AI.Api.Gemini.Service`
- Ollama (local): `Webilim.AI.Api.Ollama.Service`
- OpenRouter: `Webilim.AI.Api.OpenRouterAI.Service`

**NuGet packages**
- Core: `Webilim.AI.Api`
- Gemini: `Webilim.AI.Api.Gemini`
- Ollama: `Webilim.AI.Api.Ollama`
- OpenRouter: `Webilim.AI.Api.OpenRouterAI`

**Install**
```bash
dotnet add package Webilim.AI.Api
dotnet add package Webilim.AI.Api.Gemini
dotnet add package Webilim.AI.Api.Ollama
dotnet add package Webilim.AI.Api.OpenRouterAI
```

**Core interface**
```csharp
public interface IApiContentGenerator
{
    Task<ApiContentGeneratorResult> Generate(string prompt);
}
```

## Quick start

Register a provider and call `Generate`.

**Gemini**
```csharp
builder.Services.AddSingleton<IApiContentGenerator>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new Webilim.AI.Api.Gemini.Service(config["Gemini:ApiKey"]);
});
```

**OpenRouter**
```csharp
builder.Services.AddSingleton<IApiContentGenerator>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new Webilim.AI.Api.OpenRouterAI.Service(
        config["OpenRouter:ApiKey"],
        model: "meta-llama/llama-3.3-8b-instruct:free"
    );
});
```

**Ollama (local)**
```csharp
builder.Services.AddSingleton<IApiContentGenerator>(sp =>
{
    // Example: http://localhost:11434
    return new Webilim.AI.Api.Ollama.Service("http://localhost", 11434);
});
```

**Use the generator**
```csharp
var result = await generator.Generate("Hello!");
Console.WriteLine(result.Answer);
```

## Configuration notes

- API keys are injected externally (no secrets are stored in this repo).
- Gemini expects an API key string.
- OpenRouter expects an API key and optional model name.
- Ollama expects a base address and port to a local server.

## Testing

Mock the interface without calling real APIs:
```csharp
var mock = new Mock<IApiContentGenerator>();
mock.Setup(x => x.Generate(It.IsAny<string>()))
    .ReturnsAsync(new ApiContentGeneratorResult { Answer = "test" });
```
