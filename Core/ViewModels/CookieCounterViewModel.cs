﻿using ClickerGame.Interfaces;
using ClickerGame.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Localization;
using System.Net;
using static System.Net.WebRequestMethods;

namespace ClickerGame.ViewModels
{

    public class CookieCounterViewModel : BaseCounterViewModel
    {
        public CookieCounterViewModel(IInventory inventory)
        {
            Inventory = inventory;
            this.ImageSource = @"https://www.svgrepo.com/download/30963/cookie.svg";
            this.ItemType = Data.Cookie;
            this.ClickCommand = new RelayCommand(Increment, CanClickCookie);
        }
        bool CanClickCookie() => !DisableClick;        
    }
}
