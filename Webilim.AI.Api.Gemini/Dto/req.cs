namespace Webilim.AI.Api.Gemini.Dto
{
	using Newtonsoft.Json;
	using System.Collections.Generic;

	public class GenerateContentRequest
	{
		[JsonProperty("contents")]
		public List<Content> Contents { get; set; } = new List<Content>();
	}

	public class Content
	{
		[JsonProperty("parts")]
		public List<Part> Parts { get; set; } = new List<Part>();
	}

	public class Part
	{
		[JsonProperty("text")]
		public string Text { get; set; }
	}
}
