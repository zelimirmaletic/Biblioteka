using System.Windows.Controls;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for UputstvoPage.xaml
    /// </summary>
    public partial class UputstvoPage : Page
    {
        public UputstvoPage()
        {
            InitializeComponent();
            PdfViewerControl.Load(@"..\..\Resources\Manual\manual.pdf.pdf");
        }
    }
}
