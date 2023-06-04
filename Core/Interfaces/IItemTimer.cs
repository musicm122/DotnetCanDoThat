using ClickerGame.Models;
using ClickerGame.Services;

namespace ClickerGame.Interfaces
{
    public interface IItemTimer
    {
        ItemType Definition { get; set; }

        event CooldownEventHandler? RaiseCooldownEvent;

        bool CanGenerateNewItem(Inventory inventory);
        void ResetTimer();
    }
}