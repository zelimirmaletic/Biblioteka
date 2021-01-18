using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.DataAccess.MySql;
using Biblioteka.Data.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for AddClanWindow.xaml
    /// </summary>
    public partial class AddClanWindow : Window
    {
        public AddClanWindow()
        {
            InitializeComponent();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (txbIme.Text.Equals("") || txbPrezime.Text.Equals("") || txbEmail.Text.Equals("") || txbBrojTelefona.Text.Equals("") || txbAdresa.Text.Equals("") || cbMjesto.SelectedItem==null || dpDatumRodjenja.SelectedDate.Equals(null))
            {
                string message = "Molimo vas da unesete vrijednosti u sva polja.";
                string caption = "Upozorenje";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(message, caption, buttons, icon);
                return;
            }
            //UPDATE
            var mysqlOsoba = new MySqlOsoba();
            var osoba = new Osoba();

            osoba.IdOsoba = Int32.Parse(tbSifra.Text);
            osoba.Ime = txbIme.Text;
            osoba.Prezime = txbPrezime.Text;
            osoba.NazivMjesta = cbMjesto.Text;
            osoba.Adresa = txbAdresa.Text;
            osoba.DatumRodjenja = dpDatumRodjenja.SelectedDate.Value;
            osoba.BrojTelefona = txbBrojTelefona.Text;
            osoba.Email = txbEmail.Text;

            mysqlOsoba.SaveOsoba(osoba);
            MessageBox.Show("Podaci o osobi su uspješno ažurirani!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbMjesto_DropDownOpened(object sender, EventArgs e)
        {
            cbMjesto.Items.Clear();
                try
                {
                    //Initialize cbMjesto values
                    MySqlMjesto query = new MySqlMjesto();
                    List<Mjesto> mjesta = query.GetAllMjesto();
                    foreach (Mjesto m in mjesta)
                        this.cbMjesto.Items.Add(m.Naziv);
                }catch(DataAccessException exc)
                {
                    MessageBox.Show("Došlo je do greške u komunikaciji sa bazom podataka!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }

        private void addMjesto_Click(object sender, RoutedEventArgs e)
        {
            var newAddMjestoWindow = new AddMjestoWindow();
            newAddMjestoWindow.ShowDialog();
        }
    }
}
