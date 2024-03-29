﻿using Biblioteka.Data.DataAccess.MySql;
using Biblioteka.Data.Model;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for ZaduzivanjePage.xaml
    /// </summary>


    public partial class ZaduzivanjePage : Page
    {
        DataTable selectedItemsTable = new DataTable();

        public ZaduzivanjePage()
        {
            InitializeComponent();
        }

        private void cbZanr_DropDownOpened(object sender, EventArgs e)
        {
            cbZanr.Items.Clear();
            //Initialize cbAutor values
            var query = new MySqlZanr();
            var list = query.GetAllZanr();
            foreach (var item in list)
                cbZanr.Items.Add(item.Naziv);
        }

        private void btnPretraga_Click(object sender, RoutedEventArgs e)
        {
            var mysqlKnjiga = new MySqlKnjiga();
            try
            {
                dgKnjige.ItemsSource = mysqlKnjiga.GetKnjigaAutorZanrIzdavacJoin(txbNaslov.Text, cbZanr.SelectedItem == null ? "_%" : cbZanr.SelectedItem.ToString(), txbIzdavac.Text, txbAutor.Text).DefaultView;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Došlo je do greške u bazi podataka!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (dgKnjige.Items.IsEmpty)
                MessageBox.Show("Nema rezultata pretrage", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void optUkloni_Click(object sender, RoutedEventArgs e)
        {
            lwOdabranaGradja.Items.Remove(lwOdabranaGradja.SelectedItem);
        }

        private void optDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (dgKnjige.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dgKnjige.SelectedItems.Count; i++)
                {
                    DataRowView selectedRow = (DataRowView)dgKnjige.SelectedItems[i];
                    int IdKnjiga = Int32.Parse(selectedRow.Row.ItemArray[0].ToString());
                    string Naslov = Convert.ToString(selectedRow.Row.ItemArray[1]);
                    string ime = Convert.ToString(selectedRow.Row.ItemArray[4]);
                    string prezime = Convert.ToString(selectedRow.Row.ItemArray[5]);
                    string zanr = Convert.ToString(selectedRow.Row.ItemArray[2]);
                    //Check if there is available number of books
                    var mysqlPozajmica = new MySqlPozajmica();
                    var mysqlKnjiga = new MySqlKnjiga();
                    
                    int brojPozajmica = mysqlPozajmica.GetUkupanBrojPozajmicaByKnjigaId(IdKnjiga);
                    int brojKopija = mysqlKnjiga.GetBrojKopijaById(IdKnjiga);

                    if (brojPozajmica >= brojKopija)
                    {
                        MessageBox.Show("Sve kopije su zaduzene za odabranu knjigu.","Informacija",MessageBoxButton.OK,MessageBoxImage.Information);
                        return;
                    }    
                    if(lwOdabranaGradja.Items.Count<4)
                        lwOdabranaGradja.Items.Add(IdKnjiga + " " + Naslov + " | " +ime + " "+prezime+" | "+zanr);
                    else
                        MessageBox.Show("Maksimalan broj knjiga za jedno zauživanje je 4.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }

        private void btnZaduzi_Click(object sender, RoutedEventArgs e)
        {
            if (lwOdabranaGradja.Items.Count == 0)
            {
                MessageBox.Show("Potrebno je da dodate gradju!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                var mysqlClan = new MySqlClan();
                var clan = mysqlClan.GetClanByID(Int32.Parse(txbSifraClana.Text));
                if (clan.DatumObnavljanjaClanstva.CompareTo(DateTime.Today.Date) < 0)
                {
                    MessageBox.Show("Članu je isteklo članstvo u biblioteci!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Netačna šifra člana!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            foreach (var item in lwOdabranaGradja.Items)
            {
                var split = (item.ToString()).Split(' ');
                int IdKnjiga = Int32.Parse(split[0]);
                int IdClan = Int32.Parse(txbSifraClana.Text);
                var novaPozajmica = new Pozajmica(0, IdClan, IdKnjiga, MainWindow.IdBibliotekar, DateTime.Today, false, txbOpis.Text);

                var mysqlPozajmica = new MySqlPozajmica();
                mysqlPozajmica.SavePozajmica(novaPozajmica);

            }
            MessageBox.Show("Knjige uspješno zadužene!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            //Clear all fields
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new ZaduzivanjePage());



        }
    }
}
