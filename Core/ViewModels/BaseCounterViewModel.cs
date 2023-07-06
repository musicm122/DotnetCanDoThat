using System.Text;
using System.Net;
using ClickerGame.Interfaces;
using ClickerGame.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using ClickerGame.Services;
using CommunityToolkit.Mvvm.Input;

namespace ClickerGame.ViewModels
{
    public abstract class BaseCounterViewModel : ObservableObject, IFieldCounter
    {
        public event Action<string, int>? CountChangedEvent;

        public void RaiseCountChangedEvent(string label, int amount)
        {
            CountChangedEvent?.Invoke(label, amount);
        }

        private string _imageSource = "";

        /// <summary>
        /// Source SVG file for Couter Image
        /// </summary>
        /// <remarks>
        /// Expects source to be svg
        /// </remarks>
        public string ImageSource 
        { 
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        public IRelayCommand ClickCommand { get; set; } 


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
                SetProperty(ref _disableClick, value);
            }
        }
        private readonly Guid _id = new(Guid.NewGuid().ToString());
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
                RaiseCountChangedEvent(ItemType.Name, value);
                OnPropertyChanged(nameof(Count));
            }
        }

        public bool CanPayCost(string typeName) => Inventory.CanPayCostByItemName(typeName);

        public void PayCost(string typeName) => Inventory.PayCostByItemName(typeName);

        public void DecrementCooldown()
        {
            if (CurrentCookieCooldown > 0)
            {
                CurrentCookieCooldown--;
                DisableClick = CurrentCookieCooldown > 0;
            }
            else
            {
                DisableClick = false;
            }
        }

        public virtual void Increment()
        {   
            DisableClick = true;
            Count ++;
            CurrentCookieCooldown = MaxClickCooldown;
        }

        public void ProcessFrame(bool isActive)
        {
            if (isActive)
            {
                DecrementCooldown();
            }
            ClickCommand?.NotifyCanExecuteChanged();
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

        public void RefreshCount() 
        {
            OnPropertyChanged(nameof(Count));
        }
    }
}
