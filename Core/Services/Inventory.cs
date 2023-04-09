using ClickerGame.Models;

namespace ClickerGame.Services;

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
