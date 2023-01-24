using CustomerRecords.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace CustomerRecords.ViewModels
{
    public class CustomerRecordsViewModel : INotifyPropertyChanged
    {
        //private CustomerRecord _selectedRecord;
        private CustomerRecordViewModel _record;

        public ObservableCollection<CustomerRecordViewModel> Records { get; set; }
        public string FirstName { get => _record.CustomersRecord.FirstName;
            set
            {
                _record.CustomersRecord.FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get => _record.CustomersRecord.LastName;
            set
            {
                _record.CustomersRecord.LastName = value;
                OnPropertyChanged("LastName");
            }
        }
        //public CustomerRecordViewModel SelectedRecord
        //{
        //    get { return _selectedRecord; }
        //    set
        //    {
        //        _selectedRecord = value;
        //        OnPropertyChanged("SelectedRecord");
        //    }
        //}
        
        public CustomerRecordsViewModel()
        {
            _record = new CustomerRecordViewModel(string.Empty, string.Empty);
            Records = new ObservableCollection<CustomerRecordViewModel>();
        }

        public void AddToRecords()
        {
            if (FirstName.Length > 0 && LastName.Length > 0)
            {
                if (Records.Count > 0)
                {
                    var customersRecord = new CustomerRecordViewModel(FirstName, LastName);
                    customersRecord.CustomersRecord.Id = Records.Last().CustomersRecord.Id + 1;
                    Records.Add(customersRecord);
                }
                else
                {
                    Records.Add(new CustomerRecordViewModel(FirstName, LastName));
                }

                FirstName = string.Empty;
                LastName = string.Empty;
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
