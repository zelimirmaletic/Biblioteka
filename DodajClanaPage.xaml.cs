using Biblioteka.Data.DataAccess.MySql;
using Biblioteka.Data.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for DodajClanaPage.xaml
    /// </summary>
    public partial class DodajClanaPage : Page
    {
        public DodajClanaPage()
        {
            InitializeComponent();
        }

        private void btnDodajMjesto_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddMjestoWindow();
            win.ShowDialog();
        }

        private void cbMjesto_DropDownOpened(object sender, EventArgs e)
        {
            cbMjesto.Items.Clear();
            //Initialize cbMjesto values
            MySqlMjesto query = new MySqlMjesto();
            List<Mjesto> mjesta = query.GetAllMjesto();
            foreach (Mjesto mjesto in mjesta)
                this.cbMjesto.Items.Add(mjesto.Naziv);

        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (txbIme.Text.Equals("") || cbMjesto.SelectedItem.Equals(null) || dpDatumRodjenja.SelectedDate.Equals(null) || txbPrezime.Text.Equals("") || txbAdresa.Text.Equals("") || txbEmail.Text.Equals("") || txbBrojTelefona.Text.Equals(""))
            {
                string message = "Molimo vas da unesete vrijednosti u sva polja.";
                string caption = "Upozorenje";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(message, caption, buttons, icon);
                return;
            }

            var novaOsoba = new Osoba();
            novaOsoba.IdOsoba = 0;
            novaOsoba.Ime = txbIme.Text;
            novaOsoba.Prezime = txbPrezime.Text;
            novaOsoba.NazivMjesta = cbMjesto.Text;
            novaOsoba.Adresa = txbAdresa.Text;
            novaOsoba.BrojTelefona = txbBrojTelefona.Text;
            novaOsoba.Email = txbEmail.Text;
            novaOsoba.DatumRodjenja = dpDatumRodjenja.SelectedDate.Value;

            var mysqlOsoba = new MySqlOsoba();
            mysqlOsoba.SaveOsoba(novaOsoba);

            //Add clan table
            var noviClan = new Clan(novaOsoba.IdOsoba, DateTime.Today, DateTime.Today.AddYears(1));
            var mysqlClan = new MySqlClan();
            mysqlClan.SaveClan(noviClan, "insert");

            MessageBox.Show("Uspjesno dodan član!\nBroj članske karte novog člana je: "+novaOsoba.IdOsoba.ToString(), "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);

            //Clear all fields
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new DodajClanaPage());


        }
    }
}
