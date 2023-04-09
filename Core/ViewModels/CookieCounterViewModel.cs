using System.Diagnostics.Metrics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClickerGame.ViewModels
{
	public class CookieCounterViewModel : ObservableObject
	{
		private int maxCookieCooldown = 1;

		public CookieCounterViewModel(int maxCooldown = 1)
		{
			this.maxCookieCooldown = maxCooldown;
		}

		public int MaxCookieCooldown
		{
			get => maxCookieCooldown;
		}

		private int currentCookieCooldown = 0;
		public int CurrentCookieCooldown
		{
			get => currentCookieCooldown;
			set => SetProperty(ref currentCookieCooldown, value);
		}

		private int cookieCount = 0;
		public int CookieCount
		{
			get => cookieCount;
			set => SetProperty(ref cookieCount, value);
		}

		private string unitLabel= "Cookie";
		public string UnitLabel
		{
			get => unitLabel;
			set => SetProperty(ref unitLabel, value);
		}
		public bool CanClickCookie() => 
			CurrentCookieCooldown <=0;

		public void Increment()
		{
			CookieCount++;
			CurrentCookieCooldown = maxCookieCooldown;
		}
	}
}
