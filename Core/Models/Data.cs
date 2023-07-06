using ClickerGame.Constants;

namespace ClickerGame.Models;


public static class Data
{
    
    public static ItemType Cookie = new(ItemName.Cookie, 1, null);
    public static ItemType HotDog = new(ItemName.HotDog, 3, new ItemCost(ItemName.Cookie, 2));

    public static ItemType[] Definitions()
    {
        return new[] { Cookie, HotDog };
    }
    
    public static bool HasDefinitions(string name) =>ItemDefinitions.ContainsKey(name);

    public static readonly Dictionary<string, ItemType> ItemDefinitions =
        new()
        {
            { ItemName.HotDog, HotDog },
            { ItemName.Cookie, Cookie }
        };
}