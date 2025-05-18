using GlobaWBNew.Model;
using GlobaWBNew.ViewModel;
using System.Windows;

namespace GlobaWBNew.View {
    public partial class AddNewStaffWindow : Window {
        public AddNewStaffWindow(Staff staff, Model.Point point) {
            InitializeComponent();
            DataContext = new AddNewStaffVM(staff, point);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
        }
    }
}
