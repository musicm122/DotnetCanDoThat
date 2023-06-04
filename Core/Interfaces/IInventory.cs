using ClickerGame.Models;

namespace ClickerGame.Interfaces;

public interface IInventory
{
    ItemType GetDefinition(string name);
    bool ContainsName(string key);
    void Add(string name, int amount);
    bool CanPayCostByItemName(string itemTypeName);
    bool CanPayCost(ItemCost? cost);
    void PayCostByItemName(string itemTypeName);
    void PayCost(ItemCost? cost);
    int Increment(string fieldName);
    int Amount(string fieldName);
    int Count { get; }
    IEnumerator<KeyValuePair<string, int>> GetEnumerator();
}