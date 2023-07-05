using System.Text;
using System.Net;
using ClickerGame.Interfaces;
using ClickerGame.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using ClickerGame.Services;

namespace ClickerGame.ViewModels
{
    public abstract class BaseCounterViewModel : ObservableObject, IFieldCounter
    {
        private IInventory _inventory;
        public IInventory Inventory
        {
            get => _inventory;
            set => SetProperty(ref _inventory, value);
        }

        private int _currentCookieCooldown = 0;
        public int CurrentCookieCooldown 
        {
            get => _currentCookieCooldown;

            set
            {
                if (_currentCookieCooldown != value)
                {
                    Console.WriteLine($"CurrentCookieCooldown changed to {value}");
                }

                SetProperty(ref _currentCookieCooldown, value);
                DisableClick = (_currentCookieCooldown > 0);
            }                
        }

        private bool _disableClick = true;
        public bool DisableClick 
        {
            get => _disableClick;
            set
            {
                if (_disableClick != value)
                {
                    Console.WriteLine($"DisableClick changed to {value}");
                }
                SetProperty(ref _disableClick, value);
            }
        }
        private Guid _id = new(Guid.NewGuid().ToString());
        public Guid Id { 
            get => _id;
        }

        private ItemType _itemType;
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

        public int Count 
        {
            get => 
                Inventory.Amount(ItemType.Name);
            set {
                _ = Inventory.SetAmount(ItemType.Name, value);
                OnPropertyChanged(nameof(Count));
            }
        }

        public bool CanPayCost(string typeName) => Inventory.CanPayCostByItemName(typeName);

        public void PayCost(string typeName) => Inventory.PayCostByItemName(typeName);

        public void DecrementCooldown()
        {
            if (CurrentCookieCooldown > 0)
            {
                //Console.WriteLine($"DecrementCooldown called with current cooldown = {CurrentCookieCooldown} and disable click ={DisableClick}");
                CurrentCookieCooldown--;
                DisableClick = CurrentCookieCooldown > 0;
            }
            else
            {
                DisableClick = false;
            }
        }

        public virtual void Increment(IFieldCounter? optionalVm = null)
        {
            //Console.WriteLine("Increment called");
            DisableClick = true;
            Count++;
            CurrentCookieCooldown = MaxClickCooldown;
            Console.WriteLine($"Count update to {Count}");
            //Console.WriteLine("currentCookieCooldown update to " + CurrentCookieCooldown);
        }

        public virtual void Increment()
        {
            //Console.WriteLine("Increment called");
            DisableClick = true;
            Count ++;
            CurrentCookieCooldown = MaxClickCooldown;
            Console.WriteLine($"Count update to {Count}");
            //Console.WriteLine("currentCookieCooldown update to " + CurrentCookieCooldown);
        }


        public string DumpContents()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"inventory = {_inventory.DumpContents()}");
            sb.AppendLine($"currentCookieCooldown = {_currentCookieCooldown}");
            sb.AppendLine($"disableClick = {_disableClick}");
            sb.AppendLine($"id = {_id}");
            sb.AppendLine($"itemType = {_itemType}");
            return sb.ToString();
        }
    }
}
