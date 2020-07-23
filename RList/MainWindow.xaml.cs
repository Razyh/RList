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
using Notif;
namespace RList
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RMain RMain = new RMain().init();
        public NotifC Notif = new NotifC();
        public MainWindow()
        {
            InitializeComponent();
            /*
            foreach (var item in RMain.Ranobes)
            {
                RList.Items.Add(item);
            }
            */
            RList.ItemsSource = RMain.Ranobes;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            SearchResult searchResult = new SearchResult();
            searchResult.Owner = this;
            searchResult.RMain = RMain;
            searchResult.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Notif.createNotif("Test","Test","RList");
        }
        private void R_delete(object sender, RoutedEventArgs e)
        {
            foreach (RMain.Ranobe item in RList.SelectedItems)
            {
                RMain.Remove(item);
            }
        }
        private void R_update(object sender, RoutedEventArgs e)
        {
            RMain.Update();
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var t = (sender as TextBlock).DataContext as RMain.Ranobe;
            //var id = RList.Items.IndexOf(t);
            t.RededChapters += 1;
            RList.Items.Refresh();

            //RList.Items.RemoveAt(id);
            //RList.Items.Insert(id, t);
            RMain.Save();
        }

        private void R_edit(object sender, RoutedEventArgs e)
        {
            foreach (RList.RMain.Ranobe item in RList.SelectedItems)
            {

            }
        }
    }
}
