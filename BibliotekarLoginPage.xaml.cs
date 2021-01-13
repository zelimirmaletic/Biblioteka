using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.DataAccess.MySql;
using Biblioteka.Data.Model;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for BibliotekarLoginPage.xaml
    /// </summary>
    public partial class BibliotekarLoginPage : Page
    {
        public BibliotekarLoginPage()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PrijavaNaSistem());
        }

        private void btnPrijaviSe_Click(object sender, RoutedEventArgs e)
        {
            if (txbLozinka.Password.ToString().Equals("") || txbKorisnickoIme.Text.Equals(""))
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
                var bibliotekar = new Bibliotekar();
                var mysqlBibliotekar = new MySqlBibliotekar();
                bibliotekar = mysqlBibliotekar.GetBibliotekarByUsername(txbKorisnickoIme.Text);
                if (bibliotekar.Lozinka.Equals(txbLozinka.Password.ToString()))
                {
                    MessageBox.Show("Uspješno ste se prijavili!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                    //IMPLEMENT WINDOW OPENING!
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("Netačna lozinka. Provjerite Vašu lozinku pa pokušajte ponovo.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (DataAccessException exc)
            {
                MessageBox.Show("Ne postoji nalog sa unijetim korisničkim imenom.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
