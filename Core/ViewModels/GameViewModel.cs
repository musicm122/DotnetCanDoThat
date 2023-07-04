using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ClickerGame.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Localization;

namespace ClickerGame.ViewModels
{
    public class GameViewModel : ObservableObject, IGameViewModel
    {
        public IInventory Inventory { get; private set; }

        public readonly PeriodicTimer GameTimer = new(TimeSpan.FromSeconds(1));
        public ICommand ToggleIsRunningCommand { get; }
        public ICommand ClickCookieCommand { get; }
        public ICommand ClickHotdogCommand { get; }

        private CookieCounterViewModel cookieCounter = new();


        public CookieCounterViewModel CookieCounter 
        { 
            get => cookieCounter; 
            set => SetProperty(ref cookieCounter , value);
        }

        private HotDogCounterViewModel hotdogCounter = new();


        public HotDogCounterViewModel HotdogCounter
        {
            get => hotdogCounter;
            set => SetProperty(ref hotdogCounter, value);
        }

        bool CanClickCookie()
        {
            Console.WriteLine($"CanClickCookie = {CookieCounter.DisableClick}");
            return CookieCounter.DisableClick;
        }

        public GameViewModel(IInventory inventory)
        {
            Inventory = inventory;
            CookieCounter.Inventory = Inventory;
            HotdogCounter.Inventory = Inventory;
            ToggleIsRunningCommand = new RelayCommand(ToggleIsRunning);

            CookieCounter.PropertyChanged += CookieCounter_PropertyChanged;

            ClickCookieCommand =
                new RelayCommand(CookieCounter.Increment, CanClickCookie);

            
            ClickHotdogCommand =
                new RelayCommand(HotdogCounter.Increment, () => HotdogCounter.DisableClick);
            Task.Run(RunGame).ConfigureAwait(false);
        }

        private void CookieCounter_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Console.WriteLine( $"Property Changed: {e.PropertyName}");
        }


        private string title = "Cookie Clicker";
        public string Title
        {
            get => title;
            set => 
                SetProperty(ref title, value);
        }

        private bool isRunning = false;
        public bool IsRunning
        {
            get => isRunning;
            set => SetProperty(ref isRunning, value);
        }

        private int timeElapsed = 0;
        public int TimeElapsed
        {
            get => timeElapsed;
            set => SetProperty(ref timeElapsed, value);
        }

        public void ProcessFrame()
        {
            if (IsRunning)
            {
                CookieCounter.DecrementCooldown();
                HotdogCounter.DecrementCooldown();
                TimeElapsed++;
            }
        }

        public void ToggleIsRunning() 
        {
            IsRunning = !IsRunning;
        }

        public async Task RunGame()
        {
            IsRunning = true;
            //&& IsRunning
            while (await GameTimer.WaitForNextTickAsync() && IsRunning)
            {
                ProcessFrame();
            }
        }

    }
}
