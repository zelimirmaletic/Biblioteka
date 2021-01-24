using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for ObavjestenjaSubMenuPage.xaml
    /// </summary>
    public partial class ObavjestenjaSubMenuPage : Page
    {
        public ObavjestenjaSubMenuPage()
        {
            InitializeComponent();
        }

        private void btnNovaObavjestenja_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new NovaObavjestenjaPage());
        }

        private void btnPrethodnaObavjestenja_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new StaraObavjestenjaPage());

        }
    }
}
