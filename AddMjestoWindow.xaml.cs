using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.DataAccess.MySql;
using Biblioteka.Data.Model;
using System.Windows;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for AddMjestoWindow.xaml
    /// </summary>
    public partial class AddMjestoWindow : Window
    {
        public AddMjestoWindow()
        {
            InitializeComponent();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if(txbNaziv.Text.Equals("") || txbPostanskiBroj.Text.Equals(""))
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
                Mjesto novoMjesto = new Mjesto(0, txbNaziv.Text, txbPostanskiBroj.Text);
                //Save Mjesto to database
                MySqlMjesto mysqlMjesto = new MySqlMjesto();
                mysqlMjesto.SaveMjesto(novoMjesto);
                MessageBox.Show("Uspješno dodano novo mjesto!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (DataAccessException exc)
            {
                MessageBox.Show("Naziv mjesta ili poštanski broj već postoje u bazi!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
