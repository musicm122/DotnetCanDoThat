using System.ComponentModel;
using ClickerGame.Services;
using ClickerGame.ViewModels;
using MvvmBlazor;

namespace BlazorComponents.ViewModel
{
    public partial class GameViewModel : VMBase, IDisposable
    {
        private readonly PeriodicTimer _gameTimer = new(TimeSpan.FromSeconds(1));

        [Notify] private Inventory _inventory = new();

        [Notify] private bool _isRunning;
        [Notify] private int _timeElapsed;

        [Notify] public CounterViewModel _hotDogCounter;
        [Notify] public CounterViewModel _cookieCounter;

        public GameViewModel(Inventory inventory)
        {
            _cookieCounter = new CounterViewModel(inventory, "Cookie");
            _hotDogCounter = new CounterViewModel(inventory, "HotDog");
        }

        public void TogglePause() => IsRunning = !IsRunning;

        private void ProcessFrame()
        {
            CookieCounter.DecrementCooldown();
            HotDogCounter.DecrementCooldown();
            TimeElapsed++;
        }

        public override async Task OnInitializedAsync()
        {
            IsRunning = true;
            while (await _gameTimer.WaitForNextTickAsync())
            {
                if (IsRunning)
                {
                    ProcessFrame();
                }
            }

            await base.OnInitializedAsync();
        }

        public void Dispose()
        {
            _gameTimer.Dispose();
        }
    }
}