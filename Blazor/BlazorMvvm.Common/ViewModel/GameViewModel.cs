using System.ComponentModel;
using BlazorMvvm.Common.ViewModel;
using BlazorMvvm.Common.ViewModel.Common;
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

        [Notify] private IInventory _inventory;
        [Notify] private bool _isRunning;
        [Notify] private int _timeElapsed;

		[Notify] public HotDogBlazorViewModel _hotDogCounter;
        [Notify] public CookieCounterBlazorViewModel _cookieCounter;

        public GameViewModel(Inventory inventory, CookieCounterBlazorViewModel cookieCounterVm, HotDogBlazorViewModel hotDogVm)
        {
            _inventory = inventory;
            _cookieCounter = cookieCounterVm;
            _hotDogCounter = hotDogVm;
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