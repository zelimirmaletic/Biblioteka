using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.DataAccess.MySql;
using Biblioteka.Data.Model;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for AdminLoginPange.xaml
    /// </summary>
    public partial class AdminLoginPange : Page
    {
        public AdminLoginPange()
        {
            InitializeComponent();
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
                var admin = new Administrator();
                var mysqlAdmin = new MySqlAdmin();
                admin = mysqlAdmin.GetAdminrByUsername(txbKorisnickoIme.Text);
                if (admin.Lozinka.Equals(txbLozinka.Password.ToString()))
                {
                    //MessageBox.Show("Uspješno ste se prijavili!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                    //IMPLEMENT WINDOW OPENING!
                    MainWindow.isAdmin = true;
                    MainWindow.IdBibliotekar = admin.IdAdministrator;
                    Application.Current.MainWindow.Close();
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PrijavaNaSistem());
        }
    }
}
