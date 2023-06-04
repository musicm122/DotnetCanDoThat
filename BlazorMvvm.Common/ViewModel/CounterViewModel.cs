using ClickerGame.Interfaces;
using ClickerGame.Models;
using ClickerGame.Services;
using ClickerGame.ViewModels;
using Microsoft.Extensions.Logging;
using MvvmBlazor;

namespace BlazorComponents.ViewModel
{
    public partial class CounterViewModel : BlazorVMBase, IFieldCounter
    {

        [Notify] private ItemType _itemType;
		[Notify] private Guid _id;
		[Notify] private int _maxCookieCooldown;
		[Notify] private string _unitLabel = "NoName";
		[Notify] private int _currentCookieCooldown;
		[Notify] private bool _disableClick;
		private IInventory _inventory;

		public CounterViewModel(IInventory inventory, string unitLabel)
		{
			_inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
			UnitLabel = unitLabel ?? throw new ArgumentNullException(nameof(unitLabel));
			ItemType = _inventory.GetDefinition(unitLabel);
			MaxCookieCooldown = (int)ItemType.WaitTime;
		}


		public IInventory Inventory 
		{ 
			get => _inventory; 
			set =>
				Set(ref _inventory, value, "Inventory");
		}


		public int Count
        {
            get => _inventory.Amount(_unitLabel);
            set
            {
                var previousCount = _inventory.Amount(_unitLabel);
                Set(ref previousCount, value, "Count");
            }
        }
    }
}