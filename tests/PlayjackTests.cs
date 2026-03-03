using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PlayjackAutomation.Pages;
using System.Threading.Tasks;

namespace PlayjackAutomation.Tests
{
    [TestFixture]
    public class PlayjackTests : PageTest
    {
        [Test]
        public async Task Registration_And_Bonus_Verification_Test()
        {
            var playjackPage = new PlayjackPage(Page);
            var testEmail = $"qa_user_{System.DateTime.Now.Ticks}@testmail.com";

            // 1. Регистрация
            await playjackPage.RegisterAsync(testEmail, "SecurePass123!");
            
            // 2. Проверка на Бонус История (Optional Task)
            await playjackPage.GoToBonusHistoryAsync();
            
            // Валидация на специфичния бонус от снимката в заданието
            var bonusEntry = Page.Locator("text=Registration - Endless");
            await Expect(bonusEntry).ToBeVisibleAsync();
            
            var amount = Page.Locator("text=5,000");
            await Expect(amount).First.ToBeVisibleAsync();
        }

        [Test]
        public async Task Successful_Login_Test()
        {
            await Page.GotoAsync("https://playjack.com/");
            await Page.ClickAsync("text=Log In");
            await Page.FillAsync("input[name='email']", "existing_user@test.com");
            await Page.FillAsync("input[name='password']", "SecurePass123!");
            await Page.ClickAsync("button[type='submit']");

            await Expect(Page.Locator(".user-balance-area")).ToBeVisibleAsync();
        }
    }
}