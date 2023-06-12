using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BlazorComponents.ViewModel;
using ClickerGame.Interfaces;

namespace BlazorMvvm.Common.ViewModel
{
    public class CookieCounterBlazorViewModel : BaseCounterBlazorViewModel
    {        
        private CookieCounterBlazorViewModel(IInventory inventory, string unitLabel) : base(inventory, unitLabel)
        {
        }

        public CookieCounterBlazorViewModel(IInventory inventory):this(inventory,"Cookie")
        {
        }
    }
}
