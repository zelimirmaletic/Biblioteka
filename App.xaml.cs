using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

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

