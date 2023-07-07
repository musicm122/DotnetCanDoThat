using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ClickerGame.Interfaces;
using ClickerGame.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Localization;

namespace ClickerGame.ViewModels
{
    public class GameViewModel : ObservableObject, IGameViewModel
    {
        private bool gameOver = false;
        public bool GameOver 
        {
            get => gameOver;
            set => SetProperty(ref gameOver, value);
        }

        public IInventory Inventory { get; private set; }

        public readonly PeriodicTimer GameTimer = new(TimeSpan.FromSeconds(0.5));
        public IRelayCommand ToggleIsRunningCommand { get; }
        public IRelayCommand ResetGameCommand { get; }


        /// <summary>
        /// Bindable Command for button used to add new ClickCookieCommand 
        /// </summary>
        /// <remarks>
        /// Because RelayCommand has NotifyCanExecuteChanged I use it instead of ICommand
        /// </remarks>
        public IRelayCommand ClickCookieCommand { get => CookieCounter.ClickCommand; }

        /// <summary>
        /// Bindable Command for button used to add new ClickHotdogCommand 
        /// </summary>
        /// <remarks>
        /// Because RelayCommand has NotifyCanExecuteChanged I use it instead of ICommand
        /// </remarks>

        public IRelayCommand ClickHotdogCommand { get; }

       
        private CookieCounterViewModel cookieCounter;

        public CookieCounterViewModel CookieCounter
        {
            get => cookieCounter;
            set => SetProperty(ref cookieCounter, value);
        }

        private HotDogCounterViewModel hotdogCounter;


        public HotDogCounterViewModel HotdogCounter
        {
            get => hotdogCounter;
            set => SetProperty(ref hotdogCounter, value);
        }


        public GameViewModel(IInventory inventory)
        {
            Inventory = inventory;
            CookieCounter = new CookieCounterViewModel(inventory);
            HotdogCounter = new HotDogCounterViewModel(inventory);
            CookieCounter.CountChangedEvent += Counter_CountChangedEvent;
            HotdogCounter.CountChangedEvent += Counter_CountChangedEvent;
            CookieCounter.ClickCommand = new RelayCommand(IncrementCookie, CanClickCookie);
            HotdogCounter.ClickCommand = new RelayCommand(IncrementHotdog, CanClickHotdog);
            ToggleIsRunningCommand = new RelayCommand(ToggleIsRunning);
            ResetGameCommand = new RelayCommand(ResetGame);
            Task.Run(RunGame).ConfigureAwait(false);
        }
       
        public void ResetGame() 
        {
            Inventory.ResetDataStore();
            TimeElapsed = 0;
            IsRunning = true;
            GameOver= false;
            ViewModelPropertyChanged();
            Task.Run(RunGame).ConfigureAwait(false);
        }

        private bool CanClickCookie() => CookieCounter.CanClickCookie();
        private void IncrementCookie()
        {
            CookieCounter.Increment();
        }

        private bool CanClickHotdog() => HotdogCounter.CanClickHotdog();        
        private void IncrementHotdog()
        {
            HotdogCounter.Increment();
            CookieCounter.RefreshCount();
            HotdogCounter.RefreshCount();
        }

        private string title = "Cookie Clicker";
        public string Title
        {
            get => title;
            set
            {
                SetProperty(ref title, value);
                ViewModelPropertyChanged();
            }
        }

        private bool isRunning = false;
        public bool IsRunning
        {
            get => isRunning;
            set
            {
                SetProperty(ref isRunning, value);
                ViewModelPropertyChanged();

            }
        }

        private int timeElapsed = 0;
        public int TimeElapsed
        {
            get => timeElapsed;
            set
            {
                SetProperty(ref timeElapsed, value);
                ViewModelPropertyChanged();
            }
        }

        public void ProcessFrame()
        {
            CookieCounter.ProcessFrame(isRunning);
            HotdogCounter.ProcessFrame(isRunning);
            if (IsRunning)
            {
                TimeElapsed++;
                CheckForWinConditions();
            }
            ViewModelPropertyChanged();
        }

        public void ToggleIsRunning()
        {
            IsRunning = !IsRunning;
        }

        public async Task RunGame()
        {
            IsRunning = true;
            while (IsRunning)
            {
                await GameTimer.WaitForNextTickAsync();
                ProcessFrame();
            }
        }

        public string DumpContents
        {
            get => this.ToString();
        }

        public override string ToString() =>
            new StringBuilder()
            .AppendLine($"GameOver = {this.GameOver.ToString()}")
            .AppendLine($"Is Running = {this.IsRunning}")
            .AppendLine($"Time Elapsed = {this.TimeElapsed}")
            .AppendLine($"Inventory = {this.Inventory.DumpContents()}")
            .AppendLine($"CookieCounter = {this.CookieCounter.DumpContents()}")
            .AppendLine($"HotdogCounter = {this.HotdogCounter.DumpContents()}")
            .ToString();
        


        public bool DoesMeetWinCondition() =>
            HotdogCounter.Count > 3 && CookieCounter.Count > 3;

        public event Action<string,int>? CountChangedEvent;

        public void RaiseCountChangedEvent(string label, int amount)
        {
            CountChangedEvent?.Invoke(label, amount);
            CheckForWinConditions();
        }

        public event Action<string>? GameOverEvent;


        public void RaiseGameOverEvent()
        {
            GameOverEvent?.Invoke($"You have {HotdogCounter.Count} Hot Dogs and {CookieCounter.Count} Cookies! You Win!!!");
        }

        void CheckForWinConditions() 
        {
            if (DoesMeetWinCondition())
            {
                RaiseGameOverEvent();

                IsRunning = false;
                GameOver = true;
                Console.WriteLine("Game Over");
            }
        }
        private void Counter_CountChangedEvent(string arg1, int arg2)
        {
            Console.WriteLine("Counter_CountChangedEvent Called");
            ViewModelPropertyChanged();
            CheckForWinConditions();
        }

        void ViewModelPropertyChanged() 
        {
            OnPropertyChanged(nameof(IsRunning));
            OnPropertyChanged(nameof(TimeElapsed));
            OnPropertyChanged(nameof(DumpContents));
            OnPropertyChanged(nameof(GameOver));
            CookieCounter.RefreshCount();
            HotdogCounter.RefreshCount();
        }
    }
}
