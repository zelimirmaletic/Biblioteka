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
    /// Interaction logic for PodesavanjaTemePage.xaml
    /// </summary>
    public partial class PodesavanjaTemePage : Page
    {
        private int stil = 1;
        public PodesavanjaTemePage()
        {
            InitializeComponent();

            MySqlTema mysqlTema = new MySqlTema();
            Tema tema = new Tema();
            tema = mysqlTema.GetTemaByOsobaId(MainWindow.IdBibliotekar);
            stil = tema.Stil;
    }

        private void btnBlue_Click(object sender, RoutedEventArgs e)
        {
            stil = 1;
        }

        private void btnGreen_Click(object sender, RoutedEventArgs e)
        {
            stil = 2;
        }

        private void btnPink_Click(object sender, RoutedEventArgs e)
        {
            stil = 3;
        }

        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            stil = 4;
        }

        private void btnDark_Click(object sender, RoutedEventArgs e)
        {
            stil = 5;
        }


        private void btnBlue1_Click(object sender, RoutedEventArgs e)
        {
            stil = 6;
        }

        private void btnGreen1_Click(object sender, RoutedEventArgs e)
        {
            stil = 7;
        }

        private void btnPink1_Click(object sender, RoutedEventArgs e)
        {
            stil = 8;
        }

        private void btnRed1_Click(object sender, RoutedEventArgs e)
        {
            stil = 9;
        }

        private void btnDark1_Click(object sender, RoutedEventArgs e)
        {
            stil = 10;
        }

        private void btnBlue2_Click(object sender, RoutedEventArgs e)
        {
            stil = 11;
        }

        private void btnPink2_Click(object sender, RoutedEventArgs e)
        {
            stil = 13;
        }

        private void btnGreen2_Click(object sender, RoutedEventArgs e)
        {
            stil = 12;
        }

        private void btnRed2_Click(object sender, RoutedEventArgs e)
        {
            stil = 14;
        }

        private void btnDark2_Click(object sender, RoutedEventArgs e)
        {
            stil = 15;
        }


        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).setStyle(stil);
            //Save theme to database
            var mysqlTema = new MySqlTema();
            var tema = new Tema();
            tema.IdTema = 1;//Bitno je da nije <= 0
            tema.IdOsoba = MainWindow.IdBibliotekar;
            tema.Stil = stil;
            mysqlTema.SaveTema(tema);

        }
    }
}
