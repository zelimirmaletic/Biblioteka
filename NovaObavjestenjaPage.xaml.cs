using Biblioteka.Data.DataAccess.MySql;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for NovaObavjestenjaPage.xaml
    /// </summary>
    public partial class NovaObavjestenjaPage : Page
    {
        public NovaObavjestenjaPage()
        {
            InitializeComponent();

        }

        private void lvObavjestenja_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //Ucitaj obavjestenja
            var mysqlObavjestenje = new MySqlObavjestenje();
            DataTable table = mysqlObavjestenje.GetObavjestenjeJoin(MainWindow.IdBibliotekar);
            foreach (DataRow row in table.Rows)
            {
                string ime = row["Ime"].ToString();
                string prezime = row["Prezime"].ToString();
                string datum = row["Datum"].ToString();
                string naslov = row["Naslov"].ToString().ToUpper();
                string tekst = row["Tekst"].ToString();
                string formatiranTekst = "\n"+naslov + "\n" + "---------------------------------" + "\n" + tekst + "\n" + datum + "\n" + ime + " " + prezime+"\n";

                if (DateTime.Today.Equals(DateTime.Parse(datum)))
                {
                    lvObavjestenja.Items.Add(formatiranTekst);
                }

            }
            if(lvObavjestenja.Items.Count == 0)
                lvObavjestenja.Items.Add("Nemate novih obavještenja.");
        }
    }
}
