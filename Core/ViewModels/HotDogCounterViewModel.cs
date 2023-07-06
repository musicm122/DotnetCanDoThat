using System.Net;
using System.Windows.Input;
using ClickerGame.Interfaces;
using ClickerGame.Models;
using CommunityToolkit.Mvvm.Input;

namespace ClickerGame.ViewModels
{
    public class HotDogCounterViewModel : BaseCounterViewModel
    {
        public HotDogCounterViewModel(IInventory inventory)
        {
            Inventory = inventory;
            this.ItemType = Data.HotDog;
            this.ImageSource = @"https://www.svgrepo.com/download/475120/hotdog.svg";
            this.ClickCommand = new RelayCommand(Increment, CanClickCookie);
        }
        bool CanClickCookie() => !DisableClick;

    }
}
