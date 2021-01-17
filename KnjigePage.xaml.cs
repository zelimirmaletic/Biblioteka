using Biblioteka.Data.DataAccess.MySql;
using Biblioteka.Data.Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
            var mysqlKnjiga = new MySqlKnjiga();
            try
            {
                dgKnjige.ItemsSource = mysqlKnjiga.GetKnjigaAutorZanrIzdavacJoin(txbNaslov.Text, cbZanr.SelectedItem == null ? "_%" : cbZanr.SelectedItem.ToString(), txbIzdavac.Text, txbAutor.Text).DefaultView;
            }
            catch(Exception exc)
            {
                MessageBox.Show("Došlo je do greške u bazi podataka!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (dgKnjige.Items.IsEmpty)
                MessageBox.Show("Nema rezultata pretrage", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void cbZanr_DropDownOpened(object sender, System.EventArgs e)
        {
            cbZanr.Items.Clear();
            //Initialize cbAutor values
            var query = new MySqlZanr();
            List<Zanr> list = query.GetAllZanr();
            foreach (var item in list)
                cbZanr.Items.Add(item.Naziv);
        }
    }
}
