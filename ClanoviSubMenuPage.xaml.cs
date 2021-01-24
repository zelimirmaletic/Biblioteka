using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for ClanoviSubMenuPage.xaml
    /// </summary>
    public partial class ClanoviSubMenuPage : Page
    {
        public ClanoviSubMenuPage()
        {
            InitializeComponent();
        }

        private void btnClanovi_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new ClanoviPage());
        }

        private void btnDodajClana_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new DodajClanaPage());
        }

    }
}
