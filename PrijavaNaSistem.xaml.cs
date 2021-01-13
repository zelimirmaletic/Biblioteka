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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for PrijavaNaSistem.xaml
    /// </summary>
    public partial class PrijavaNaSistem : Page
    {
        public PrijavaNaSistem()
        {
            InitializeComponent();
        }

        private void btnBibliotekar_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new BibliotekarLoginPage());
        }

        private void btnAdmin_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AdminLoginPange());

        }
    }
}
