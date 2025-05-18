using GlobaWBNew.Model;

namespace GlobaWBNew.ViewModel {
    public class AddNewStaffVM : PropChange {
        public Staff Staff { get; set; }
        public Point Point { get; set; }
        public List<string> Roles { get; set; }
        public RelayCommand ClickOKCmd { get; set; }
        public AddNewStaffVM(Staff staff, Point point) {
            Staff = staff;
            Point = point;
            Roles = new List<string>() { "ADM", "STF" };
            ClickOKCmd = new(ClickOK);
        }

        private string newFullName;
        public string NewFullName {
            get { return newFullName; }
            set { newFullName = value; OnPropertyChanged(nameof(NewFullName)); }
        }

        private string newPost;
        public string NewPost {
            get { return newPost; }
            set { newPost = value; OnPropertyChanged(nameof(NewPost)); }
        }

        private int newSalary;
        public int NewSalary {
            get { return newSalary; }
            set { newSalary = value; OnPropertyChanged(nameof(NewSalary)); }
        }

        private string newLogin;
        public string NewLogin {
            get { return newLogin; }
            set { newLogin = value; OnPropertyChanged(nameof(NewLogin)); }
        }

        private string newPassword;
        public string NewPassword {
            get { return newPassword; }
            set { newPassword = value; OnPropertyChanged(nameof(NewPassword)); }
        }

        string selectedRole;
        public string SelectedRole {
            get { return selectedRole; }
            set { selectedRole = value; OnPropertyChanged(nameof(SelectedRole)); }
        }

        void ClickOK(object obj) {
            Staff.FullName = NewFullName;
            Staff.Post = NewPost;
            Staff.Salary = NewSalary;
            Staff.Point = Point;
            Staff.Login = NewLogin;
            Staff.Password = NewPassword;
            Staff.Role = SelectedRole;
        }
    }
}
