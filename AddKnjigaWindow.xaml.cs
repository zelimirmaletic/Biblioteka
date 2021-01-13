using Biblioteka.Data.DataAccess.MySql;
using Biblioteka.Data.Model;
using System;
using System.Collections.Generic;
using System.Windows;


namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for AddKnjigaWindow.xaml
    /// </summary>
    public partial class AddKnjigaWindow : Window
    {
        public AddKnjigaWindow()
        {
            InitializeComponent();
        }

        private void cbAutor_DropDownOpened(object sender, EventArgs e)
        {
            cbAutor.Items.Clear();
            //Initialize cbAutor values
            MySqlAutor query = new MySqlAutor();
            List<Autor> mjesta = query.GetAllAutor();
            foreach (Autor autor in mjesta)
                 cbAutor.Items.Add(autor.IdAutor +" "+ autor.Ime+" "+autor.Prezime);
        }

        private void btnDodajAutor_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddAutorWindow();
            window.ShowDialog();
        }

        private void cbZanr_DropDownOpened(object sender, EventArgs e)
        {
            cbZanr.Items.Clear();
            //Initialize cbAutor values
            var query = new MySqlZanr();
            List<Zanr> list = query.GetAllZanr();
            foreach (var item in list)
                cbZanr.Items.Add(item.IdZanr + " " +item.Naziv);
        }

        private void btnDodajZnar_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddZanrWindow();
            window.ShowDialog();
        }

        private void cbIzdavac_DropDownOpened(object sender, EventArgs e)
        {
            cbIzdavac.Items.Clear();
            //Initialize cbAutor values
            var query = new MySqlIzdavac();
            List<Izdavac> list = query.GetAllIzdavac();
            foreach (var item in list)
                cbIzdavac.Items.Add(item.IdIzdavac + " " +item.Naziv);
        }

        private void btnDodajIzdavaca_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddIzdavacWindow();
            window.ShowDialog();
        }

        private void cbJezik_DropDownOpened(object sender, EventArgs e)
        {
            cbJezik.Items.Add("srpski");
            cbJezik.Items.Add("engleski");
            cbJezik.Items.Add("ruski");
            cbJezik.Items.Add("njemački");
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (txbNaslov.Text.Equals("")||cbAutor.SelectedItem.Equals(null)||cbZanr.SelectedItem.Equals(null)||cbIzdavac.SelectedItem.Equals(null)||dpDatumObjavljivanja.SelectedDate.Equals(null)||txbISBN.Text.Equals("")||txbBrojKopija.Text.Equals("")||txbBrojStranica.Text.Equals(""))
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

            var split = cbZanr.SelectedItem.ToString().Split(' ');
            novaKnjiga.IdZanr = Int32.Parse(split[0]);

            split = cbIzdavac.SelectedItem.ToString().Split(' ');
            novaKnjiga.IdIzdavac = Int32.Parse(split[0]);

            novaKnjiga.DatumObjavljivanja = dpDatumObjavljivanja.SelectedDate.Value;
            novaKnjiga.ISBN = txbISBN.Text;
            try
            {
                novaKnjiga.UkupanBrojKopija = Int32.Parse(txbBrojKopija.Text);
            }catch(Exception exc)
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
            novaKnjiga.Jezik = cbJezik.SelectedItem.ToString();
            novaKnjiga.Opis = txbOpis.Text;

            var mysqlKnjiga = new MySqlKnjiga();
            mysqlKnjiga.SaveKnjiga(novaKnjiga);

            //Add Knjiga_ima_Autor
            split = cbAutor.SelectedItem.ToString().Split(' ');
            var knjigaImaAutor = new Knjiga_ima_Autor(novaKnjiga.IdKnjiga, Int32.Parse(split[0]));
            var mysqlKnjiga_ima_Autor = new MySqlKnjiga_ima_autor();
            mysqlKnjiga_ima_Autor.SaveKnjiga_ima_Autor(knjigaImaAutor,"insert");

            MessageBox.Show("Uspjesno dodana knjiga!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();

        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
