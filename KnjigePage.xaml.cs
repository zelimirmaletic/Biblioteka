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
    /// Interaction logic for KnjigePage.xaml
    /// </summary>
    public partial class KnjigePage : Page
    {
        public KnjigePage()
        {
            InitializeComponent();
        }

        private void btnPretraga_Click(object sender, RoutedEventArgs e)
        {
            bool naslovIsEntered = false;

            if (!txbNaslov.Text.Equals(""))
                naslovIsEntered = true;
             
            var mysqlKnjiga = new MySqlKnjiga();
            var listaKnjiga = new List<Knjiga>();

            if (!naslovIsEntered)
                listaKnjiga=mysqlKnjiga.GetAllKnjiga();
            else
                listaKnjiga=mysqlKnjiga.GetAllKnjigaByNaslov(txbNaslov.Text);


            if (lwKnjige.HasItems)
                lwKnjige.ItemsSource = new List<Knjiga>();
            if (listaKnjiga.Count == 0)
                MessageBox.Show("Nema rezultata pretrage", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                lwKnjige.ItemsSource = listaKnjiga;
            
        }

        private void optIzmijeni_Click(object sender, RoutedEventArgs e)
        {

        }

        private void optObrisi_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite obrisati knjigu?", "Upozorenje", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result== MessageBoxResult.Yes)
            {
                var selectedItem = (dynamic)lwKnjige.SelectedItems[0];
                int id = selectedItem.IdKnjiga;
                var mysqlKnjiga = new MySqlKnjiga();
                mysqlKnjiga.DeleteKnjigaById(id);
            }

        }
    }
}
