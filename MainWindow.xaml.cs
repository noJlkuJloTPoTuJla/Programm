using GlobaWBNew.ViewModel;
using System.Windows;

namespace GlobaWBNew {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            DataContext = new LoginVM(this);
        }
    }
}