using GlobaWBNew.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobaWBNew.ViewModel
{
    class EditStaff:PropChange
    {
        public Staff Good { get; set; }
        
        public RelayCommand ClickOKcmd { get; set; }    

        public EditStaff(Staff good)
        {
            Good = good;
            

            NewTitle = good.FullName;
            NewDesc = good.Post;
            NewPrice = good.Salary;
            
                
            

            ClickOKcmd = new RelayCommand(ClickOK);
        }

        private string newTitle;
        public string NewTitle
        {
            get { return newTitle; }
            set { newTitle = value; OnPropertyChanged(nameof(NewTitle)); }
        }

        private string newDesc;
        public string NewDesc
        {
            get { return newDesc; }
            set { newDesc = value; OnPropertyChanged(nameof(newDesc)); }
        }

        private int newPrice;
        public int NewPrice
        {
            get { return newPrice; }
            set { newPrice = value; OnPropertyChanged(nameof(NewPrice)); }
        }

        //private int newAmount;
        //public int NewAmount
        //{
        //    get { return newAmount; }
        //    set { newAmount = value; OnPropertyChanged(nameof(NewAmount)); }
        //}

        //private double newRate;
        //public double NewRate
        //{
        //    get { return newRate; }
        //    set { newRate = value; OnPropertyChanged(nameof(NewRate)); }
        //}

        //private Seller selectedSeller;
        //public Seller SelectedSeller
        //{
        //    get { return selectedSeller; }
        //    set { selectedSeller = value; OnPropertyChanged(nameof(SelectedSeller)); }
        //}

        void ClickOK(object obj)
        {
            Good.FullName = NewTitle;
            Good.Post = NewDesc;
            Good.Salary = NewPrice;
            //Good.Amount = NewAmount;
            //Good.Rate = NewRate;
            //Good.Seller = SelectedSeller;

            //NewTitle = good.FullName;
            //NewDesc = good.Post;
            //NewPrice = good.Salary;
        }
    }
}
