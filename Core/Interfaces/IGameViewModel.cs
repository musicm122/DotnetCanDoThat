﻿using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ClickerGame.Interfaces
{
    public interface IGameViewModel
    {
        IRelayCommand ClickCookieCommand { get; }
        IRelayCommand ClickHotdogCommand { get; }
        IInventory Inventory { get; }
        bool IsRunning { get; set; }
        int TimeElapsed { get; set; }
        string Title { get; set; }

        void ProcessFrame();
        Task RunGame();
    }
}