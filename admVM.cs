using GlobaWBNew.Model;
using GlobaWBNew.View;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;

namespace GlobaWBNew.ViewModel {
    public class admVM : PropChange {
        ApplicationContext db = new ApplicationContext();

        public ObservableCollection<Model.Point> Points { get; set; }
        public ObservableCollection<Seller> Sellers { get; set; }
        public ObservableCollection<Book> Books { get; set; }
        public ObservableCollection<Staff> Staff { get; set; }

        public RelayCommand AddNewGoodCmd { get; set; }
        public RelayCommand EditGoodCmd { get; set; }
        public RelayCommand DeleteGoodCmd { get; set; }
        public RelayCommand ClearSelectedSellerCmd { get; set; }
        public RelayCommand DeleteStaffCmd { get; set; }

        public RelayCommand EditStaffCmd { get; set; }
        public RelayCommand AddNewStaffCmd { get; set; }

        public admVM() {
            db.Database.EnsureCreated();

            db.Sellers.Load();
            db.Books.Load();
            db.Points.Load();
            db.Staff.Load(); 

            Sellers = db.Sellers.Local.ToObservableCollection();
            Books = db.Books.Local.ToObservableCollection();
            Points = db.Points.Local.ToObservableCollection();
            Staff = db.Staff.Local.ToObservableCollection();

            AddNewGoodCmd = new(AddNewGood);
            EditGoodCmd = new(EditGood);
            DeleteGoodCmd = new(DeleteGood);
            ClearSelectedSellerCmd = new(ClearSelectedSeller);
            DeleteStaffCmd = new(DeleteStaff);
            //EditStaffCmd = new(EditStaff);
            AddNewStaffCmd = new(AddNewStaff);
        }

        private string search;
        public string Search {
            get {
                return search;
            }
            set {
                search = value; OnPropertyChanged(nameof(Search));
                OnPropertyChanged(nameof(FoundItems));
            }
        }

        public ObservableCollection<Book> FoundItems {
            get {
                if(selectedSeller == null && search != null) {
                    return new ObservableCollection<Book>();
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
            get => selectedSeller;
            set { selectedSeller = value; OnPropertyChanged(nameof(SelectedSeller)); OnPropertyChanged(nameof(FoundItems)); }
        }

        private Model.Point selectedPoint;
        public Model.Point SelectedPoint {
            get { return selectedPoint; }
            set { selectedPoint = value; OnPropertyChanged(nameof(SelectedPoint)); }
        }

        void AddNewGood(object obj) {
            var good = new Book();
            if(new AddNewGoodWindow(good, Sellers).ShowDialog() == false) return;
            else if(good.Title == "") return;  
            else {
                db.Books.Add(good);
                db.SaveChanges();
            }
        }

        void EditGood(object obj) {
            if(new EditGoodWindow(obj as Book, Sellers).ShowDialog() == false) return;
            else if((obj as Book).Title == "") return;
            else {
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        //void EditStaff(object obj)
        //{
        //    if (new EditGoodWindow(obj as Book, Sellers).ShowDialog() == false) return;
        //    else if ((obj as Staff).FullName == "") return;
        //    else
        //    {
        //        db.Entry(obj).State = EntityState.Modified;
        //        db.SaveChanges();
        //    }
        //}

        void AddNewStaff(object obj) {
            if(SelectedPoint == null) {
                MessageBox.Show("Выберите пункт выдачи!", "Предупреждение!");
                return;
            }
            var staff = new Staff();
            if(new AddNewStaffWindow(staff, SelectedPoint).ShowDialog() == false) return;
            else if(staff.FullName == "") return;
            else {
                db.Staff.Add(staff);
                db.SaveChanges();
            }
        }

        void ClearSelectedSeller(object obj) {
            SelectedSeller = null;
            OnPropertyChanged(nameof(SelectedSeller));
            OnPropertyChanged(nameof(FoundItems));
        }

        void DeleteGood(object obj) {
            if(MessageBox.Show("Вы уверены, что хотите удалить " + ((Book)obj).Title + "?", "Удаление товара", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                db.Books.Remove(obj as Book);
            }
        }

        

        void DeleteStaff(object obj) {
            if(MessageBox.Show("Вы уверены, что хотите удалить " + ((Staff)obj).FullName + "?", "Удаление сотрудника", MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                db.Staff.Remove(obj as Staff);
            }
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
