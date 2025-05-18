using GlobaWBNew.Model;
using GlobaWBNew.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;

namespace GlobaWBNew.View {
    public partial class EditGoodWindow : Window {
        public EditGoodWindow(Book good, ObservableCollection<Seller> sellers) {
            InitializeComponent();
            DataContext = new Editdisks(good, sellers);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
        }
    }
}
