using System.Timers;
using Timer = System.Timers.Timer;

namespace Core;

public class Inventory : Dictionary<string, int>
{
    //private bool HasCost => Definition.Cost != null;
    //private int CostAmount => Definition.Cost?.Amt ?? 0;
    public bool CanPayCost(ItemCost? cost)
    {
        if (cost == null) return true;
        var amt = cost?.Amt ?? 0;
        return ContainsKey(cost!.Name) && this[cost.Name] >= amt;
    }

    public void PayCost(ItemCost cost)
    {
        if (cost == null) return;
        this[cost.Name] -= cost.Amt;
    }
}

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
        return inventory.CanPayCost(Definition.Cost) && CooldownComplete;
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