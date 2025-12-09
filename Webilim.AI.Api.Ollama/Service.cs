using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Webilim.AI.Api.Dto;

namespace Webilim.AI.Api.Ollama
{
	public class Service : IApiContentGenerator
	{
		private readonly string _ipadress;
		private readonly int _port;

		public Service(string ipadress, int port = 11434)
		{
			_ipadress = ipadress;
			_port = port;
		}

		public async Task<ApiContentGeneratorResult> Generate(string prompt)
		{
			using var client = new HttpClient
			{
				Timeout = TimeSpan.FromSeconds(500)
			};

			var requestBody = new
			{
				model = "llama3",
				prompt = prompt,
				stream = false,
				raw = true
			};

			var json = JsonSerializer.Serialize(requestBody);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			try
			{
				// Ollama Local API endpoint
				using var response = await client.PostAsync($"{_ipadress}:{_port}/api/generate", content);
				response.EnsureSuccessStatusCode();

				var result = await response.Content.ReadAsStringAsync();

				// JSON'dan response alanını çekmek için basit parsing
				using var doc = JsonDocument.Parse(result);
				string text = doc.RootElement.GetProperty("response").GetString();

				return new ApiContentGeneratorResult()
				{
					Answer = text,
					Succeed = true,
					ErrorMessage = ""
				};
			}
			catch (Exception ex)
			{
				return new ApiContentGeneratorResult()
				{
					Answer = "",
					Succeed = false,
					ErrorMessage = ex.Message
				};
			}
		}
	}
}
