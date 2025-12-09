# Webilim.AI.Api

A lightweight and unified abstraction layer for sending prompts to multiple AI providers.  
This library standardizes how you interact with models such as **Gemini**, **Ollama**, and **OpenRouterAI**, using a single interface across all implementations.

---

## ✨ Features

- Unified interface for multiple AI providers  
- Built-in implementations:
  - **GeminiService**
  - **OllamaService**
  - **OpenRouterService**
- Provider-agnostic application code  
- Highly testable (easy to mock)  
- API keys and configuration injected from outside (DI-friendly)  
- Minimal dependencies and clean DTO structure  
- Simple, extendable architecture  

---

## 📐 Core Interface
```
public interface IApiContentGenerator
{
    Task<ApiContentGeneratorResult> Generate(string prompt);
}
````

## 🚀 Quick Start

1️⃣ Register a provider (example: Gemini)
```
builder.Services.AddSingleton<IApiContentGenerator>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new GeminiService(config["Gemini:ApiKey"]);
});
````
2️⃣ Send a prompt
```
var result = await generator.Generate("Hello!");
Console.WriteLine(result.Answer);
````

## 🧪 Testing

Mock the interface without calling real APIs:

```
var mock = new Mock<IApiContentGenerator>();
mock.Setup(x => x.Generate(It.IsAny<string>()))
    .ReturnsAsync(new ApiContentGeneratorResult { Answer = "test" });
````
