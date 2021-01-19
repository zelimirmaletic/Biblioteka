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
    /// Interaction logic for PodesavanjaProfilaPage.xaml
    /// </summary>
    public partial class PodesavanjaProfilaPage : Page
    {
        public PodesavanjaProfilaPage()
        {
            InitializeComponent();

            var mysqlOsoba = new MySqlOsoba();
            var osoba = mysqlOsoba.GetOsobaByID(MainWindow.IdBibliotekar);

            txbIme.Text = osoba.Ime;
            txbPrezime.Text = osoba.Prezime;
            cbMjesto.SelectedItem = osoba.NazivMjesta;
            txbAdresa.Text = osoba.Adresa;
            txbBrojTelefona.Text = osoba.BrojTelefona;
            dpDatumRodjenja.Text = osoba.DatumRodjenja.ToShortDateString();
            txbEmail.Text = osoba.Email;

        }

        private void btnDodajMjesto_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddMjestoWindow();
            win.ShowDialog();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (cbMjesto.SelectedItem==null || txbAdresa.Text.Equals("") || txbEmail.Text.Equals("") || txbBrojTelefona.Text.Equals(""))
            {
                string message = "Molimo vas da unesete vrijednosti u sva polja.";
                string caption = "Upozorenje";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(message, caption, buttons, icon);
                return;
            }

            var novaOsoba = new Osoba();
            novaOsoba.IdOsoba = MainWindow.IdBibliotekar;
            novaOsoba.Ime = txbIme.Text;
            novaOsoba.Prezime = txbPrezime.Text;
            novaOsoba.NazivMjesta = cbMjesto.SelectedItem.ToString();
            novaOsoba.Adresa = txbAdresa.Text;
            novaOsoba.BrojTelefona = txbBrojTelefona.Text;
            novaOsoba.Email = txbEmail.Text;
            novaOsoba.DatumRodjenja = DateTime.Parse(dpDatumRodjenja.Text);

            var mysqlOsoba = new MySqlOsoba();
            mysqlOsoba.SaveOsoba(novaOsoba);

            //Reload
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new PodesavanjaProfilaPage());
            MessageBox.Show("Profil je uspješno ažuriran!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);


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
    }
}
