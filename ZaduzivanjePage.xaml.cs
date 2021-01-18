using Biblioteka.Data.DataAccess.MySql;
using Biblioteka.Data.Model;
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
        DataTable selectedItemsTable = new DataTable();
        private object mysqlPozajmica;

        public ZaduzivanjePage()
        {
            InitializeComponent();
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
            lwOdabranaGradja.Items.Remove(lwOdabranaGradja.SelectedItem);
        }

        private void optDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (dgKnjige.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dgKnjige.SelectedItems.Count; i++)
                {
                    DataRowView selectedRow = (DataRowView)dgKnjige.SelectedItems[i];
                    int IdKnjiga = Int32.Parse(selectedRow.Row.ItemArray[0].ToString());
                    string Naslov = Convert.ToString(selectedRow.Row.ItemArray[1]);
                    //Check if there is available number of books
                    var mysqlPozajmica = new MySqlPozajmica();
                    var mysqlKnjiga = new MySqlKnjiga();
                    
                    int brojPozajmica = mysqlPozajmica.GetUkupanBrojPozajmicaByKnjigaId(IdKnjiga);
                    int brojKopija = mysqlKnjiga.GetBrojKopijaById(IdKnjiga);

                    if (brojPozajmica >= brojKopija)
                    {
                        MessageBox.Show("Sve kopije su zaduzene za odabranu knjigu.","Informacija",MessageBoxButton.OK,MessageBoxImage.Information);
                        return;
                    }    
        
                    lwOdabranaGradja.Items.Add(IdKnjiga + " " + Naslov);
                }
            }

        }

        private void btnZaduzi_Click(object sender, RoutedEventArgs e)
        {
            if (lwOdabranaGradja.Items.Count == 0)
            {
                MessageBox.Show("Potrebno je da dodate gradju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                var mysqlClan = new MySqlClan();
                var clan = mysqlClan.GetClanByID(Int32.Parse(txbSifraClana.Text));
                if (clan.DatumObnavljanjaClanstva.CompareTo(DateTime.Today.Date) < 0)
                {
                    MessageBox.Show("Članu je isteklo članstvo u biblioteci!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Netačna šifra člana!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            foreach (var item in lwOdabranaGradja.Items)
            {
                var split = (item.ToString()).Split(' ');
                int IdKnjiga = Int32.Parse(split[0]);
                int IdClan = Int32.Parse(txbSifraClana.Text);
                var novaPozajmica = new Pozajmica(0, IdClan, IdKnjiga, MainWindow.IdBibliotekar, DateTime.Today, false, txbOpis.Text);

                var mysqlPozajmica = new MySqlPozajmica();
                mysqlPozajmica.SavePozajmica(novaPozajmica);

            }
            MessageBox.Show("Knjige uspješno zadužene!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            //Clear all fields
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new ZaduzivanjePage());



        }
    }
}
