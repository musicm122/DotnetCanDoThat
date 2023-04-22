using ClickerGame.Models;

namespace ClickerGame.Services;

public class Inventory : Dictionary<string, int>
{
    public Inventory()
    {
        foreach (var itemType in Data.Definitions())
        {
            Add(itemType.Name, 0);
        }
    }

    //private bool HasCost => Definition.Cost != null;
    //private int CostAmount => Definition.Cost?.Amt ?? 0;
    public bool CanPayCostByItemName(string itemTypeName)
    {
        var cost = Data.ItemDefinitions[itemTypeName].Cost;
        return CanPayCost(cost);
    }
    
    public bool CanPayCost(ItemCost? cost)
    {
        if (cost == null) return true;
        var amt = cost?.Amt ?? 0;
        return ContainsKey(cost!.Name) && this[cost.Name] >= amt;
    }
    public void PayCostByItemName(string itemTypeName)
    {
        var cost = Data.ItemDefinitions[itemTypeName].Cost;
        if (cost != null) PayCost(cost);
    }


    public void PayCost(ItemCost cost)
    {
        if (cost == null) return;
        this[cost.Name] -= cost.Amt;
    }
}
