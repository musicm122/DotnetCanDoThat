using ClickerGame.ViewModels;

namespace BlazorComponents.ViewModel
{
    public class CookieCounterViewModel : MvvmBlazor.ViewModel.ViewModelBase
    {
        protected int maxCookieCooldown = 10;
        protected string unitLabel = "Cookie";

        public CookieCounterViewModel()
        {
            _currentCookieCooldown = maxCookieCooldown;
            //MaxCookieCooldown = 10;
            //UnitLabel = unitLabel;
        }

        public int MaxCookieCooldown
        {
            get => maxCookieCooldown;
            set => Set(ref maxCookieCooldown, value);
        }

        private int _currentCookieCooldown = 0;

        public int CurrentCookieCooldown
        {
            get => _currentCookieCooldown;
            set
            {
                Console.WriteLine($"CurrentCookieCooldown Setter called with value = {value}");
                Set(ref _currentCookieCooldown, value);
                DisableClick = _currentCookieCooldown == 0;
                Console.WriteLine($"DisableClick set to {DisableClick}");

            }
        }

        private int _cookieCount = 0;

        public int CookieCount
        {
            get => _cookieCount;
            set => Set(ref _cookieCount, value);
        }

        public string UnitLabel
        {
            get => unitLabel;
            set => Set(ref unitLabel, value);
        }

        private bool _diableClick = false;

        public bool DisableClick
        {
            get => _diableClick;
            set => Set(ref _diableClick, value);
        }


        public void Increment()
        {
            Console.WriteLine("Increment called");
            CookieCount++;
            this._currentCookieCooldown = MaxCookieCooldown;
            this.CurrentCookieCooldown = MaxCookieCooldown;
            Set(ref _currentCookieCooldown, MaxCookieCooldown);
            
            Console.WriteLine("currentCookieCooldown update to " + CurrentCookieCooldown);
        }
    }
}