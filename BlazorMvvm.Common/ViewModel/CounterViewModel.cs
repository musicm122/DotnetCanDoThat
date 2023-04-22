using ClickerGame.Models;
using ClickerGame.Services;
using ClickerGame.ViewModels;
using MvvmBlazor;

namespace BlazorComponents.ViewModel
{
    public partial class CounterViewModel : VMBase
    {
        [Notify] private ItemType _itemType;
        private Inventory _inventory;

        public int Count
        {
            get => _inventory[_unitLabel];
            set
            {
                var previousCount = _inventory[_unitLabel];
                Set(ref previousCount, value, "Count");
            }
        }

        [Notify] private Guid _id;
        [Notify] private int _maxCookieCooldown;
        [Notify] private string _unitLabel;
        [Notify] private int _currentCookieCooldown;
        [Notify] private bool _disableClick;

        public CounterViewModel(Inventory inventory, string unitLabel)
        {
            _inventory = inventory;
            UnitLabel = unitLabel;
            ItemType = Data.ItemDefinitions[unitLabel];
            MaxCookieCooldown = (int)ItemType.WaitTime;
        }

        private bool CanPayCost(string typeName) =>
            _inventory.CanPayCostByItemName(typeName);

        private void PayCost(string typeName) =>
            _inventory.PayCostByItemName(typeName);


        public void Increment(CounterViewModel vm = null)
        {
            if (vm != null)
            {
                if (!CanPayCost(vm.ItemType.Name)) return;
                PayCost(vm.ItemType.Name);
            }

            Increment();
        }

        public void DecrementCooldown()
        {
            Console.WriteLine(
                $"DecrementCooldown called with current cooldown = {CurrentCookieCooldown} and disable click ={DisableClick}");
            DisableClick = CurrentCookieCooldown > 0;
            if (!DisableClick) return;
            CurrentCookieCooldown--;
        }

        private void Increment()
        {
            Console.WriteLine("Increment called");
            DisableClick = true;
            _inventory[ItemType.Name] += 1;
            CurrentCookieCooldown = MaxCookieCooldown;
            Console.WriteLine("currentCookieCooldown update to " + CurrentCookieCooldown);
        }
    }
}