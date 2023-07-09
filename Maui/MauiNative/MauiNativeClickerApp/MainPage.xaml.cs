using ClickerGame.Interfaces;
using ClickerGame.ViewModels;
using System.Diagnostics;

namespace MauiNativeClickerApp
{
	public partial class MainPage : ContentPage
	{
        public GameViewModel ViewModel { get; set; }

        //int count = 0;

        public MainPage(GameViewModel viewModel)
        {
            ViewModel = viewModel;
            ViewModel.GameOverEvent += ViewModel_GameOverEvent;

            BindingContext = ViewModel;
            InitializeComponent();
		}

        private async void ViewModel_GameOverEvent(string obj)
        {
            Console.WriteLine("Game Over");
            Console.WriteLine($"Result:{obj}");

            bool shouldReset= 
                await DisplayAlert("Game Over", "You Win!!! Start Over?", "Reset", "Quit");
            
            if (shouldReset)
            {
                ViewModel.ResetGame();
            }
            else
            {
                Application.Current.Quit();
            }
        }

        //private void OnCounterClicked(object sender, EventArgs e)
        //{
        //	count++;

        //	if (count == 1)
        //		CounterBtn.Text = $"Clicked {count} time";
        //	else
        //		CounterBtn.Text = $"Clicked {count} times";

        //	SemanticScreenReader.Announce(CounterBtn.Text);
        //}
    }
}