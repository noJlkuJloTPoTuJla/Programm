using GlobaWBNew.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace GlobaWBNew.ViewModel {
    public class empVM : PropChange {
        ApplicationContext db = new ApplicationContext();

        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<Seller> Sellers { get; set; }
        public RelayCommand IssueGoodCmd { get; set; }
        public RelayCommand ClearSelectedSellerCmd { get; set; }

        public empVM() {
            //db.Database.EnsureCreated();
            db.Books.Load();
            db.Sellers.Load();
            Books = db.Books.Local.ToObservableCollection();
            Sellers = db.Sellers.Local.ToObservableCollection();

            IssueGoodCmd = new(IssueGood);
            ClearSelectedSellerCmd = new(ClearSelectedSeller);
        }

        private string search;
        public string Search {
            get { return search; }
            set { search = value; OnPropertyChanged(nameof(Search)); OnPropertyChanged(nameof(FoundItems)); }
        }

        public ObservableCollection<Book> FoundItems {
            get {
                if(selectedSeller == null && search != null) {
                    return new ObservableCollection<Book>(Books.Where(book => book.Title.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0));
                }
                if(selectedSeller != null && search == null) {
                    return new ObservableCollection<Book>(Books.Where(book => book.Seller.Name == SelectedSeller.Name));
                }
                if(selectedSeller != null && search != null) {
                    return new ObservableCollection<Book>(Books.Where(book => book.Seller.Name == SelectedSeller.Name
                                                            && book.Title.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0));
                } else return Books;
            }
        }

        private Seller selectedSeller;
        public Seller SelectedSeller {
            get { return selectedSeller; }
            set { selectedSeller = value; OnPropertyChanged(nameof(SelectedSeller)); OnPropertyChanged(nameof(FoundItems)); }
        }

        void IssueGood(object obj) {
            if(((Book)obj).Amount > 1) {
                ((Book)obj).Amount--;
                db.Update((Book)obj);
                db.SaveChanges();
                OnPropertyChanged(nameof(FoundItems));
            } else {
                db.Remove((Book)obj);
                db.SaveChanges();
                OnPropertyChanged(nameof(FoundItems));
            }
        }

        void ClearSelectedSeller(object obj) {
            SelectedSeller = null;
            OnPropertyChanged(nameof(SelectedSeller));
        }

        private RelayCommand close;
        public EventHandler CloseHandler;
        public RelayCommand Close {
            get {
                return close ?? (close = new RelayCommand(obj => {
                    var handler = CloseHandler;
                    if(handler != null) handler.Invoke(this, EventArgs.Empty);
                }));
            }
        }
    }
}
