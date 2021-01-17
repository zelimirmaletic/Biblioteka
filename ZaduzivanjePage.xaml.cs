using Biblioteka.Data.DataAccess.MySql;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ZaduzivanjePage.xaml
    /// </summary>
    public partial class ZaduzivanjePage : Page
    {
        DataTable table = new DataTable();
        public ZaduzivanjePage()
        {
            InitializeComponent();
            //Define columns
            var IdKnjiga = new DataColumn("Šifra", typeof(int));
            var Naslov = new DataColumn("Naslov", typeof(string));
            var NazivZanra = new DataColumn("Žanr", typeof(string));
            var Izdavac = new DataColumn("Izdavač", typeof(string));
            var ImeAutora = new DataColumn("Ime autora", typeof(string));
            var PrezimeAutora = new DataColumn("Prezime autora", typeof(string));
            var DatumObjavljivanja = new DataColumn("Datum objavljivanja", typeof(string));
            var ISBN = new DataColumn("ISBN", typeof(string));
            var BrojKopija = new DataColumn("Broj kopija", typeof(int));
            var BrojStranica = new DataColumn("Broj stranica", typeof(int));
            var Jezik = new DataColumn("Jezik", typeof(string));
            //Add columns to a table
            table.Columns.Add(IdKnjiga);
            table.Columns.Add(Naslov);
            table.Columns.Add(NazivZanra);
            table.Columns.Add(Izdavac);
            table.Columns.Add(ImeAutora);
            table.Columns.Add(PrezimeAutora);
            table.Columns.Add(DatumObjavljivanja);
            table.Columns.Add(ISBN);
            table.Columns.Add(BrojKopija);
            table.Columns.Add(BrojStranica);
            table.Columns.Add(Jezik);
            dgOdabranaGradja.ItemsSource = table.DefaultView;
        }

        private void cbZanr_DropDownOpened(object sender, EventArgs e)
        {
            cbZanr.Items.Clear();
            //Initialize cbAutor values
            var query = new MySqlZanr();
            var list = query.GetAllZanr();
            foreach (var item in list)
                cbZanr.Items.Add(item.Naziv);
        }

        private void btnPretraga_Click(object sender, RoutedEventArgs e)
        {
            var mysqlKnjiga = new MySqlKnjiga();
            try
            {
                dgKnjige.ItemsSource = mysqlKnjiga.GetKnjigaAutorZanrIzdavacJoin(txbNaslov.Text, cbZanr.SelectedItem == null ? "_%" : cbZanr.SelectedItem.ToString(), txbIzdavac.Text, txbAutor.Text).DefaultView;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Došlo je do greške u bazi podataka!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (dgKnjige.Items.IsEmpty)
                MessageBox.Show("Nema rezultata pretrage", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void optUkloni_Click(object sender, RoutedEventArgs e)
        {

        }

        private void optDodaj_Click(object sender, RoutedEventArgs e)
        {
            table.Rows.Add(dgKnjige.SelectedItem);
        }
    }
}
