using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using System.Text.RegularExpressions;
using Test.CustomWebApplicationFactory;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Withywoods.WebTesting;
 
namespace Test.PlayWrightTests;

public class EndToEndTests : PageTest, IClassFixture<CustomWebApplicationFactory<Program>>{
	private readonly CustomWebApplicationFactory<Program> _factory;
	private string _baseUrl;
	private HttpClient _client;
	
	public EndToEndTests(CustomWebApplicationFactory<Program> factory) {
		_factory = factory;
		_client = factory.CreateClient();
		_baseUrl = _client.BaseAddress!.ToString();
	}
	
	[Fact]
	public async Task test()
	{
		await Page.GotoAsync(_baseUrl);
		
		await Expect(Page.Locator("h1")).ToContainTextAsync("Chirp!");
	}


}