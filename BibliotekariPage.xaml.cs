using Biblioteka.Data.DataAccess.MySql;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for BibliotekariPage.xaml
    /// </summary>
    public partial class BibliotekariPage : Page
    {
        public BibliotekariPage()
        {
            InitializeComponent();
        }

        private void btnPretraga_Click(object sender, RoutedEventArgs e)
        {
            var mysqlBibliotekar = new MySqlBibliotekar();
            try
            {
                dgBibliotekari.ItemsSource = mysqlBibliotekar.GetBibliotekarOsobaJoin(txbIme.Text, txbPrezime.Text).DefaultView;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Došlo je do greške u bazi podataka!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (dgBibliotekari.Items.IsEmpty)
                MessageBox.Show("Nema rezultata pretrage", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
