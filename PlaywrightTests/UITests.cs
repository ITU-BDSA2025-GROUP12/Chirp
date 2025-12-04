using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class UItests : PageTest {
	[Test]
	public async Task logIn() {
		await Page.GotoAsync("https://bdsa2025gr12chirprazor-gzh9b7ghhxb0cybn.norwayeast-01.azurewebsites.net/");
		await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
		await Page.GetByPlaceholder("name@example.com").ClickAsync();
		await Page.GetByPlaceholder("name@example.com").FillAsync("test@test.dk");
		await Page.GetByPlaceholder("password").ClickAsync();
		await Page.GetByPlaceholder("password").FillAsync("Test1234!");
		await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
		await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Tell others what's on your" }))
			.ToBeVisibleAsync();
		await Expect(Page.GetByText("Tell others what's on your mind testuser! Cheep")).ToBeVisibleAsync();
	}

	[Test]
	public async Task registerEmailInUse() {
		await Page.GotoAsync("https://bdsa2025gr12chirprazor-gzh9b7ghhxb0cybn.norwayeast-01.azurewebsites.net/");
		await Page.GetByRole(AriaRole.Link, new() { Name = "Register" }).ClickAsync();
		await Page.GetByPlaceholder("name@example.com").ClickAsync();
		await Page.GetByPlaceholder("name@example.com").FillAsync("test@test.dk");
		await Page.GetByPlaceholder("name", new() { Exact = true }).ClickAsync();
		await Page.GetByPlaceholder("name", new() { Exact = true }).FillAsync("test");
		await Page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
		await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("Test1234!");
		await Page.GetByLabel("Confirm Password").ClickAsync();
		await Page.GetByLabel("Confirm Password").FillAsync("Test1234!");
		await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
		await Expect(Page.GetByRole(AriaRole.Listitem)).ToContainTextAsync("Email is in use.");
	}
	[Test]
	public async Task logInLogOut() {
		await Page.GotoAsync("https://bdsa2025gr12chirprazor-gzh9b7ghhxb0cybn.norwayeast-01.azurewebsites.net/");
		await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
		await Page.GetByPlaceholder("name@example.com").ClickAsync();
		await Page.GetByPlaceholder("name@example.com").FillAsync("test@test.dk");
		await Page.GetByPlaceholder("password").ClickAsync();
		await Page.GetByPlaceholder("password").FillAsync("Test1234!");
		await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
		await Page.GetByRole(AriaRole.Link, new() { Name = "Logout [test1234]" }).ClickAsync();
		await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Click here to Logout" })).ToBeVisibleAsync();
		await Expect(Page.Locator("body")).ToContainTextAsync("Click here to Logout");
		await Page.GetByRole(AriaRole.Button, new() { Name = "Click here to Logout" }).ClickAsync();
		await Expect(Page.GetByRole(AriaRole.Main)).ToContainTextAsync("You have successfully logged out of the application.");
	}
	
}