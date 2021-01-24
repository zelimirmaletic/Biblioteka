using Biblioteka.Data.DataAccess.MySql;
using Biblioteka.Data.Model;
using System.Windows;
using System.Windows.Controls;

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
            btnSacuvaj_Click(sender, e);
        }

        private void btnGreen_Click(object sender, RoutedEventArgs e)
        {
            stil = 2;
            btnSacuvaj_Click(sender, e);
        }

        private void btnPink_Click(object sender, RoutedEventArgs e)
        {
            stil = 3;
            btnSacuvaj_Click(sender, e);
        }

        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            stil = 4;
            btnSacuvaj_Click(sender, e);
        }

        private void btnDark_Click(object sender, RoutedEventArgs e)
        {
            stil = 5;
            btnSacuvaj_Click(sender, e);
        }


        private void btnBlue1_Click(object sender, RoutedEventArgs e)
        {
            stil = 6;
            btnSacuvaj_Click(sender, e);
        }

        private void btnGreen1_Click(object sender, RoutedEventArgs e)
        {
            stil = 7;
            btnSacuvaj_Click(sender, e);
        }

        private void btnPink1_Click(object sender, RoutedEventArgs e)
        {
            stil = 8;
            btnSacuvaj_Click(sender, e);
        }

        private void btnRed1_Click(object sender, RoutedEventArgs e)
        {
            stil = 9;
            btnSacuvaj_Click(sender, e);
        }

        private void btnDark1_Click(object sender, RoutedEventArgs e)
        {
            stil = 10;
            btnSacuvaj_Click(sender, e);
        }

        private void btnBlue2_Click(object sender, RoutedEventArgs e)
        {
            stil = 11;
            btnSacuvaj_Click(sender, e);
        }

        private void btnPink2_Click(object sender, RoutedEventArgs e)
        {
            stil = 13;
            btnSacuvaj_Click(sender, e);
        }

        private void btnGreen2_Click(object sender, RoutedEventArgs e)
        {
            stil = 12;
            btnSacuvaj_Click(sender, e);
        }

        private void btnRed2_Click(object sender, RoutedEventArgs e)
        {
            stil = 14;
            btnSacuvaj_Click(sender, e);
        }

        private void btnDark2_Click(object sender, RoutedEventArgs e)
        {
            stil = 15;
            btnSacuvaj_Click(sender, e);
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
