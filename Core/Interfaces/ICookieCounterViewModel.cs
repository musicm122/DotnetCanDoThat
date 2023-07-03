namespace ClickerGame.Interfaces
{
    public interface ICookieCounterViewModel
    {
        int CookieCount { get; set; }
        int CurrentCookieCooldown { get; set; }
        int MaxCookieCooldown { get; }
        string UnitLabel { get; set; }

        bool CanClickCookie();
        void Increment();
    }
}