using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using Webilim.AI.Api.Dto;
using Webilim.AI.Api.Gemini.Dto;

namespace Webilim.AI.Api.Gemini
{
	public class Service : IApiContentGenerator
	{
		private readonly string _apiKey;

		public Service(string apiKey)
		{
			_apiKey = apiKey;
		}

		public async Task<ApiContentGeneratorResult> Generate(string prompt)
		{
			var requestBody = new GenerateContentRequest
			{
				Contents = new List<Content>
			{
				new Content
				{
					Parts = new List<Part>
					{
						new Part { Text = prompt }
					}
				}
			}
			};

			var options = new RestClientOptions("https://generativelanguage.googleapis.com")
			{
				MaxTimeout = -1,
			};
			var client = new RestClient(options);
			var request = new RestRequest($"/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}", Method.Post);
			request.AddHeader("Content-Type", "application/json");

			var reqBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
			request.AddStringBody(reqBody, DataFormat.Json);
			RestResponse response = client.ExecuteAsync(request).Result;
			var result = JsonConvert.DeserializeObject<GeminiResponseModel>(response.Content);

			if (result.candidates == null)
			{
				return new ApiContentGeneratorResult()
				{
					Answer = "",
					ErrorMessage = "Candidates are null",
					Succeed = false
				};
			}

			return new ApiContentGeneratorResult()
			{
				Answer = result.candidates[0].content.parts[0].text,
				ErrorMessage = "",
				Succeed = true
			};
		}
	}
}
