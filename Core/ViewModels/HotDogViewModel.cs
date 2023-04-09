namespace ClickerGame.ViewModels
{
	public class HotDogViewModel : CookieCounterViewModel
	{
		private int CookieCost = 10;

		public bool CanPayCost(CookieCounterViewModel cookieVM) =>		
			cookieVM.CookieCount >= CookieCost;
		

		public HotDogViewModel(int maxCooldown = 10) : base(maxCooldown)
		{
			unitLabel = "HotDog";
		}

		public override bool CanClickCookie() 
		{ 
			return base.CanClickCookie();
		}
		
		public void Increment(CookieCounterViewModel cookieVM)
		{
			if (CanPayCost(cookieVM)) 
			{
				cookieVM.CookieCount -= CookieCost;
				base.Increment();
			}
		}
	}
}
