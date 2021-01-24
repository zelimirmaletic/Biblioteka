using Biblioteka.Data.DataAccess.MySql;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for RazduziKnjiguPage.xaml
    /// </summary>
    public partial class RazduziKnjiguPage : Page
    {
        public RazduziKnjiguPage()
        {
            InitializeComponent();
        }

        private void btnUcitaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mysqlClan = new MySqlClan();
                mysqlClan.GetClanByID(Int32.Parse(txbSifraClana.Text));
            }
            catch (Exception exc)
            {
                MessageBox.Show("Netačna šifra člana!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var mysqlPozajmica = new MySqlPozajmica();
            try
            {
                dgKnjige.ItemsSource = mysqlPozajmica.GetPozajmicaJoin(Int32.Parse(txbSifraClana.Text)).DefaultView;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Došlo je do greške u bazi podataka!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (dgKnjige.Items.IsEmpty)
                MessageBox.Show("Nema rezultata pretrage", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            //Write amount
            tbIznos.Text = mysqlPozajmica.GetPozajmicaCijena(Int32.Parse(txbSifraClana.Text)).ToString() + " "+ "KM";
        }

        private void optRazduzi_Click(object sender, RoutedEventArgs e)
        {
            var mysqlPozajmica = new MySqlPozajmica();
            if (dgKnjige.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dgKnjige.SelectedItems.Count; i++)
                {
                    DataRowView selectedRow = (DataRowView)dgKnjige.SelectedItems[i];
                    int IdPozajmica = Int32.Parse(selectedRow.Row.ItemArray[0].ToString());
                    //Check if there is available number of books
                    mysqlPozajmica.PozajmicaRazduzi(IdPozajmica);
                }
                dgKnjige.ItemsSource = mysqlPozajmica.GetPozajmicaJoin(Int32.Parse(txbSifraClana.Text)).DefaultView;
                tbIznos.Text = mysqlPozajmica.GetPozajmicaCijena(Int32.Parse(txbSifraClana.Text)).ToString() + " " + "KM";
            }

        }

        private void optProduzi_Click(object sender, RoutedEventArgs e)
        {
            var mysqlPozajmica = new MySqlPozajmica();
            if (dgKnjige.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dgKnjige.SelectedItems.Count; i++)
                {
                    DataRowView selectedRow = (DataRowView)dgKnjige.SelectedItems[i];
                    int IdPozajmica = Int32.Parse(selectedRow.Row.ItemArray[0].ToString());
                    //Check if there is available number of books
                    mysqlPozajmica.PozajmicaProduzi(IdPozajmica);
                }
                dgKnjige.ItemsSource = mysqlPozajmica.GetPozajmicaJoin(Int32.Parse(txbSifraClana.Text)).DefaultView;
                tbIznos.Text = mysqlPozajmica.GetPozajmicaCijena(Int32.Parse(txbSifraClana.Text)).ToString() + " " + "KM";

            }

        }
    }
}
