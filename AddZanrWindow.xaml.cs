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
using System.Windows.Shapes;

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
                Zanr noviZanr = new Zanr(0,txbNaziv.Text,txbOpis.Text);
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
