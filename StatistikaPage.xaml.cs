using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for StatistikaPage.xaml
    /// </summary>
    public partial class StatistikaPage : Page
    {
        public StatistikaPage()
        {
            InitializeComponent();
        }

        private void btnGeneralno_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new StatistikaGeneralnoPage());

        }
    }
}
