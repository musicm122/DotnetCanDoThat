using ClickerGame.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System.Windows.Input;

namespace UnoClicker.Presentation.Controls
{
    public sealed partial class CookieClicker : UserControl
    {
        public event Action<int,string>? CountChangedEvent;
        
        public void RaiseCountChangedEvent()
        {
            Console.WriteLine($"You have {Count} {UnitLabel}(s)!");
            CountChangedEvent?.Invoke(Count,UnitLabel);
        }

        public CookieClicker()
        {
            InitializeComponent();
            clicker.Click += clicker_Click;
        }

        public ImageSource SourceImage
        {
            get => (ImageSource)GetValue(SourceImageProperty);
            set => SetValue(SourceImageProperty, value);
        }

        //Using a DependencyProperty as the backing store for MyProperty.This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceImageProperty =

            DependencyProperty.Register(
                "SourceImage", 
                typeof(ImageSource), 
                typeof(CookieClicker), 
                new PropertyMetadata(
                    default(ImageSource) ,
                    new PropertyChangedCallback(OnPropertyChanged)));     

        public int Count
        {
            get => (int)GetValue(CountProperty);
            set
            {
                RaiseCountChangedEvent();
                SetValue(CountProperty, value);
            }
        }

        //Using a DependencyProperty as the backing store for MyProperty.This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CountProperty =
            DependencyProperty.Register("Count", typeof(int), typeof(CookieClicker), new PropertyMetadata(default(int), OnPropertyChanged));


        public string UnitLabel
        {
            get => (string)GetValue(UnitLabelProperty);
            set => SetValue(UnitLabelProperty, value);
        }
        //Using a DependencyProperty as the backing store for MyProperty.This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnitLabelProperty =
            DependencyProperty.Register("UnitLabel", typeof(string), typeof(CookieClicker), new PropertyMetadata(default(string), OnPropertyChanged));


        public IRelayCommand? ClickCommand
        {
            get => (IRelayCommand)GetValue(ClickProperty);
            set => SetValue(ClickProperty, value);
        }

        public static readonly DependencyProperty ClickProperty =
            DependencyProperty.Register(
                "Click",
                typeof(IRelayCommand),
                typeof(CookieClicker),
                new PropertyMetadata(null, OnPropertyChanged));

        public void OnPropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            DependencyProperty property = args.Property;

            if (property is not null && ClickCommand is not null)
            {
                ClickCommand.NotifyCanExecuteChanged();
            }
        }

        private static void OnPropertyChanged(
            DependencyObject sender,
            DependencyPropertyChangedEventArgs args)
        {
            var owner = (CookieClicker)sender;
            owner.OnPropertyChanged(args);
        }

        private void clicker_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("CookieClicker Clicked");
        }
    }
}
