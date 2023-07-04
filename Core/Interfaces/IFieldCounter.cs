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

        int Count { get;set;}

        int CurrentCookieCooldown { get; set; }
        bool DisableClick { get; set; }
        Guid Id { get;  }
        ItemType ItemType { get; set; }

        bool CanPayCost(string typeName);
    }
}