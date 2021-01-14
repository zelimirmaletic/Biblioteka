using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void btnStatistika_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setSubmenuPage(new StatistikaPage());
        }

        private void btnKnjige_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setSubmenuPage(new KnjigeSubMenuPage());

        }

        private void btnClanovi_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setSubmenuPage(new ClanoviSubMenuPage());

        }

        private void btnObavjestenja_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setSubmenuPage(new ObavjestenjaSubMenuPage());
        }

        private void btnIzvjestaji_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setSubmenuPage(new IzvjestajSubMenuPage());

        }

        private void btnPodesavanja_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setSubmenuPage(new PodesavanjaSubMenuPage());

        }

        private void btnPomoc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setSubmenuPage(new PomocSubMenuPage());
        }

        private void btnOdjava_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
