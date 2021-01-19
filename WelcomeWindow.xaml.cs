using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
        /*
        private void btnBibliotekar_Click(object sender, RoutedEventArgs e)
        {
            BibliotekarLogin bibLogin = new BibliotekarLogin();
            bibLogin.Show();
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminLogin adminLogin = new AdminLogin();
            adminLogin.Show();
        }
        */
    }
}
