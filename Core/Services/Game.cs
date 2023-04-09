using ClickerGame.Models;

namespace ClickerGame.Services;

public class Game
{
    public Action? RefreshUI { get; set; }

    private ItemTimer InitializeTimer(ItemType itemType)
    {
        var timer = new ItemTimer(itemType);
        timer.RaiseCooldownEvent += (_, _) =>
        {
            RefreshUI?.Invoke();
        };
        return timer;
    }

    public Game()
    {
        ItemTimers =
            new List<ItemTimer>(Data.Definitions().Select(InitializeTimer));
    }

    public Inventory Inventory { get; set; } = new();
    public List<ItemTimer> ItemTimers { get; init; }
    public bool IsRunning { get; set; }

    public void StartGame()
    {
        IsRunning = true;
        //ProcessFrame();
    }

    public void AddItem(string name, int amount)
    {
        if (Inventory.ContainsKey(name))
            Inventory[name] += amount;
        else
            Inventory.Add(name, amount);
    }

    public string GetCurrentLabel()
    {
        var message = string.Empty;
        if (Inventory.Count <= 0) return message;
        foreach (var item in Inventory)
            if (item.Value == 1)
                message += $"You have {item.Value} {item.Key}";
            else
                message += $"You have {item.Value} {item.Key}s ";

        return message;
    }

    public void ProcessFrame()
    {
        while (IsRunning)
            if (Console.Read() != 0)
                IsRunning = false;
        //run game loop 
    }
}