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
        private int boja = 1;
        public PodesavanjaTemePage()
        {
            InitializeComponent();

            MySqlTema mysqlTema = new MySqlTema();
            Tema tema = new Tema();
            tema = mysqlTema.GetTemaByOsobaId(MainWindow.IdBibliotekar);
            boja = tema.Boja;
    }

        private void btnBlue_Click(object sender, RoutedEventArgs e)
        {
            boja = 1;
        }

        private void btnGreen_Click(object sender, RoutedEventArgs e)
        {
            boja = 2;
        }

        private void btnPink_Click(object sender, RoutedEventArgs e)
        {
            boja = 3;
        }

        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            boja = 4;
        }

        private void btnDark_Click(object sender, RoutedEventArgs e)
        {
            boja = 5;
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).setStyle(boja);
            //Save theme to database
            var mysqlTema = new MySqlTema();
            var tema = new Tema();
            tema.IdTema = 1;//Bitno je da nije <= 0
            tema.IdOsoba = MainWindow.IdBibliotekar;
            tema.Boja = boja;
            tema.Font = "";
            mysqlTema.SaveTema(tema);

        }
    }
}
