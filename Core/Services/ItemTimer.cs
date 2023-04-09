using System.Timers;
using ClickerGame.Models;
using Timer = System.Timers.Timer;

namespace ClickerGame.Services;

public class ItemTimer
{
    public ItemTimer(ItemType definition)
    {
        Definition = definition;
        Timer = new Timer
        {
            AutoReset = true,
            Interval = definition.WaitTime,
            Enabled = true
        };
        Timer.Elapsed += OnCooldownComplete;
    }

    public ItemType Definition { get; set; }
    private bool CooldownComplete { get; set; }

    private Timer Timer { get; }
    public event CooldownEventHandler? RaiseCooldownEvent;

    public bool CanGenerateNewItem(Inventory inventory)
    {
        var canPay = inventory.CanPayCost(Definition.Cost);
        return canPay && CooldownComplete;
    }

    public void ResetTimer()
    {
        Console.WriteLine($"Restarting time for {Definition.Name}");
        CooldownComplete = false;
        Timer.Stop();
        Timer.Start();
    }

    private void OnCooldownComplete(object? sender, ElapsedEventArgs e)
    {
        Timer.Stop();
        Console.WriteLine("OnCooldownComplete");
        RaiseCooldownEvent?.Invoke(sender, new CooldownEventArgs(Definition));
        CooldownComplete = true;
    }
}