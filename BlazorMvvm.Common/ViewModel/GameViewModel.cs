using System.ComponentModel;
using ClickerGame.Constants;
using ClickerGame.Interfaces;
using ClickerGame.Services;
using ClickerGame.ViewModels;
using MvvmBlazor;

namespace BlazorComponents.ViewModel
{
    public partial class GameViewModel : BlazorVMBase, IDisposable
    {
        private readonly PeriodicTimer _gameTimer = new(TimeSpan.FromSeconds(1));

        [Notify] private IInventory _inventory = new Inventory();

        [Notify] private bool _isRunning;
        [Notify] private int _timeElapsed;

		[Notify] public IFieldCounter _hotDogCounter;
        [Notify] public IFieldCounter _cookieCounter;

        public GameViewModel(Inventory inventory)
        {
            _cookieCounter = new CounterViewModel(inventory, ItemName.Cookie);
            _hotDogCounter = new CounterViewModel(inventory, ItemName.HotDog);
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