using System.Windows;
using System.Windows.Controls;

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
