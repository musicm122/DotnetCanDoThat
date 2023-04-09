using System.Diagnostics.Metrics;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ClickerGame.ViewModels
{

	public class CookieCounterViewModel : ObservableObject
	{
		protected int maxCookieCooldown = 1;
		protected string unitLabel = "Cookie";


		public CookieCounterViewModel(int maxCooldown = 1)
		{
			maxCookieCooldown = maxCooldown;
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

		public string UnitLabel
		{
			get => unitLabel;
			set => SetProperty(ref unitLabel, value);
		}
		public virtual bool CanClickCookie() => 
			CurrentCookieCooldown <=0;

		public void Increment()
		{
			CookieCount++;
			CurrentCookieCooldown = maxCookieCooldown;
		}
	}
}
