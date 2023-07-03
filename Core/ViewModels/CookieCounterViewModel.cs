using ClickerGame.Models;
using Microsoft.Extensions.Localization;

namespace ClickerGame.ViewModels
{

    public class CookieCounterViewModel : BaseCounterViewModel
    {
        public CookieCounterViewModel()
        {
            this.ItemType = Data.Cookie;
        }        
    }
}
