using Avalonia.Controls;
using OnlineScoreboardOfFlights.ViewModels;

namespace OnlineScoreboardOfFlights.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(this);
        }
    }
}