using Biblioteka.Data.DataAccess.MySql;
using Biblioteka.Data.Model;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var window = new WelcomeWindow();
            window.ShowDialog();


            
           
        }

        private void btnDodajKnjigu_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new AddKnjigaWindow();
            newWindow.ShowDialog();
        }

        private void btnDodajClana_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new AddClanWindow();
            newWindow.ShowDialog();
        }

        private void btnIznajmiKnjigu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRazduziKnjigu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPretraga_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
