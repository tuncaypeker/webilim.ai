using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Webilim.AI.Api.Dto;

namespace Webilim.AI.Api.OpenRouterAI
{
	public class Service : IApiContentGenerator
	{
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
			var result = new ApiContentGeneratorResult();

			try
			{
				var url = "https://openrouter.ai/api/v1/chat/completions";

				using var http = new HttpClient();

				http.DefaultRequestHeaders.Authorization =
					new AuthenticationHeaderValue("Bearer", _apiKey);

				// OpenRouter için önerilen header'lar
				http.DefaultRequestHeaders.Add("HTTP-Referer", "https://google.com");
				http.DefaultRequestHeaders.Add("X-Title", "My App");

				var request = new OpenRouterRequest
				{
					Model = _model,
					Messages = new List<Message>
					{
						new() { Role = "system", Content = "You are a helpful assistant." },
						new() { Role = "user", Content = prompt }
					}
				};

				var json = JsonSerializer.Serialize(request);
				using var content = new StringContent(json, Encoding.UTF8, "application/json");

				var response = await http.PostAsync(url, content);

				if (!response.IsSuccessStatusCode)
				{
					result.Succeed = false;
					result.ErrorMessage =
						$"HTTP {(int)response.StatusCode} - {await response.Content.ReadAsStringAsync()}";
					return result;
				}

				var responseJson = await response.Content.ReadAsStringAsync();
				var openRouterResponse =
					JsonSerializer.Deserialize<OpenRouterResponse>(responseJson);

				var answer = openRouterResponse?
					.Choices?
					.FirstOrDefault()?
					.Message?
					.Content;

				if (string.IsNullOrWhiteSpace(answer))
				{
					result.Succeed = false;
					result.ErrorMessage = "Model returned empty response.";
					return result;
				}

				result.Succeed = true;
				result.Answer = answer;
				return result;
			}
			catch (Exception ex)
			{
				result.Succeed = false;
				result.ErrorMessage = ex.Message;
				return result;
			}
		}
	}

	public class OpenRouterRequest
	{
		[JsonPropertyName("model")]
		public string Model { get; set; }

		[JsonPropertyName("messages")]
		public List<Message> Messages { get; set; }
	}

	public class Message
	{
		[JsonPropertyName("role")]
		public string Role { get; set; } // system | user | assistant

		[JsonPropertyName("content")]
		public string Content { get; set; }
	}

	public class OpenRouterResponse
	{
		[JsonPropertyName("choices")]
		public List<Choice> Choices { get; set; }
	}

	public class Choice
	{
		[JsonPropertyName("message")]
		public Message Message { get; set; }
	}
}
