using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for KnjigeSubMenuPage.xaml
    /// </summary>
    public partial class KnjigeSubMenuPage : Page
    {
        public KnjigeSubMenuPage()
        {
            InitializeComponent();
        }

        private void btnKnjige_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new KnjigePage());

        }

        private void btnDodajKnjigu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new DodajKnjiguPage());

        }

        private void btnZaduziKnjigu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new ZaduzivanjePage());

        }

        private void btnRazduziKnjigu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new RazduziKnjiguPage());

        }
    }
}
