using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ClickerGame.Interfaces
{
    public interface IGameViewModel
    {
        RelayCommand ClickCookieCommand { get; }
        RelayCommand ClickHotdogCommand { get; }
        IInventory Inventory { get; }
        bool IsRunning { get; set; }
        int TimeElapsed { get; set; }
        string Title { get; set; }

        void ProcessFrame();
        Task RunGame();
    }
}