using System.Windows;
using ReactiveUI;
using Realtime.Edu.Core.ViewModels;

namespace Realtime.Edu.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewFor<MainViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as MainViewModel;
        }

        public MainViewModel ViewModel { get; set; }
    }
}