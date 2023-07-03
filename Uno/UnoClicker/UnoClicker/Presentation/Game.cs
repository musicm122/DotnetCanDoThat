// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

using ClickerGame.ViewModels;

namespace UnoClicker.Presentation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {
        public Game()
        {
            this.InitializeComponent();            
            DataContext = App.Host?.Services.GetRequiredService<GameViewModel>();
        }
    }
}
