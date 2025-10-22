namespace Chirp.Razor.Tests;
using Microsoft.AspNetCore.Mvc.Testing;

public class TestAPI : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _fixture;
    private readonly HttpClient _client;

    public TestAPI(WebApplicationFactory<Program> fixture)
    {
        _fixture = fixture;
        _client = _fixture.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = true, HandleCookies = true });
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
    [InlineData("Sven")]
    [InlineData("Eduard")]
    public async Task CanSeePrivateTimeline(string author)
    {
        var response = await _client.GetAsync($"/{author}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("Chirp!", content);
        Assert.Contains($"{author}'s Timeline", content);
    }
    [Fact]
    public async Task OutputTestHelge()
    {
        var response = await _client.GetAsync($"/{"Helge"}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        string expected = "Hello, BDSA students!";
        Assert.Contains(expected, content);
    }
    
    [Fact]
    public async Task OutputTestAdrian()
    {
        var response = await _client.GetAsync($"/{"Adrian"}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        string expected = "Hej, velkommen til kurset.";
        Assert.Contains(expected, content);
    }

}