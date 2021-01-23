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
    /// Interaction logic for DodajBibliotekaraPage.xaml
    /// </summary>
    public partial class DodajBibliotekaraPage : Page
    {
        public DodajBibliotekaraPage()
        {
            InitializeComponent();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (txbIme.Text.Equals("") || cbMjesto.SelectedItem.Equals(null) || dpDatumRodjenja.SelectedDate.Equals(null) || txbPrezime.Text.Equals("") || txbAdresa.Text.Equals("") || txbEmail.Text.Equals("") || txbBrojTelefona.Text.Equals("") || txbKorIme.Text.Equals("")||txbLozinka.Text.Equals(""))
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


            //Add bibliotekar table
            var noviBibliotekar = new Bibliotekar(novaOsoba.IdOsoba,txbKorIme.Text, txbLozinka.Text);
            var mysqlBibliotekar = new MySqlBibliotekar();

            try
            {
                mysqlBibliotekar.SaveBibliotekar(noviBibliotekar, "insert");
            }catch(Exception exc)
            {
                MessageBox.Show("Korisničko ime već postoji u bazi. Odaberite drugo korisničko ime, pa pokušajte ponovo.","Greška",MessageBoxButton.OK,MessageBoxImage.Error);
                mysqlOsoba.DeleteOsobaById(novaOsoba.IdOsoba);
                return;
            }

            //Dodaj temu!
            var mysqlTema = new MySqlTema();
            var tema = new Tema();
            tema.IdTema = 0;
            tema.IdOsoba = novaOsoba.IdOsoba;
            tema.Stil = 1;
            mysqlTema.SaveTema(tema);

            MessageBox.Show("Uspjesno dodan bibliotekar!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);

            //Clear all fields
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new DodajBibliotekaraPage());

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

        private void btnDodajMjesto_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddMjestoWindow();
            win.ShowDialog();
        }
    }
}
