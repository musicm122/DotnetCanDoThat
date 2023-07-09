using ClickerGame.Interfaces;
using ClickerGame.Models;
using ClickerGame.Services;

namespace MauiMvvm.Common
{
    // All the code in this file is included in all platforms.
    public class CounterViewModel : VMBase, IFieldCounter
	{
		private ItemType _itemType;
		private Guid _id;
		private int _maxCookieCooldown;
		private string _unitLabel;
		private int _currentCookieCooldown;
		private bool _disableClick;
		private IInventory _inventory;

		public CounterViewModel(IInventory inventory, string unitLabel)
		{
			_inventory = inventory;
			UnitLabel = unitLabel;
			ItemType = Data.ItemDefinitions[unitLabel];
			MaxCookieCooldown = (int)ItemType.WaitTime;
		}
		
		public IInventory Inventory
		{
			get => _inventory;
			set => SetProperty(ref _inventory, value, "Inventory");
		}

		public int Count
		{
			get => Inventory.Amount(_unitLabel);
			set
			{
				var previousCount = Inventory.Amount(_unitLabel);
				SetProperty(ref previousCount, value, "Count");
			}
		}

		public ItemType ItemType
		{
			get => _itemType;
			set => SetProperty(ref _itemType, value, "ItemType");
		}
		public Guid Id
		{
			get => _id;
			set => SetProperty(ref _id, value, "Id");
		}
		public int MaxCookieCooldown
		{
			get => _maxCookieCooldown;
			set => SetProperty(ref _maxCookieCooldown, value, "MaxCookieCooldown");
		}
		public string UnitLabel
		{
			get => _unitLabel;
			set => SetProperty(ref _unitLabel, value, "UnitLabel");
		}
		public int CurrentCookieCooldown
		{
			get => _currentCookieCooldown;
			set => SetProperty(ref _currentCookieCooldown, value, "CurrentCookieCooldown");
		}
		public bool DisableClick
		{
			get => _disableClick;
			set => SetProperty(ref _disableClick, value, "DisableClick");
		}

        public bool CanPayCost(string typeName)=> Inventory.CanPayCostByItemName(typeName);
    }
}