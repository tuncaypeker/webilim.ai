namespace Webilim.AI.Api
{
	using Dto;
	using System.Threading.Tasks;

	public interface IApiContentGenerator
	{
		Task<ApiContentGeneratorResult> Generate(string prompt);
	}
}
