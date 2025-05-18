using GlobaWBNew.Model;
using System.Collections.ObjectModel;

namespace GlobaWBNew.ViewModel {
    public class AddGoodVM : PropChange {
        public Book Good { get; set; }
        public ObservableCollection<Seller> Sellers { get; set; }
        public RelayCommand ClickOKcmd { get; set; }

        public AddGoodVM(Book good, ObservableCollection<Seller> sellers) {
            Good = good;
            Sellers = sellers;
            ClickOKcmd = new(ClickOK);
        }

        private string newTitle;
        public string NewTitle {
            get { return newTitle; }
            set { newTitle = value; OnPropertyChanged(nameof(NewTitle)); }
        }

        private string newDesc;
        public string NewDesc {
            get { return newDesc; }
            set { newDesc = value; OnPropertyChanged(nameof(newDesc)); }
        }

        private int newPrice;
        public int NewPrice {
            get { return newPrice; }
            set { newPrice = value; OnPropertyChanged(nameof(NewPrice)); }
        }

        private int newAmount;
        public int NewAmount {
            get { return newAmount; }
            set { newAmount = value; OnPropertyChanged(nameof(NewAmount)); }
        }

        private double newRate;
        public double NewRate {
            get { return newRate; }
            set { newRate = value; OnPropertyChanged(nameof(NewRate)); }
        }

        private Seller selectedSeller;
        public Seller SelectedSeller {
            get { return selectedSeller; }
            set { selectedSeller = value; OnPropertyChanged(nameof(SelectedSeller)); }
        }

        void ClickOK(object obj) {
            Good.Title = NewTitle;
            Good.Description = NewDesc;
            Good.Price = NewPrice;
            Good.Amount = NewAmount;
            Good.Rate = NewRate;
            Good.Seller = SelectedSeller;
        }
    }
}
