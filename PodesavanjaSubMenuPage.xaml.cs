using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for PodesavanjaSubMenuPage.xaml
    /// </summary>
    public partial class PodesavanjaSubMenuPage : Page
    {
        public PodesavanjaSubMenuPage()
        {
            InitializeComponent();
        }

        private void btnNalog_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new PodesavanjaProfilaPage());
        }

        private void btnIzgled_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new PodesavanjaTemePage());
        }
    }
}
