using ClickerGame.Services;

namespace ClickerGame.Interfaces
{
    public interface IGame
    {
        Inventory Inventory { get; set; }
        bool IsRunning { get; set; }
        List<ItemTimer> ItemTimers { get; init; }
        Action? RefreshUi { get; set; }

        void AddItem(string name, int amount);
        string GetCurrentLabel();
        void ProcessFrame();
        void StartGame();
    }
}