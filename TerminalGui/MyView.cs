//------------------------------------------------------------------------------

//  <auto-generated>
//      This code was generated by:
//        TerminalGuiDesigner v1.0.17.0
//      You can make changes to this file and they will not be overwritten when saving.
//  </auto-generated>
// -----------------------------------------------------------------------------

using System.Reflection;

namespace TerminalGui
{
    using ClickerGame.Services;
    using Terminal.Gui;

    public partial class MyView
    {
        void Log(string msg)
        {
            this.label.Text = $"{msg}\r\n{this.label.Text}";
        }
        
        public Button InitializeTimerButton(ItemTimer timer, int offset =0)
        {
            var width = 20;
            var retval = new Terminal.Gui.Button()
            {
                Text = $"Get {timer.Definition.Name}",
                Id = $"ID_{timer.Definition.Name}",
                Width = 12,
                Height = 1,
                X = Pos.At(offset* width),
                Y = Pos.Center(),
                Data = timer.Definition.Name,
                TextAlignment = Terminal.Gui.TextAlignment.Left,
                IsDefault = true
            };
            retval.Initialized += (_, _) =>
            {
                Log("Button Initialized called");
                retval.Enabled = false;
                timer.ResetTimer();
                timer.RaiseCooldownEvent += (_, _) =>
                {
                    Log("Button should be enabled");
                    retval.Enabled = true;
                };
            };

            retval.Clicked += () =>
            {
                Log($"{timer.Definition.Name} clicked");
                if (!timer.CanGenerateNewItem(Game.Inventory))
                {
                    Log($"You don't have enough for a {timer.Definition.Name}.");
                }
                else
                {
                    Game.Inventory.PayCost(timer.Definition.Cost);   
                    Game.AddItem(timer.Definition.Name, 1);
                    retval.Enabled = false;
                    timer.ResetTimer();
                    this.label1.Text = Game.GetCurrentLabel();
                }
            };
            return retval;
        }

        public Game Game { get; set; }

        void RefreshUI()
        {
            var message = Game.GetCurrentLabel();
            if (string.IsNullOrEmpty(message))
            {
                this.label1.Text = "You don't have any items.";
            }
            else
            {
                this.label1.Text = Game.GetCurrentLabel();
            }
        }

        public MyView()
        {
            InitializeComponent();
            Game = new Game();
            this.Game.RefreshUI += RefreshUI;
            
            for(int i=0;i<Game.ItemTimers.Count;i++)
            {
                this.Add(InitializeTimerButton(Game.ItemTimers[i], i));
            }
            RefreshUI();
        }
    }
}