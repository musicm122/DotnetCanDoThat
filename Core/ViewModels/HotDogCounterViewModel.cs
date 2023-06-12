using System.Net;
using System.Windows.Input;
using ClickerGame.Interfaces;
using ClickerGame.Models;
using CommunityToolkit.Mvvm.Input;

namespace ClickerGame.ViewModels
{
    public class HotDogCounterViewModel : BaseCounterViewModel
    {
        public HotDogCounterViewModel()
        {
            this.ItemType = Data.HotDog;
        }
    }
}
