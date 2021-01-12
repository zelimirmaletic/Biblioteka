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
    /// Interaction logic for AddClanWindow.xaml
    /// </summary>
    public partial class AddClanWindow : Window
    {
        public AddClanWindow()
        {
            InitializeComponent();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            //Get an ID for chosen value from combobox
            Mjesto mjesto = new Mjesto();
            MySqlMjesto mysqlMjesto = new MySqlMjesto();
            mjesto = mysqlMjesto.GetMjestoByNaziv(cbMjesto.Text);
            //Save Clan to a database

            //Show message of success
            MessageBox.Show("Uspješno dodan novi clan!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cbMjesto_DropDownOpened(object sender, EventArgs e)
        {
            if (cbMjesto.Items.IsEmpty)
            {
                try
                {
                    //Initialize cbMjesto values
                    MySqlMjesto query = new MySqlMjesto();
                    List<Mjesto> mjesta = query.GetAllMjesto();
                    foreach (Mjesto mjesto in mjesta)
                        this.cbMjesto.Items.Add(mjesto.Naziv);
                }catch(DataAccessException exc)
                {
                    MessageBox.Show("Došlo je do greške u komunikaciji sa bazom podataka!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void addMjesto_Click(object sender, RoutedEventArgs e)
        {
            var newAddMjestoWindow = new AddMjestoWindow();
            newAddMjestoWindow.ShowDialog();
        }
    }
}
