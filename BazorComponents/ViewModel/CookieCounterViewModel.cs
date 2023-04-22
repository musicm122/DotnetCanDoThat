using ClickerGame.ViewModels;
using MvvmBlazor;

namespace BlazorComponents.ViewModel
{
    public partial class CookieCounterViewModel : MvvmBlazor.ViewModel.ViewModelBase
    {
        [Notify] private Guid _id;
        [Notify] private int _maxCookieCooldown = 3;
        [Notify] private string _unitLabel = "Cookie";
        [Notify] private int _currentCookieCooldown;
        [Notify] private int _cookieCount;
        [Notify] private bool _disableClick;
        
        public void DecrementCooldown()
        {
            Console.WriteLine($"DecrementCooldown called with current cooldown = {CurrentCookieCooldown} and disable click ={DisableClick}");
            DisableClick = CurrentCookieCooldown > 0;
            if (!DisableClick) return;
            CurrentCookieCooldown--;

        }
        public void Increment()
        {
            Console.WriteLine("Increment called");
            DisableClick = true;
            CookieCount++;
            CurrentCookieCooldown = _maxCookieCooldown;
            //_currentCookieCooldown = _maxCookieCooldown;
            Console.WriteLine("currentCookieCooldown update to " + CurrentCookieCooldown);
        }
    }
}