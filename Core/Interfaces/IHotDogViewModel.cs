using ClickerGame.ViewModels;

namespace ClickerGame.Interfaces
{
    public interface IHotDogViewModel
    {
        bool CanClickCookie();
        bool CanPayCost(CookieCounterViewModel cookieVM);
        void Increment(CookieCounterViewModel cookieVM);
    }
}