using Biblioteka.Data.DataAccess.MySql;
using System;
using System.Windows.Controls;

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
            txbBrojIzdavaca.Text = mysqlIzdavac.GetBrojIzdavaca().ToString();
        }
    }
}
