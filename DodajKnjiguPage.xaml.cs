using Biblioteka.Data.DataAccess.MySql;
using Biblioteka.Data.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for DodajKnjiguPage.xaml
    /// </summary>
    public partial class DodajKnjiguPage : Page
    {
        public DodajKnjiguPage()
        {
            InitializeComponent();
        }

        private void btnDodajAutora_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddAutorWindow();
            window.ShowDialog();
        }

        private void btnDodajZanr_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddZanrWindow();
            window.ShowDialog();
        }

        private void btnDodajIzdavaca_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddIzdavacWindow();
            window.ShowDialog();
        }

        private void cbAutor_DropDownOpened(object sender, System.EventArgs e)
        {
            cbAutor.Items.Clear();
            //Initialize cbMjesto values
            var query = new MySqlAutor();
            var list = query.GetAllAutor();
            foreach (var item in list)
                cbAutor.Items.Add(item.IdAutor + " " + item.Prezime + " " + item.Ime);
        }

        private void cbZanr_DropDownOpened(object sender, System.EventArgs e)
        {
            cbZanr.Items.Clear();
            //Initialize cbMjesto values
            var query = new MySqlZanr();
            var list = query.GetAllZanr();
            foreach (var item in list)
                cbZanr.Items.Add(item.Naziv);
        }

        private void cbIzdavac_DropDownOpened(object sender, System.EventArgs e)
        {
            cbIzdavac.Items.Clear();
            //Initialize cbMjesto values
            var query = new MySqlIzdavac();
            var list = query.GetAllIzdavac();
            foreach (var item in list)
                cbIzdavac.Items.Add(item.IdIzdavac + " " + item.Naziv);
        }

        private void cbJezik_DropDownOpened(object sender, System.EventArgs e)
        {
            cbJezik.Items.Add("srpski");
            cbJezik.Items.Add("ruski");
            cbJezik.Items.Add("engleski");
            cbJezik.Items.Add("italijanski");
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (txbNaslov.Text.Equals("") || cbAutor.SelectedItem.Equals(null) || cbZanr.SelectedItem.Equals(null) || cbIzdavac.SelectedItem.Equals(null) || dpDatumObjavljivanja.SelectedDate.Equals(null) || txbISBN.Text.Equals("") || txbBrojKopija.Text.Equals("") || txbBrojStranica.Text.Equals(""))
            {
                string message = "Molimo vas da unesete vrijednosti u sva polja.";
                string caption = "Upozorenje";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBox.Show(message, caption, buttons, icon);
                return;
            }
            var novaKnjiga = new Knjiga();
            novaKnjiga.IdKnjiga = 0;
            novaKnjiga.Naslov = txbNaslov.Text;
            novaKnjiga.NazivZanra = cbZanr.Text;

            var split = cbIzdavac.SelectedItem.ToString().Split(' ');
            novaKnjiga.IdIzdavac = Int32.Parse(split[0]);

            split = cbAutor.SelectedItem.ToString().Split(' ');
            novaKnjiga.IdAutor = Int32.Parse(split[0]);

            novaKnjiga.DatumObjavljivanja = dpDatumObjavljivanja.SelectedDate.Value;
            novaKnjiga.ISBN = txbISBN.Text;
            try
            {
                novaKnjiga.UkupanBrojKopija = Int32.Parse(txbBrojKopija.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Broj kopija mora biti cijeli broj", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                novaKnjiga.BrojStranica = Int32.Parse(txbBrojStranica.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Broj stranica mora biti cijeli broj", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            novaKnjiga.Jezik = cbJezik.Text;
            novaKnjiga.Opis = txbOpis.Text;

            var mysqlKnjiga = new MySqlKnjiga();
            mysqlKnjiga.SaveKnjiga(novaKnjiga);

            MessageBox.Show("Uspjesno dodana knjiga!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);

            //Clear all fields
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new DodajKnjiguPage());
        }
    }
}
