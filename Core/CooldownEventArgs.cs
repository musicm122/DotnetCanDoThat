namespace ClickerGame;

public delegate void CooldownEventHandler(object? sender, CooldownEventArgs args);

public class CooldownEventArgs : EventArgs
{
    public CooldownEventArgs(ItemType item)
    {
        SourceItem = item;
    }

    public ItemType SourceItem { get; }
}