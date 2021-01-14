using Biblioteka.Data.DataAccess.MySql;
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
    /// Interaction logic for StatistikaGeneralnoPage.xaml
    /// </summary>
    public partial class StatistikaGeneralnoPage : Page
    {
        public StatistikaGeneralnoPage()
        {
            InitializeComponent();

            var mysqlPozajmica = new MySqlPozajmica();
            txbBrojPozajmica.Text = mysqlPozajmica.GetUkupanBrojPozajmica().ToString();
            txbBrojKasnjenja.Text = mysqlPozajmica.GetUkupanBrojKasnihPozajmica().ToString();
            //Broj izgubljenih knjiga
            var mysqlClan = new MySqlClan();
            txbBrojClanova.Text = mysqlClan.GetBrojClanova().ToString();
            var mysqlKnjiga = new MySqlKnjiga();
            txbBrojJedinstvenihNaslova.Text = mysqlKnjiga.GetBrojNaslova().ToString();
            txbUkupanBrojKopija.Text = mysqlKnjiga.GetBrojKopija().ToString();
            txbBrojDostupnihKopija.Text = (Int32.Parse(txbUkupanBrojKopija.Text) - Int32.Parse(txbBrojPozajmica.Text)).ToString();
            var mysqlZanr = new MySqlZanr();
            txbBrojZanrova.Text = mysqlZanr.GetBrojZanrova().ToString();
            var mysqlAutor = new MySqlAutor();
            txbBrojAutora.Text = mysqlAutor.GetBrojAutora().ToString();
            var mysqlIzdavac = new MySqlIzdavac();
            txbBrojIzdavaca.Text = mysqlIzdavac.GetAllIzdavac().ToString();
        }
    }
}
