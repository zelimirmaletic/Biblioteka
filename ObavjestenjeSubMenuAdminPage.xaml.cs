using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for ObavjestenjeSubMenuAdminPage.xaml
    /// </summary>
    public partial class ObavjestenjeSubMenuAdminPage : Page
    {
        public ObavjestenjeSubMenuAdminPage()
        {
            InitializeComponent();
        }

        private void btnKreiraj_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new KreirajObavjestenjePage());
        }

        private void btnPrethodnaObavjestenja_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new ArhivaObavjestenjaPage());
        }
    }
}
