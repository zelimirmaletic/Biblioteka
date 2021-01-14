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
        public MainWindow()
        {
            InitializeComponent();

            //var window = new WelcomeWindow();
           // window.ShowDialog();

            Loaded += MainMenuLoaded;

        }

        private void MainMenuLoaded(Object sender, RoutedEventArgs e)
        {
            frmMainMenu.NavigationService.Navigate(new MainMenuPage());
            frmSideMenu.NavigationService.Navigate(new StatistikaPage());
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
