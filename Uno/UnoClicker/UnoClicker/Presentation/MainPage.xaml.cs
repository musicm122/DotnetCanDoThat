using ClickerGame.Interfaces;
using ClickerGame.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;
using UnoClicker.Presentation.Controls;

namespace UnoClicker.Presentation
{
    public sealed partial class MainPage : Page
    {
        private IInventory? Inventory { get; set; }
        private INavigator? Navigation { get; set; }
        private App? CurrentApp { get; set; }
        public GameViewModel? ViewModel { get;set; }

        public MainPage()
        {
            Console.WriteLine("MainPage Constructor Called");

            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;

            CurrentApp = (App)Application.Current;
            if (CurrentApp != null)
            {
                Navigation = CurrentApp.Get<INavigator>();
                Inventory = CurrentApp.Get<IInventory>();
                if (Inventory != null)
                {
                    ViewModel = CurrentApp.Get<GameViewModel>() ?? new GameViewModel(Inventory);
                    ViewModel.GameOverEvent += ViewModel_GameOverEvent;

                }
            }
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.GameOverEvent += ViewModel_GameOverEvent;
            }
        }
     
        public async Task ShowWinMessage(INavigator navigator)
        {
            var reset = "Reset";
            var quit = "Quit";

            var result =
                await navigator.ShowMessageDialogAsync<string>(this,
                title: "Game Over",
                content: "You Win!!! Start Over?",
                buttons: new[]{
                    new DialogAction(reset),
                    new DialogAction(quit)
                });

            if (result == reset)
            {
                Console.WriteLine("Resetting Game.");
                ViewModel?.ResetGame();
            }
            else if (result == quit)
            {
                Console.WriteLine("Thanks for Playing!");
                Application.Current.Exit();
            }
        }

        private void ViewModel_GameOverEvent(string obj)
        {
            if (Navigation != null)
            {
                Task.Run(async () =>
                {
                    await ShowWinMessage(Navigation);
                });
            }
                        
        }
    }
}