using GlobaWBNew.Model;
using GlobaWBNew.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;

namespace GlobaWBNew.View {
    public partial class AddNewGoodWindow : Window {
        public AddNewGoodWindow(Book good, ObservableCollection<Seller> sellers) {
            InitializeComponent();
            DataContext = new AddGoodVM(good, sellers);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
        }
    }
}
