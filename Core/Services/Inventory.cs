using System.Collections;
using ClickerGame.Interfaces;
using ClickerGame.Models;

namespace ClickerGame.Services;

public class Inventory : IEnumerable<KeyValuePair<string, int>>, IInventory
{
    public ItemType GetDefinition(string name)
    {
        if (!Data.HasDefinitions(name)) throw new KeyNotFoundException($"Unable to find Item Definition for {name}");
        return Data.ItemDefinitions[name];
    }

    private Dictionary<string, int> DataStore { get; set; } = new();

    public bool ContainsName(string key) =>
        DataStore.ContainsKey(key);

    public void Add(string name, int amount)
    {
        if (ContainsName(name))
        {
            DataStore[name] += amount;
        }
        else
        {
            DataStore.Add(name, amount);
        }
    }

    public Inventory(Dictionary<string, int>? initialDataStore)
    {
        var dataStore = initialDataStore;
        if (dataStore != null)
            foreach (var itemType in Data.Definitions())
            {
                dataStore.Add(itemType.Name, 0);
            }
    }

    public Inventory()
    {
        foreach (var itemType in Data.Definitions())
        {
            DataStore.Add(itemType.Name, 0);
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
        var amt = cost.Amt;
        return DataStore.ContainsKey(cost.Name) && DataStore[cost.Name] >= amt;
    }

    public void PayCostByItemName(string itemTypeName)
    {
        var cost = Data.ItemDefinitions[itemTypeName].Cost;
        if (cost != null) PayCost(cost);
    }

    public void PayCost(ItemCost? cost)
    {
        if (cost != null)
        {
            DataStore[cost.Name] -= cost.Amt;
        }
    }

    public int Increment(string fieldName)
    {
        DataStore[fieldName] += 1;
        return DataStore[fieldName];
    }

    public int Amount(string fieldName) => DataStore[fieldName];

    public int Count => DataStore.Count;

    public IEnumerator<KeyValuePair<string, int>> GetEnumerator()
    {
        foreach (var keyValuePair in DataStore)
        {
            yield return keyValuePair;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}