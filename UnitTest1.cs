using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace KlipTok.IntegrationTests
{
	[Parallelizable(ParallelScope.Self)]
	public class Tests : PlaywrightTest
	{
		[Test]
		public async Task VerifyAboutPage()
		{

			await using var browser = await Playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
			{
				Headless = true,
			});
			var context = await browser.NewContextAsync();

			// Open new page
			var page = await context.NewPageAsync();

			// Go to https://kliptok.com/
			await page.GotoAsync("https://kliptok.com/");

			// Click text=About
			await page.ClickAsync("text=About");
			Assert.AreEqual("https://kliptok.com/about", page.Url);

		}

		[Test]
		public async Task VerifyFritzStreamerPage()
		{

			await using var browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
			{
				Headless = false,
			});
			var context = await browser.NewContextAsync();
			// Open new page
			var page = await context.NewPageAsync();
			// Go to https://kliptok.com/
			await page.GotoAsync("https://kliptok.com/");
			// Click [placeholder="Search KlipTok..."]
			await page.ClickAsync("[placeholder=\"Search KlipTok...\"]");
			// Fill [placeholder="Search KlipTok..."]
			await page.FillAsync("[placeholder=\"Search KlipTok...\"]", "csharpfritz");
			// Click :nth-match(:text("csharpfritz"), 2)
			await page.ClickAsync(":nth-match(:text(\"csharpfritz\"), 2)");
			Assert.AreEqual("https://kliptok.com/csharpfritz", page.Url);

		}

	}
}