using BlazorComponents.ViewModel;
using ClickerGame.Interfaces;

namespace BlazorMvvm.Common.ViewModel
{
    public class HotDogBlazorViewModel : BaseCounterBlazorViewModel
    {
        private HotDogBlazorViewModel(IInventory inventory, string unitLabel) : base(inventory, unitLabel)
        {
        }

        public HotDogBlazorViewModel(IInventory inventory) : this(inventory, "HotDog") { }
    }
}
