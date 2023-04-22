namespace BlazorComponents.ViewModel
{
	public class HotDogViewModel : CookieCounterViewModel
	{
		private int CookieCost = 10;

		public bool CanPayCost(CookieCounterViewModel cookieVM) =>		
			cookieVM.CookieCount >= CookieCost;
		
		public void Increment(CookieCounterViewModel cookieVm)
		{
			if (!CanPayCost(cookieVm)) return;
			cookieVm.CookieCount -= CookieCost;
			base.Increment();
		}
	}
}
