using System.Windows.Input;
using ClickerGame.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace UnoClicker.Presentation
{
    public partial class MainViewModel : ObservableObject
    {
        public string? Title { get; }

        [ObservableProperty]
        private string? name;

        public ICommand GoToSecond { get; }

        public ICommand GoToGame { get; }

        public MainViewModel(
            INavigator navigator,
            IStringLocalizer localizer)
        {
            _navigator = navigator;
            Title = $"Main - {localizer["ApplicationName"]}";
            GoToSecond = new AsyncRelayCommand(GoToSecondView);
            GoToGame = new AsyncRelayCommand(GoToGameView);
        }

        private async Task GoToSecondView()
        {
            await _navigator.NavigateViewModelAsync<SecondViewModel>(this, data: new Entity(Name!));
        }

        private async Task GoToGameView()
        {

            await _navigator.NavigateViewModelAsync<GameViewModel>(this);
        }

        private INavigator _navigator;
    }
}