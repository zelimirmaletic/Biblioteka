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
            novaKnjiga.Naslov = txbNaslov.Text;

            var split = cbAutor.SelectedItem.ToString().Split(' ');
            novaKnjiga.IdZanr = Int32.Parse(split[0]);
            



            //Add Knjiga_has_Autor
            split = cbAutor.SelectedItem.ToString().Split(' ');
            MessageBox.Show(split[0]);

            


            //Get zanr id by naziv
            //Get Izdavac by naziv
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
