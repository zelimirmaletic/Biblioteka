using Biblioteka.Data.DataAccess.Exceptions;
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
                var mysqlBibliotekar = new MySqlAdmin();
                admin = mysqlBibliotekar.GetBibliotekarByUsername(txbKorisnickoIme.Text);
                if (admin.Lozinka.Equals(txbLozinka.Password.ToString()))
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PrijavaNaSistem());
        }
    }
}
