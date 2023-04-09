using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ClickerGame.ViewModels
{

	public class GameViewModel : ObservableObject
	{
		
		public readonly PeriodicTimer GameTimer = new(TimeSpan.FromSeconds(1));

		public GameViewModel() 
		{
			ClickCookieCommand = new RelayCommand(ClickCookie,CookieCounter.CanClickCookie);
		}

		public CookieCounterViewModel CookieCounter = new(1);

		private bool isRunning = false;
		public bool IsRunning
		{
			get => isRunning;
			set => SetProperty(ref isRunning, value);
		}

		private int timeElapsed = 0;
		public int TimeElapsed
		{
			get => timeElapsed;
			set => SetProperty(ref timeElapsed, value);
		}


		public ICommand ClickCookieCommand { get; }

		private void ClickCookie()
		{
			CookieCounter.CookieCount++;
		}

		public void ProcessFrame()
		{
			if (CookieCounter.CurrentCookieCooldown > 0)
			{
				CookieCounter.CurrentCookieCooldown--;
			}
			TimeElapsed++;
		}

		public async Task RunGame()
		{
			IsRunning = true;
			//&& IsRunning
			while (await GameTimer.WaitForNextTickAsync() && IsRunning)
			{
				ProcessFrame();
			}
		}

	}
}
