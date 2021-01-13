using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.DataAccess.MySql;
using Biblioteka.Data.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for AddAutorWindow.xaml
    /// </summary>
    public partial class AddAutorWindow : Window
    {
        public AddAutorWindow()
        {
            InitializeComponent();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (this.txbPrezime.Text.Equals("") || this.txbIme.Equals("") || this.cbMjesto.Text.Equals("") || this.dpDatumRodjenja.Equals(null))
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
                //Save Autor to a database
                Autor noviAutor = new Autor(0, mjesto.IdMjesto, txbIme.Text, txbPrezime.Text, dpDatumRodjenja.SelectedDate.Value);
                MySqlAutor mysqlAutor = new MySqlAutor();
                mysqlAutor.SaveAutor(noviAutor);
                //Show message of success
                MessageBox.Show("Uspješno dodan novi autor!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (DataAccessException exc)
            {
                MessageBox.Show("Došlo je do greške u komunikaciji sa bazom podataka!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addMjestoWindow = new AddMjestoWindow();
            addMjestoWindow.ShowDialog();
        }
    }
}
