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
        private UnoClicker.App CurrentApp { get; set; }

        public MainPage()
        {
            Console.WriteLine("MainPage Constructor Called");

            this.InitializeComponent();
            CurrentApp = (App)Application.Current;
            Navigation = CurrentApp.Get<INavigator>();
            Inventory = CurrentApp.Get<IInventory>();
            //ViewModel.GameOverEvent += ViewModel_GameOverEvent;
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Console.WriteLine("OnNavigatedTo Called");
            ViewModel.GameOverEvent += ViewModel_GameOverEvent;
        }
      
        public GameViewModel ViewModel 
        {
            get
            {
                //ViewModel.GameOverEvent += ViewModel_GameOverEvent;
                return (GameViewModel)DataContext;
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
                ViewModel.ResetGame();
            }
            else if (result == quit)
            {
                Console.WriteLine("Thanks for Playing!");
                Application.Current.Exit();
            }
        }

        private void ViewModel_GameOverEvent(string obj)
        {
            var currentApp = (UnoClicker.App)Application.Current;
            var navigator = currentApp?.Get<INavigator>();
            if (navigator != null)
            {
                Task.Run(async () =>
                {
                    await ShowWinMessage(navigator);
                });
            }            
        }
    }
}