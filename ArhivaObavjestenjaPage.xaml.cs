using Biblioteka.Data.DataAccess.MySql;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for ArhivaObavjestenjaPage.xaml
    /// </summary>
    public partial class ArhivaObavjestenjaPage : Page
    {
        public ArhivaObavjestenjaPage()
        {
            InitializeComponent();
        }

        private void lvObavjestenja_Loaded(object sender, RoutedEventArgs e)
        {
            //Ucitaj obavjestenja
            var mysqlObavjestenje = new MySqlObavjestenje();
            DataTable table = mysqlObavjestenje.GetAllObavjestenjeJoin();
            foreach (DataRow row in table.Rows)
            {
                string ime = row["Ime"].ToString();
                string prezime = row["Prezime"].ToString();
                string datum = row["Datum"].ToString();
                string naslov = row["Naslov"].ToString().ToUpper();
                string tekst = row["Tekst"].ToString();
                string formatiranTekst = "\n" + naslov + "\n" + "---------------------------------" + "\n" + tekst + "\n" + datum + "\n" + ime + " " + prezime + "\n";

                lvObavjestenja.Items.Add(formatiranTekst);

            }
        }
    }
}
