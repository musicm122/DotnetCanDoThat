using System.Net;
using System.Windows.Input;
using ClickerGame.Interfaces;
using ClickerGame.Models;
using CommunityToolkit.Mvvm.Input;

namespace ClickerGame.ViewModels
{
    public class HotDogCounterViewModel : BaseCounterViewModel
    {
        public const string HotDogImageSource = @"https://www.svgrepo.com/download/475120/hotdog.svg";
        public HotDogCounterViewModel(IInventory inventory)
        {
            Inventory = inventory;
            ItemType = Data.HotDog;
            ImageSource = HotDogImageSource;
            ClickCommand = new RelayCommand(Increment, CanClickHotdog);
        }
        public bool CanClickHotdog()=>
            !DisableClick && CanPayCost(this.ItemType.Name);
        
        public override void Increment()
        {
            if (CanClickHotdog())
            {
                PayCost(this.ItemType.Name);
                base.Increment();
            }
        }
    }
}
