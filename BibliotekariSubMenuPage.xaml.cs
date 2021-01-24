using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for BibliotekariSubMenuPage.xaml
    /// </summary>
    public partial class BibliotekariSubMenuPage : Page
    {
        public BibliotekariSubMenuPage()
        {
            InitializeComponent();
        }

        private void btnBibliotekari_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new BibliotekariPage());
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new DodajBibliotekaraPage());
        }
    }
}
