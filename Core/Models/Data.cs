namespace ClickerGame.Models;

public static class Data
{
    public static ItemType Cookie = new("Cookie", 3f, null);
    public static ItemType HotDog = new("HotDog", 5f, new ItemCost("Cookie", 2));

    public static ItemType[] Definitions()
    {
        return new[] { Cookie, HotDog };
    }

    public static Dictionary<string, ItemType> ItemDefinitions =
        new()
        {
            { "HotDog", HotDog },
            { "Cookie", Cookie }
        };
}