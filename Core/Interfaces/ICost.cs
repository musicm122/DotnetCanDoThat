using ClickerGame.Services;

namespace ClickerGame.Interfaces
{
    public interface ICost
    {
        protected IInventory Inventory { get; set; }
        public bool CanPayCost(string typeName) => Inventory.CanPayCostByItemName(typeName);

        public void PayCost(string typeName) => Inventory.PayCostByItemName(typeName);

    }
}