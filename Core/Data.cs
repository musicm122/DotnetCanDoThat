namespace Core;

public static class Data
{
    public static ItemType Cookie = new("Cookie", 500f, null);
    public static ItemType HotDog = new("HotDog", 1000f, new ItemCost("Cookie", 2));

    public static ItemType[] Definitions()
    {
        return new[] { Cookie, HotDog };
    }
}