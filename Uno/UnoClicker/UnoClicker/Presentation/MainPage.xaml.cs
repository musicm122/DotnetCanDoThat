using ClickerGame.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;
using UnoClicker.Presentation.Controls;

namespace UnoClicker.Presentation
{
    public sealed partial class MainPage : Page
    {
        //public Clicker CookieClicker { get; set; } = new Clicker();
        
        //public Clicker HotDogClicker { get; set; } = new Clicker();

        public MainPage()
        {
            this.InitializeComponent();
            if (this.DataContext != null)
            {
                //var vm = (GameViewModel)this.DataContext;
                //this.CookieClicker.ViewModel = vm.CookieCounter;
                //this.HotDogClicker.ViewModel = vm.HotdogCounter;
            }
        }
    }
}