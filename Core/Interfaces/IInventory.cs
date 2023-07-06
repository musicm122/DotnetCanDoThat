using System.Text;
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
    int SetAmount(string fieldName, int amount);

    int Amount(string fieldName);
    int Count { get; }
    void ResetDataStore();

    IEnumerator<KeyValuePair<string, int>> GetEnumerator();
    string DumpContents() 
    {
        var sb = new StringBuilder();
        sb.AppendLine($"===========Start Of Inventory Contents================");

        foreach(var item in this) {
            sb.AppendLine($"Key:{item.Key} || Value:{item.Value}");
        }
        sb.AppendLine($"===========End of Inventory Contents==================");

        return sb.ToString();
    }
}