using System.Collections.Generic;

namespace Webilim.AI.Api.Gemini.Dto
{
	public class Candidate
	{
		public ContentRes content { get; set; }
		public string finishReason { get; set; }
		public int index { get; set; }
	}

	public class ContentRes
	{
		public List<PartRes> parts { get; set; }
		public string role { get; set; }
	}

	public class PartRes
	{
		public string text { get; set; }
	}

	public class PromptTokensDetail
	{
		public string modality { get; set; }
		public int tokenCount { get; set; }
	}

	public class GeminiResponseModel
	{
		public List<Candidate> candidates { get; set; }
		public UsageMetadata usageMetadata { get; set; }
		public string modelVersion { get; set; }
		public string responseId { get; set; }
	}

	public class UsageMetadata
	{
		public int promptTokenCount { get; set; }
		public int candidatesTokenCount { get; set; }
		public int totalTokenCount { get; set; }
		public List<PromptTokensDetail> promptTokensDetails { get; set; }
		public int thoughtsTokenCount { get; set; }
	}
}
