using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Webilim.AI.Api.Dto;

namespace Webilim.AI.Api.OpenRouterAI
{
	public class Service : IApiContentGenerator
	{
		readonly string url = "https://openrouter.ai/api/v1/chat/completions";

		private readonly string _apiKey;
		private readonly string _model;

		public Service(string apiKey, string model = "meta-llama/llama-3.3-8b-instruct:free")
		{
			_apiKey = apiKey;
			_model = model;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="prompt"></param>
		/// <param name="model">
		/// meta-llama/llama-3.3-8b-instruct:free
		/// deepseek/deepseek-chat-v3.1:free
		/// x-ai/grok-4-fast:free
		/// </param>
		/// <returns></returns>
		public async Task<ApiContentGeneratorResult> Generate(string prompt)
		{
			using var client = new HttpClient
			{
				Timeout = TimeSpan.FromSeconds(30)
			};

			client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
			//client.DefaultRequestHeaders.Add("HTTP-Referer", "https://seninsiten.com");
			//client.DefaultRequestHeaders.Add("X-Title", "BenimUygulamam");

			var requestBody = new
			{
				model = _model,
				messages = new[]
				{
				new { role = "user", content = prompt }
			}
			};

			var json = JsonSerializer.Serialize(requestBody);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			try
			{
				var response = await client.PostAsync(url, content);

				if (!response.IsSuccessStatusCode)
				{
					return new ApiContentGeneratorResult
					{
						Succeed = false,
						ErrorMessage = $"HTTP Hatası: {response.StatusCode}"
					};
				}

				var result = await response.Content.ReadAsStringAsync();

				using var doc = JsonDocument.Parse(result);

				// Eğer error alanı varsa
				if (doc.RootElement.TryGetProperty("error", out var error))
				{
					return new ApiContentGeneratorResult
					{
						Succeed = false,
						ErrorMessage = error.GetProperty("message").GetString()
					};
				}

				// Normal cevap
				var answer = doc.RootElement
					.GetProperty("choices")[0]
					.GetProperty("message")
					.GetProperty("content")
					.GetString();

				return (new ApiContentGeneratorResult
				{
					Succeed = true,
					Answer = answer
				});
			}
			catch (TaskCanceledException)
			{
				return (new ApiContentGeneratorResult
				{
					Succeed = false,
					ErrorMessage = "İstek zaman aşımına uğradı."
				});
			}
			catch (Exception ex)
			{
				return (new ApiContentGeneratorResult
				{
					Succeed = false,
					ErrorMessage = "Genel hata: " + ex.Message
				});
			}
		}
	}
}
