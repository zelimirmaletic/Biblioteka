using Biblioteka.Data.DataAccess.Exceptions;
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
using System.Windows.Shapes;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for AddBibliotekarWindow.xaml
    /// </summary>
    public partial class AddBibliotekarWindow : Window
    {
        public AddBibliotekarWindow()
        {
            InitializeComponent();
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (txbIme.Text.Equals("") || txbPrezime.Text.Equals("") || txbEmail.Text.Equals("") || txbBrojTelefona.Text.Equals("") || txbAdresa.Text.Equals("") || cbMjesto.SelectedItem.Equals(null) || dpDatumRodjenja.SelectedDate.Equals(null) || txbLozinka.Equals("") || txbKorisnickoIme.Equals(""))
            {
                string message = "Molimo vas da unesete vrijednosti u sva polja.";
                string caption = "Upozorenje";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(message, caption, buttons, icon);
                return;
            }
            try
            {
                //Get an ID for chosen value from combobox
                Mjesto mjesto = new Mjesto();
                MySqlMjesto mysqlMjesto = new MySqlMjesto();
                mjesto = mysqlMjesto.GetMjestoByNaziv(cbMjesto.Text);
                //Create Osoba
                //Osoba novaOsoba = new Osoba(0, mjesto.IdMjesto, txbIme.Text, txbPrezime.Text, txbAdresa.Text, txbBrojTelefona.Text, txbEmail.Text, dpDatumRodjenja.SelectedDate.Value);
                var mysqlOsoba = new MySqlOsoba();
                //mysqlOsoba.SaveOsoba(novaOsoba);
                //Create Bibliotekar
                //var noviBibliotekar = new Bibliotekar(novaOsoba.IdOsoba, txbKorisnickoIme.Text, txbLozinka.Text);
                var mysqlBibliotekar = new MySqlBibliotekar();
                //mysqlBibliotekar.SaveBibliotekar(noviBibliotekar, "insert");
                //Show message of success
                MessageBox.Show("Uspješno dodan novi bibliotekar!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (DataAccessException exc)
            {
                MessageBox.Show("Došlo je do greške u komunikaciji sa bazom podataka!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void addMjesto_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new AddMjestoWindow();
            newWindow.ShowDialog();
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
                }
                catch (DataAccessException exc)
                {
                    MessageBox.Show("Došlo je do greške u komunikaciji sa bazom podataka!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }
    }
}
