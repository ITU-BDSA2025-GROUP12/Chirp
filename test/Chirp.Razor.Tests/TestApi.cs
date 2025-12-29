/*namespace Chirp.Razor.Tests;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Test.CustomWebApplicationFactory;

public class TestAPI : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public TestAPI(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false, HandleCookies = true });
    }

    [Fact]
    public async Task CanSeePublicTimeline()
    {
        var response = await _client.GetAsync("/");
        var test = Environment.GetEnvironmentVariable("ToolsPath");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("Chirp!", content);
        Assert.Contains("Public Timeline", content);
    }

    [Theory]
    [InlineData("Helge")]
    [InlineData("Adrian")]
    //This test throws System.NotImplementedException because the method is currently not implemented in CheepRepository
    public async Task CanSeePrivateTimeline(string author)
    {
        var response = await _client.GetAsync($"/{author}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
    
        Assert.Contains("Chirp!", content);
        Assert.Contains($"{author}'s Timeline", content);
    }
    [Fact]
     //This test throws System.NotImplementedException because the method is currently not implemented in CheepRepository
    public async Task OutputTestHelge()
    {
        var response = await _client.GetAsync($"/{"Helge"}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        string expected = "Hello, BDSA students!";
        Assert.Contains(expected, content);
    }

    [Fact]
     //This test throws System.NotImplementedException because the method is currently not implemented in CheepRepository
    public async Task OutputTestAdrian()
    {
        var response = await _client.GetAsync($"/{"Adrian"}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        string expected = "Hej, velkommen til kurset.";
        Assert.Contains(expected, content);
    }

}
*/