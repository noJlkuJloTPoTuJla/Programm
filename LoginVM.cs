using GlobaWBNew.Model;
using GlobaWBNew.View;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows;

namespace GlobaWBNew.ViewModel {
    public class LoginVM : PropChange {
        private ApplicationContext db = new ApplicationContext();

        public ObservableCollection<Staff> Staff { get; set; }
        private Window LoginWnd { get; set; }

        public LoginVM(Window loginWnd) {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            LoginWnd = loginWnd;

            Staff staff1 = new Staff { FullName = "Лыско Иван Евгеньевич", Login = "IvanLE", Password = "12345", Role = "ADM", Post = "Admin", Salary = 90000 };
            Staff staff2 = new Staff { FullName = "Вахрушева Анна Сергеевна", Login = "AnnaVS", Password = "54321", Role = "STF", Post = "Worker", Salary = 45000 };

            Model.Point point1 = new Model.Point { Address = "Ленина, 90", Staff = new ObservableCollection<Staff> { staff1 } };
            Model.Point point2 = new Model.Point { Address = "Сталелитейная, 44/2", Staff = new ObservableCollection<Staff> { staff2 } };
            Model.Point point3 = new Model.Point { Address = "Станиславского, 7", Staff = new ObservableCollection<Staff>() };
            Model.Point point4 = new Model.Point { Address = "Новогодняя, 142", Staff = new ObservableCollection<Staff>() };
            Model.Point point5 = new Model.Point { Address = "Планировочная, 16", Staff = new ObservableCollection<Staff>() };
            Model.Point point6 = new Model.Point { Address = "Станционная, 58", Staff = new ObservableCollection<Staff>() };
            Model.Point point7 = new Model.Point { Address = "Шекспира, 7", Staff = new ObservableCollection<Staff>() };
            Model.Point point8 = new Model.Point { Address = "Каменская, 82", Staff = new ObservableCollection<Staff>() };

            Book book1 = new Book { Title = "QWERTY", Description = "adsfljdnsfkj", Price = 123567, Amount = 134667, Rate = 12345 };

            Seller sel1 = new Seller() { Name = "JOYCITY", ContactInformation = "JoyCiti@gmail.com", Books = new ObservableCollection<Book>() };
            Seller sel2 = new Seller() { Name = "Samy", ContactInformation = "Samy@gmail.com", Books = new ObservableCollection<Book>() };
            Seller sel3 = new Seller() { Name = "Deiratex", ContactInformation = "Deiratex@gmail.com", Books = new ObservableCollection<Book>() };
            Seller sel4 = new Seller() { Name = "KROMLAND", ContactInformation = "KROMLAND@gmail.com", Books = new ObservableCollection<Book>() };
            Seller sel5 = new Seller() { Name = "Infindini", ContactInformation = "Infindini@gmail.com", Books = new ObservableCollection<Book>() };
            Seller sel6 = new Seller() { Name = "Barinoff", ContactInformation = "Barinoff@gmail.com", Books = new ObservableCollection<Book>() };

            db.Points.AddRange(point1, point2, point3, point4, point5, point6, point7, point8);
            db.Sellers.AddRange(sel1, sel2, sel3, sel4, sel5, sel6);
            db.Books.AddRange(book1);

            db.Staff.Add(staff1);
            db.Staff.Add(staff2);
            db.SaveChanges();
            db.Staff.Load();
            Staff = db.Staff.Local.ToObservableCollection();
        }

        private string login;
        public string Login {
            get { return login; }
            set { login = value; OnPropertyChanged(nameof(Login)); }
        }

        private string password;
        public string Password {
            get { return password; }
            set { password = value; OnPropertyChanged(nameof(Password)); }
        }

        /// <summary>
        /// Функция поиска сотрудника в БД.
        /// </summary>
        Staff? CorrectInformation(string login, string password) {
            if(login == null || password == null) { return null; }
            return Staff.FirstOrDefault(staff => staff.Login == login &&
                    staff.Password == password);
        }

        RelayCommand getAuth;
        public RelayCommand GetAuth {
            get {
                return getAuth ?? (getAuth = new RelayCommand(obj => {
                    Staff? foundedStaff = CorrectInformation(this.Login, this.Password);
                    if(foundedStaff == null) {
                        MessageBox.Show("Неверный логин или пароль!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    string role = foundedStaff.Role;
                    switch(role) {
                        case "ADM":
                            admWindow admWindow = new admWindow();
                            admWindow.Show();
                            break;
                        case "STF":
                            empWindow empWindow = new empWindow();
                            empWindow.Show();
                            break;
                    }
                    LoginWnd.Close();
                }));
            }
        }
    }
}
