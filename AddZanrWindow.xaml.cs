using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.DataAccess.MySql;
using Biblioteka.Data.Model;
using System.Windows;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for AddZanrWindow.xaml
    /// </summary>
    public partial class AddZanrWindow : Window
    {
        public AddZanrWindow()
        {
            InitializeComponent();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if(txbNaziv.Text.Equals(""))
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
                //Save Zanr to a database
                Zanr noviZanr = new Zanr(txbNaziv.Text,txbOpis.Text);
                MySqlZanr mysqlZanr = new MySqlZanr();
                mysqlZanr.SaveZanr(noviZanr);
                //Show message of success
                MessageBox.Show("Uspješno dodan novi žanr!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }catch(DataAccessException exc)
            {
                MessageBox.Show("Naziv žanra već postoji.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
