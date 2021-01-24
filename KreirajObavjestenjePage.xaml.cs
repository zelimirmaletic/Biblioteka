using Biblioteka.Data.DataAccess.MySql;
using Biblioteka.Data.Model;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for KreirajObavjestenjePage.xaml
    /// </summary>
    public partial class KreirajObavjestenjePage : Page
    {
        public KreirajObavjestenjePage()
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

        private void btnObjavi_Click(object sender, RoutedEventArgs e)
        {
            if(txbNaslov.Text.Equals("")||txbTekst.Text.Equals("")|| (lwOdabraniPrimaoci.Items.Count==0 && !chbZaSve.IsChecked.Value))
            {
                MessageBox.Show("Popunite sva polja. Moguće je da ili dodate primaoce u listu ili da kliknete na opciju za sve kojom ćete svim bibliotekarima poslati obavjest.","Upozorenje",MessageBoxButton.OK,MessageBoxImage.Warning);
            }

            var mysqlBibliotekar = new MySqlBibliotekar();
            var mysqlObavjestenje = new MySqlObavjestenje();
            var obavjestenje = new Obavjestenje();


            if (chbZaSve.IsChecked == true)
            {
                obavjestenje.IdAdministrator = MainWindow.IdBibliotekar;
                obavjestenje.Naslov = txbNaslov.Text;
                obavjestenje.Tekst = txbTekst.Text;
                obavjestenje.ZaSve = true;
                obavjestenje.IdBibliotekar = MainWindow.IdBibliotekar;
                obavjestenje.Datum = DateTime.Today;

                mysqlObavjestenje.Insert(obavjestenje);

            }
            else
            {

                foreach (var bibliotekar in lwOdabraniPrimaoci.Items)
                {
                    obavjestenje.IdAdministrator = MainWindow.IdBibliotekar;
                    var split = bibliotekar.ToString().Split(' ');
                    int id = Int32.Parse(split[0]);
                    obavjestenje.IdBibliotekar = id;
                    obavjestenje.Naslov = txbNaslov.Text;
                    obavjestenje.Tekst = txbTekst.Text;
                    obavjestenje.ZaSve = false;
                    obavjestenje.Datum = DateTime.Today;

                    mysqlObavjestenje.Insert(obavjestenje);
                }
            }

            MessageBox.Show("Uspješno objavljeno!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            MainWindow parentWindow = Window.GetWindow(this) as MainWindow;
            parentWindow.setPageArea(new KreirajObavjestenjePage());

        }

        private void optDodaj_Click(object sender, RoutedEventArgs e)
        {
            if (dgBibliotekari.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dgBibliotekari.SelectedItems.Count; i++)
                {
                    DataRowView selectedRow = (DataRowView)dgBibliotekari.SelectedItems[i];

                    string id = Convert.ToString(selectedRow.Row.ItemArray[0]);
                    string ime = Convert.ToString(selectedRow.Row.ItemArray[2]);
                    string prezime = Convert.ToString(selectedRow.Row.ItemArray[3]);
                    bool flag = false;

                    foreach(var item in lwOdabraniPrimaoci.Items)
                    {
                        if (item.ToString().StartsWith(id))
                            flag = true;
                    }
                    if (flag)
                        MessageBox.Show("Bibliotekar je već na listi primalaca.", "Duplikat", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        lwOdabraniPrimaoci.Items.Add(id + " " + ime + " " + prezime);

                }
            }
        }

        private void optUkloni_Click(object sender, RoutedEventArgs e)
        {
            lwOdabraniPrimaoci.Items.Remove(lwOdabraniPrimaoci.SelectedItem);
        }
    }
}
