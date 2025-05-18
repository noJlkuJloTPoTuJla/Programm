using GlobaWBNew.ViewModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace GlobaWBNew.View {
    /// <summary>
    /// Логика взаимодействия для empWindow.xaml
    /// </summary>
    public partial class empWindow : Window {
        private GridViewColumnHeader listViewSortCol = null;
        private SortAdorner listViewSortAdorner = null;
        public empWindow() {
            InitializeComponent();
            var VM = new empVM();
            DataContext = VM;
            VM.CloseHandler += (sender, args) => this.Close();
        }

        private void lvUsersColumnHeader_Click(object sender, RoutedEventArgs e) {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);
            string sortBy = column.Tag.ToString();
            if(listViewSortCol != null) {
                AdornerLayer.GetAdornerLayer(listViewSortCol).Remove(listViewSortAdorner);
                empListView.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if(listViewSortCol == column && listViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            listViewSortCol = column;
            listViewSortAdorner = new SortAdorner(listViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(listViewSortCol).Add(listViewSortAdorner);
            empListView.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }
    }
}
