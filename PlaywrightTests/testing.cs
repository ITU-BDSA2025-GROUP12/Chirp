using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ExampleTest : PageTest {
	[Test]
	public async Task testingWithChirp() {
		await Page.GotoAsync("https://bdsa2025gr12chirprazor-gzh9b7ghhxb0cybn.norwayeast-01.azurewebsites.net/");
		await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Wings1Chirp!" })).ToBeVisibleAsync();
	}
	
}