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
    /// Interaction logic for ClanoviSubMenuPage.xaml
    /// </summary>
    public partial class ClanoviSubMenuPage : Page
    {
        public ClanoviSubMenuPage()
        {
            InitializeComponent();
        }

        private void btnClanovi_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new ClanoviPage());
        }

        private void btnDodajClana_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new DodajClanaPage());
        }

    }
}
