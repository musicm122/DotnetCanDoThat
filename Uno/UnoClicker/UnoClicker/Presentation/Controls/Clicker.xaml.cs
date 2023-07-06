using ClickerGame.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace UnoClicker.Presentation.Controls
{
	public sealed partial class Clicker : UserControl
    {
       
        public Clicker()
        {
            this.InitializeComponent();         
        }

        public BaseCounterViewModel ViewModel 
        {
            get => (BaseCounterViewModel)DataContext;
            set => DataContext = value;
        }

        //public ImageSource SourceImage 
        //{
        //    get => (ImageSource)GetValue(SourceImageProperty);
        //    set => SetValue(SourceImageProperty, value);
        //} 

        //// Using a DependencyProperty as the backing store for MyProperty.This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty SourceImageProperty =
        //    DependencyProperty.Register("SourceImage", typeof(ImageSource), typeof(Clicker), new PropertyMetadata(default(ImageSource)));


        //public int Count 
        //{
        //    get => (int)GetValue(CountProperty);
        //    set => SetValue(CountProperty, value);
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CountProperty =
        //    DependencyProperty.Register("Count", typeof(int), typeof(Clicker), new PropertyMetadata(default(int)));


        //public string UnitLabel
        //{
        //    get => (string)GetValue(UnitLabelProperty);
        //    set => SetValue(UnitLabelProperty, value);
        //}
        //// Using a DependencyProperty as the backing store for MyProperty.This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty UnitLabelProperty =
        //    DependencyProperty.Register("UnitLabel", typeof(string), typeof(Clicker), new PropertyMetadata(default(string)));


        //public IRelayCommand? ClickCommand 
        //{
        //    get => (IRelayCommand)GetValue(ClickProperty); 
        //    set=> SetValue(ClickProperty, value);
        //}

        //public static readonly DependencyProperty ClickProperty =
        //    DependencyProperty.Register(
        //        "Click",
        //        typeof(IRelayCommand),
        //        typeof(Clicker),
        //        new PropertyMetadata(null, OnPropertyChanged));

        //public void OnPropertyChanged(DependencyPropertyChangedEventArgs args) 
        //{
        //    DependencyProperty property = args.Property;
        //    if (property is not null && ClickCommand is not null) 
        //    {
        //        ClickCommand.NotifyCanExecuteChanged();
        //    }            
        //}

        //private static void OnPropertyChanged(
        //    DependencyObject sender,
        //    DependencyPropertyChangedEventArgs args)
        //{
        //    var owner = (Clicker)sender;
        //    owner.OnPropertyChanged(args);
        //}
    }
}
