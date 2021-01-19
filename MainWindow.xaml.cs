using System;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int IdBibliotekar = 0;
        public MainWindow()
        {
            var window = new WelcomeWindow();
            window.ShowDialog();

            if(IdBibliotekar!=0)
            {
                InitializeComponent();
                Loaded += MainMenuLoaded;
            }
            else
                this.Close();

        }

        private void MainMenuLoaded(Object sender, RoutedEventArgs e)
        {
            frmMainMenu.NavigationService.Navigate(new MainMenuPage());
            frmSideMenu.NavigationService.Navigate(new StatistikaPage());
            frmPageArea.NavigationService.Navigate(new StatistikaGeneralnoPage());
        }

        public void setSubmenuPage(Page page)
        {
            this.frmSideMenu.NavigationService.Navigate(page);
        }

        public void setPageArea(Page page)
        {
            this.frmPageArea.NavigationService.Navigate(page);
        }
    }
}
