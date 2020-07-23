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
using System.Windows.Shapes;

namespace RList
{
    /// <summary>
    /// Логика взаимодействия для SearchResult.xaml
    /// </summary>
    public partial class SearchResult : Window
    {
        public RMain RMain;
        public MainWindow mainWindow;
        public SearchResult()
        {
            InitializeComponent();
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            var SText = SearchText.Text;
            if(SText.Length!=0 & SText != "SearchText")
            {
                var resutl = (new RMain.SRanobe().Search(SText)).data;
                SResultV.Items.Clear();
                foreach (var item in resutl)
                {
                    SResultV.Items.Add(item);
                }
            }
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            var g = SResultV.SelectedItems;
            foreach (RMain.SRanobe.SRdata item in g)
            {
                RMain.GetRanobe(item.id, item.names);
            }
            RMain.Save();
            mainWindow.RList.Items.Refresh();
        }
    }
}
