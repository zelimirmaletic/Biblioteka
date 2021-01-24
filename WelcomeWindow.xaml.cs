using System;
using System.Windows;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
            Loaded += PrijavaNaSistem_Loaded;
        }
        
        private void PrijavaNaSistem_Loaded(Object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new PrijavaNaSistem());
        }
    }
}
