using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for PomocSubMenuPage.xaml
    /// </summary>
    public partial class PomocSubMenuPage : Page
    {
        public PomocSubMenuPage()
        {
            InitializeComponent();
        }

        private void btnUputstvo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new UputstvoPage());
        }

        private void btnKontakt_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new KontaktPage());
        }
    }
}
