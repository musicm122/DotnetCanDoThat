using ClickerGame.ViewModels;

namespace BlazorComponents.ViewModel
{
    public class GameViewModel : VMBase, IDisposable
    {
        private readonly PeriodicTimer _gameTimer = new(TimeSpan.FromSeconds(1));

        public readonly CookieCounterViewModel CookieCounter = new();

        private bool _isRunning;

        public bool IsRunning
        {
            get => _isRunning;
            private set => Set(ref _isRunning, value);
        }

        private int _timeElapsed;

        public int TimeElapsed
        {
            get => _timeElapsed;
            set => Set(ref _timeElapsed, value);
        }

        public void ClickCookie()
        {
            CookieCounter.CookieCount++;
        }

        public void ProcessFrame()
        {
            Console.WriteLine($"CookieCounter.CurrentCookieCooldown = {CookieCounter.CurrentCookieCooldown}");
            if (CookieCounter.CurrentCookieCooldown > 0)
            {
                CookieCounter.CurrentCookieCooldown--;
            }

            TimeElapsed++;
        }

        

        public void TogglePause() => IsRunning = !IsRunning;

        public override async Task OnInitializedAsync()
        {
            IsRunning = true;
            while (await _gameTimer.WaitForNextTickAsync())
            {
                if (IsRunning)
                {
                    ProcessFrame();
                }
                //await InvokeAsync(StateHasChanged);
            }

            await base.OnInitializedAsync();
        }

        public void Dispose()
        {
            _gameTimer.Dispose();
        }
    }
}