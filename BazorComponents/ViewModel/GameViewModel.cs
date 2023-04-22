using System.ComponentModel;
using ClickerGame.ViewModels;
using MvvmBlazor;

namespace BlazorComponents.ViewModel
{
    public partial class GameViewModel : VMBase, IDisposable
    {
        private readonly PeriodicTimer _gameTimer = new(TimeSpan.FromSeconds(1));
        public readonly CookieCounterViewModel CookieCounter = new();
        
        [Notify] private bool _isRunning;
        [Notify] private int _timeElapsed;
        public void TogglePause() => IsRunning = !IsRunning;

        public void ProcessFrame()
        {
            Console.WriteLine($"CookieCounter.CurrentCookieCooldown = {CookieCounter.CurrentCookieCooldown}");
            CookieCounter.DecrementCooldown();
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