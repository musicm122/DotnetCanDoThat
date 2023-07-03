using System.Windows.Input;

namespace ClickerGame.Interfaces
{
    public interface IGameViewModel
    {
        ICommand ClickCookieCommand { get; }
        ICommand ClickHotdogCommand { get; }
        IInventory Inventory { get; }
        bool IsRunning { get; set; }
        int TimeElapsed { get; set; }
        string Title { get; set; }

        void ProcessFrame();
        Task RunGame();
    }
}