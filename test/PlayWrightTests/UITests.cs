using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace Test.PlayWrightTests;

public class UITests : PageTest{
	[Test]
	public async Task MenuIsVisibleWhenNotLoggedIn() {
		await Page.GotoAsync("https://bdsa2025gr12chirprazor-gzh9b7ghhxb0cybn.norwayeast-01.azurewebsites.net/");
		await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Public timeline" })).ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Register" })).ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Login" })).ToBeVisibleAsync();
	}

	[Test]
	public async Task PublicTimelineIsVisible() {
		await Page.GotoAsync("https://bdsa2025gr12chirprazor-gzh9b7ghhxb0cybn.norwayeast-01.azurewebsites.net/");
		await Expect(Page.Locator("h2")).ToContainTextAsync("Public Timeline");
		await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Public Timeline" })).ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Paragraph).Filter(new() { HasText = "Jacqualine Gilcoine Starbuck now is what we hear the worst. — 2023-08-01 13:" }).GetByRole(AriaRole.Link)).ToBeVisibleAsync();
		await Expect(Page.Locator("#messagelist")).ToContainTextAsync("Jacqualine Gilcoine Starbuck now is what we hear the worst. — 2023-08-01 13:17");
		await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Quintin Sitts" }).Nth(2)).ToBeVisibleAsync();
		await Expect(Page.Locator("#messagelist")).ToContainTextAsync("Quintin Sitts To the credulous mariners it seemed the cunning jeweller would show them when they were swallowed. — 2023-08-01 13:17");
		await Expect(Page.Locator("#messagelist")).ToContainTextAsync("— 2023-08-01 13:17");
	}

	[Test]
	public async Task registerPageHasCorrectElements() {
		await Page.GotoAsync("https://bdsa2025gr12chirprazor-gzh9b7ghhxb0cybn.norwayeast-01.azurewebsites.net/");
		await Page.GetByRole(AriaRole.Link, new() { Name = "Register" }).ClickAsync();
		await Expect(Page.Locator("body")).ToContainTextAsync("Register");
		await Expect(Page.Locator("h2")).ToContainTextAsync("Create a new account.");
		await Expect(Page.GetByRole(AriaRole.Textbox, new() { Name = "Email" })).ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Textbox, new() { Name = "Name" })).ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Textbox, new() { Name = "Password", Exact = true })).ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Textbox, new() { Name = "Confirm Password" })).ToBeVisibleAsync();
		await Expect(Page.Locator("#registerForm")).ToContainTextAsync("Email");
		await Expect(Page.Locator("#registerForm")).ToContainTextAsync("Name");
		await Expect(Page.Locator("#registerForm")).ToContainTextAsync("Password");
		await Expect(Page.Locator("#registerForm")).ToContainTextAsync("Confirm Password");
		await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Register" })).ToBeVisibleAsync();
		await Expect(Page.Locator("#registerSubmit")).ToContainTextAsync("Register");
		await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Use another service to" })).ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "GitHub" })).ToBeVisibleAsync();
		await Expect(Page.Locator("button[name=\"provider\"]")).ToContainTextAsync("GitHub");
	}

	[Test]
	public async Task LoginHasCorrectElements() {
		await Page.GotoAsync("https://bdsa2025gr12chirprazor-gzh9b7ghhxb0cybn.norwayeast-01.azurewebsites.net/");
		await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
		await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Log in", Exact = true })).ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Use a local account to log in." })).ToBeVisibleAsync();
		await Expect(Page.Locator("body")).ToContainTextAsync("Log in");
		await Expect(Page.GetByRole(AriaRole.Textbox, new() { Name = "Email" })).ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Textbox, new() { Name = "Password" })).ToBeVisibleAsync();
		await Expect(Page.Locator("#account")).ToContainTextAsync("Email");
		await Expect(Page.Locator("#account")).ToContainTextAsync("Password");
		await Expect(Page.GetByRole(AriaRole.Checkbox, new() { Name = "Remember me?" })).ToBeVisibleAsync();
		await Expect(Page.Locator("#account")).ToContainTextAsync("Remember me?");
		await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Log in" })).ToBeVisibleAsync();
		await Expect(Page.Locator("#login-submit")).ToContainTextAsync("Log in");
		await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Forgot your password?" })).ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Register as a new user" })).ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Resend email confirmation" })).ToBeVisibleAsync();
		await Expect(Page.Locator("#forgot-password")).ToContainTextAsync("Forgot your password?");
		await Expect(Page.Locator("#account")).ToContainTextAsync("Register as a new user");
		await Expect(Page.Locator("#resend-confirmation")).ToContainTextAsync("Resend email confirmation");
		await Expect(Page.Locator("h3")).ToContainTextAsync("Use another service to log in.");
		await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "GitHub" })).ToBeVisibleAsync();
		await Expect(Page.Locator("button[name=\"provider\"]")).ToContainTextAsync("GitHub");
	}

	[Test]
	public async Task ProgramHasCorrectIcon() {
		await Page.GotoAsync("https://bdsa2025gr12chirprazor-gzh9b7ghhxb0cybn.norwayeast-01.azurewebsites.net/");
		await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Wings1Chirp!" })).ToBeVisibleAsync();
		await Expect(Page.GetByRole(AriaRole.Img, new() { Name = "Wings1" })).ToBeVisibleAsync();
		await Expect(Page.Locator("h1")).ToContainTextAsync("Chirp!");
		await Expect(Page.Locator("h1")).ToMatchAriaSnapshotAsync("- heading \"Wings1Chirp!\" [level=1]:\n  - img \"Wings1\"\n  - text: \"\"");

	}
}
