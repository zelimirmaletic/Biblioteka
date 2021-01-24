using System;
using System.Windows;

namespace Biblioteka
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }

        /// <summary>
        /// This funtion loads a ResourceDictionary from a file at runtime
        /// </summary>
        public void setStyle(int style)
        {
            ResourceDictionary newRes = new ResourceDictionary();
            newRes.Source = new Uri("Resources\\Styles\\"+ style.ToString() +".xaml", UriKind.Relative);
            this.Resources.MergedDictionaries.Clear();
            this.Resources.MergedDictionaries.Add(newRes);
        }
    }
}

