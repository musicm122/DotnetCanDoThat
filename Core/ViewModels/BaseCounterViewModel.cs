using System.Net;
using ClickerGame.Interfaces;
using ClickerGame.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClickerGame.ViewModels
{
    public abstract class BaseCounterViewModel : ObservableObject, IFieldCounter
    {
        private IInventory? _inventory;
        public IInventory Inventory 
        {
            get => _inventory;
            set => SetProperty(ref _inventory, value);
        }

        private int _count = 0;

        public int Count 
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }

        private int _currentCookieCooldown = 1;
        public int CurrentCookieCooldown 
        {
            get => _currentCookieCooldown;
            set => SetProperty(ref _currentCookieCooldown, value);
        }

        private bool _disableClick = true;
        public bool DisableClick 
        {
            get => _disableClick;
            set => SetProperty(ref _disableClick, value);
        }
        private Guid _id = new(Guid.NewGuid().ToString());
        public Guid Id { 
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private ItemType? _itemType;
        public ItemType ItemType 
        {
            get => _itemType;
            set => SetProperty(ref _itemType, value);
        }

        public int MaxClickCooldown 
        {
            get => ItemType.WaitTime;
        }

        public string UnitLabel 
        {
            get => ItemType.Name;
        }

        public virtual bool CanClickCookie() =>        
            !DisableClick && CanPayCost(this.ItemType.Name);

        public bool CanPayCost(string typeName) => Inventory.CanPayCostByItemName(typeName);

        public void PayCost(string typeName) => Inventory.PayCostByItemName(typeName);

        public void DecrementCooldown()
        {
            DisableClick = CurrentCookieCooldown > 0;
            Console.WriteLine($"DecrementCooldown called with current cooldown = {CurrentCookieCooldown} and disable click ={DisableClick}");
            if (!DisableClick) return;
            CurrentCookieCooldown--;
        }

        public virtual void Increment(IFieldCounter? optionalVm = null)
        {
            Console.WriteLine("Increment called");
            DisableClick = true;
            Inventory.Increment(ItemType.Name);
            CurrentCookieCooldown = MaxClickCooldown;
            Console.WriteLine("currentCookieCooldown update to " + CurrentCookieCooldown);
        }

        public virtual void Increment()
        {
            Console.WriteLine("Increment called");
            DisableClick = true;
            Inventory.Increment(ItemType.Name);
            CurrentCookieCooldown = MaxClickCooldown;
            Console.WriteLine("currentCookieCooldown update to " + CurrentCookieCooldown);
        }

    }
}
