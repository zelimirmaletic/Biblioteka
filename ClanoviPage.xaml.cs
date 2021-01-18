using Biblioteka.Data.DataAccess.MySql;
using Biblioteka.Data.Model;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for ClanoviPage.xaml
    /// </summary>
    public partial class ClanoviPage : Page
    {
        public ClanoviPage()
        {
            InitializeComponent();
        }

        private void btnPretraga_Click(object sender, RoutedEventArgs e)
        {
            var mysqlClan = new MySqlClan();
            try
            {
                dgClanovi.ItemsSource = mysqlClan.GetClanOsobaJoin(txbIme.Text,txbPrezime.Text).DefaultView;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Došlo je do greške u bazi podataka!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (dgClanovi.Items.IsEmpty)
                MessageBox.Show("Nema rezultata pretrage", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /*
        private void optObrisi_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)dgClanovi.SelectedItem;
            int IdClan = Int32.Parse(selectedRow.Row.ItemArray[0].ToString());
            var mysqlClan = new MySqlClan();
            var mysqlOsoba = new MySqlOsoba();
            bool deleted = false;
            try
            {
                mysqlOsoba.DeleteOsobaById(IdClan);
                deleted = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Nije moguće obrisati nalog člana.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (deleted)
            {
                try
                {
                    mysqlClan.DeleteClanById(IdClan);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Nije moguće obrisati nalog člana. Član ima pozajmice koje treba da vrati.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            MessageBox.Show("Nalog uspješno obrisan.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);

            dgClanovi.ItemsSource = mysqlClan.GetClanOsobaJoin(txbIme.Text, txbPrezime.Text).DefaultView;
        }
        */

        private void optObnovi_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)dgClanovi.SelectedItem;
            int IdClan = Int32.Parse(selectedRow.Row.ItemArray[0].ToString());

            var mysqlClan = new MySqlClan();
            mysqlClan.UpdateClanstvo(IdClan);
            dgClanovi.ItemsSource = mysqlClan.GetClanOsobaJoin(txbIme.Text, txbPrezime.Text).DefaultView;
            MessageBox.Show("Članstvo je obnovljeno!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void optAzuriraj_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = (DataRowView)dgClanovi.SelectedItem;
            var mysqlClan = new MySqlClan();
            var osoba = new Osoba();

            int IdOsoba = Int32.Parse(selectedRow.Row.ItemArray[0].ToString());
            osoba.IdOsoba = IdOsoba;

            var win = new AddClanWindow();
            win.tbSifra.Text = IdOsoba.ToString();
            win.txbIme.Text = selectedRow.Row.ItemArray[3].ToString();
            win.txbPrezime.Text = selectedRow.Row.ItemArray[4].ToString();
            win.dpDatumRodjenja.SelectedDate = DateTime.Parse(selectedRow.Row.ItemArray[5].ToString());
            win.cbMjesto.SelectedItem = selectedRow.Row.ItemArray[6].ToString();
            win.txbAdresa.Text = selectedRow.Row.ItemArray[7].ToString();
            win.txbBrojTelefona.Text = selectedRow.Row.ItemArray[8].ToString();
            win.txbEmail.Text = selectedRow.Row.ItemArray[9].ToString();

            win.ShowDialog();

            dgClanovi.ItemsSource = mysqlClan.GetClanOsobaJoin(txbIme.Text, txbPrezime.Text).DefaultView;

        }
    }
}
