using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClickerGame.Models;
using ClickerGame.Services;
using Microsoft.Extensions.Logging;

namespace ClickerGame.Interfaces
{
    public interface IFieldCounter
    {
        IInventory Inventory { get; set; }

        int Count { get; set; }

        int CurrentCookieCooldown { get; set; }
        bool DisableClick { get; set; }
        Guid Id { get; set; }
        ItemType ItemType { get; set; }
        int MaxCookieCooldown { get; set; }
        string UnitLabel { get; set; }

		public bool CanPayCost(string typeName) => Inventory.CanPayCostByItemName(typeName);

		public void PayCost(string typeName) => Inventory.PayCostByItemName(typeName);

		public void DecrementCooldown()
        {
            Console.WriteLine($"DecrementCooldown called with current cooldown = {CurrentCookieCooldown} and disable click ={DisableClick}");
            DisableClick = CurrentCookieCooldown > 0;
            if (!DisableClick) return;
            CurrentCookieCooldown--;
        }

		public void Increment(IFieldCounter? optionalVm = null)
        {
            Console.WriteLine("Increment called");
            DisableClick = true;
            Inventory.Increment(ItemType.Name);
            CurrentCookieCooldown = MaxCookieCooldown;
            Console.WriteLine("currentCookieCooldown update to " + CurrentCookieCooldown);
        }

		public void Increment()
		{
			Console.WriteLine("Increment called");
			DisableClick = true;
			Inventory.Increment(ItemType.Name);
			CurrentCookieCooldown = MaxCookieCooldown;
			Console.WriteLine("currentCookieCooldown update to " + CurrentCookieCooldown);
		}
	}
}