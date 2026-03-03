using Microsoft.Playwright;
using System.Threading.Tasks;

namespace PlayjackAutomation.Pages
{
    public class PlayjackPage
    {
        private readonly IPage _page;

        public PlayjackPage(IPage page) => _page = page;

        public async Task RegisterAsync(string email, string password)
        {
            await _page.GotoAsync("https://playjack.com/");
            await _page.ClickAsync("text=Sign Up");
            await _page.FillAsync("input[name='email']", email);
            await _page.FillAsync("input[name='password']", password);
            await _page.CheckAsync("input[type='checkbox']");
            await _page.ClickAsync("button[type='submit']");
        }

        public async Task GoToBonusHistoryAsync()
        {
            await _page.GotoAsync("https://playjack.com/my-account/account-history");
            await _page.ClickAsync("text=HISTORY");
            await _page.ClickAsync("text=BONUS");
        }
    }
}