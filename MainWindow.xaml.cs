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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var objekat = new Autor();
            var query = new MySqlAutor();
            objekat = query.GetAutorByID(5);
            tbTest.Text += objekat.ToString();

            /*
            DateTime date = new DateTime();
            date = DateTime.Today;
            string sqlFormattedDate = date.ToString("yyyy-MM-dd");
            Autor newAutor = new Autor(0, 2, "Jovan", "Deretic", sqlFormattedDate);
            query.SaveAutor(newAutor);
            */

            query.DeleteAutorById(7);


            
           
        }
    }
}
