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
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class GameViewModel : ObservableObject, IGameViewModel
    {
        public IInventory Inventory { get; private set; }

        public readonly PeriodicTimer GameTimer = new(TimeSpan.FromSeconds(1));
        public IRelayCommand ToggleIsRunningCommand { get; }

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
            Task.Run(RunGame).ConfigureAwait(false);
        }
       
        public void ResetGame() 
        {
            Inventory.ResetDataStore();
            TimeElapsed = 0;
            IsRunning = true;
            CookieCounter.RefreshCount();
            HotdogCounter.RefreshCount();
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
                OnPropertyChanged(nameof(DumpContents));
                SetProperty(ref title, value);
            }
        }

        private bool isRunning = false;
        public bool IsRunning
        {
            get => isRunning;
            set
            {
                OnPropertyChanged(nameof(TimeElapsed));
                OnPropertyChanged(nameof(DumpContents));

                SetProperty(ref isRunning, value);
            }
        }

        private int timeElapsed = 0;
        public int TimeElapsed
        {
            get => timeElapsed;
            set
            {
                OnPropertyChanged(nameof(IsRunning));
                OnPropertyChanged(nameof(DumpContents));               
                SetProperty(ref timeElapsed, value);
            }
        }

        public void ProcessFrame()
        {
            CookieCounter.ProcessFrame(isRunning);
            HotdogCounter.ProcessFrame(isRunning);
            if (IsRunning)
            {
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
            while (IsRunning)
            {
                await GameTimer.WaitForNextTickAsync();
                ProcessFrame();
            }

            OnPropertyChanged(nameof(DumpContents));
        }

        public string DumpContents
        {
            get => this.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Time Elapsed = {this.TimeElapsed}");
            sb.AppendLine($"Is Running = {this.IsRunning}");

            sb.AppendLine($"Inventory = {this.Inventory.DumpContents()}");
            sb.AppendLine($"CookieCounter = {this.CookieCounter.DumpContents()}");
            sb.AppendLine($"HotdogCounter = {this.HotdogCounter.DumpContents()}");
            return sb.ToString();
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }

        public bool DoesMeetWinCondition() =>
            HotdogCounter.Count > 3 && CookieCounter.Count > 3;

        public event Action<string,int>? CountChangedEvent;

        public void RaiseCountChangedEvent(string label, int amount)
        {
            CountChangedEvent?.Invoke(label, amount);
        }

        public event Action<string>? GameOverEvent;


        public void RaiseGameOverEvent()
        {
            GameOverEvent?.Invoke($"You have {HotdogCounter.Count} Hot Dogs and {CookieCounter.Count} Cookies! You Win!!!");
        }

        private void Counter_CountChangedEvent(string arg1, int arg2)
        {
            Console.WriteLine("Counter_CountChangedEvent Called");
            CookieCounter.RefreshCount();
            HotdogCounter.RefreshCount();
            if (DoesMeetWinCondition())
            {
                IsRunning = false;
                Console.WriteLine("Game Over");
                RaiseGameOverEvent();
            }
        }
    }
}
