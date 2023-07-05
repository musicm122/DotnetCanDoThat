using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class GameViewModel : ObservableObject, IGameViewModel
    {
        public IInventory Inventory { get; private set; }

        public readonly PeriodicTimer GameTimer = new(TimeSpan.FromSeconds(1));
        public ICommand ToggleIsRunningCommand { get; }

        /// <summary>
        /// Bindable Command for button used to add new ClickCookieCommand 
        /// </summary>
        /// <remarks>
        /// Because RelayCommand has NotifyCanExecuteChanged I use it instead of ICommand
        /// </remarks>
        public RelayCommand ClickCookieCommand { get; }

        /// <summary>
        /// Bindable Command for button used to add new ClickHotdogCommand 
        /// </summary>
        /// <remarks>
        /// Because RelayCommand has NotifyCanExecuteChanged I use it instead of ICommand
        /// </remarks>

        public RelayCommand ClickHotdogCommand { get; }

       
        private CookieCounterViewModel cookieCounter = new();

        public CookieCounterViewModel CookieCounter
        {
            get => cookieCounter;
            set => SetProperty(ref cookieCounter, value);
        }

        private HotDogCounterViewModel hotdogCounter = new();


        public HotDogCounterViewModel HotdogCounter
        {
            get => hotdogCounter;
            set => SetProperty(ref hotdogCounter, value);
        }

        bool CanClickCookie()
        {
            Console.WriteLine($"CanClickCookie = {!CookieCounter.DisableClick}");
            return !CookieCounter.DisableClick;
        }

        public GameViewModel(IInventory inventory)
        {
            Inventory = inventory;
            CookieCounter.Inventory = Inventory;
            HotdogCounter.Inventory = Inventory;

            ToggleIsRunningCommand = new RelayCommand(ToggleIsRunning);

            ClickCookieCommand = 
                new RelayCommand(CookieCounter.Increment, CanClickCookie);
            
            ClickHotdogCommand =
                new RelayCommand(HotdogCounter.Increment, () => HotdogCounter.DisableClick);

            Task.Run(RunGame).ConfigureAwait(false);
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
                ClickCookieCommand.NotifyCanExecuteChanged();

                HotdogCounter.DecrementCooldown();
                OnPropertyChanged(nameof(HotdogCounter));
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
            while (await GameTimer.WaitForNextTickAsync() && IsRunning)
            {
                ProcessFrame();
            }
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
    }
}
