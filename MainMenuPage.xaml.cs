using System;
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
            parentWindow.frmPageArea.NavigationService.Navigate(new StatistikaGeneralnoPage());
        }

        private void btnKnjige_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setSubmenuPage(new KnjigeSubMenuPage());
            parentWindow.frmPageArea.NavigationService.Navigate(new KnjigePage());

        }

        private void btnClanovi_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setSubmenuPage(new ClanoviSubMenuPage());
            parentWindow.frmPageArea.NavigationService.Navigate(new ClanoviPage());

        }

        private void btnObavjestenja_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setSubmenuPage(new ObavjestenjaSubMenuPage());
            parentWindow.frmPageArea.NavigationService.Navigate(new NovaObavjestenjaPage());

        }

        private void btnPodesavanja_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setSubmenuPage(new PodesavanjaSubMenuPage());
            parentWindow.frmPageArea.NavigationService.Navigate(new PodesavanjaTemePage());
        }

        private void btnPomoc_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setSubmenuPage(new PomocSubMenuPage());
            parentWindow.frmPageArea.NavigationService.Navigate(new UputstvoPage());
        }

        private void btnOdjava_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
