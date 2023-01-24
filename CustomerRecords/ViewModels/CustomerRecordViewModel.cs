using CustomerRecords.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;

namespace CustomerRecords.ViewModels
{
    public class CustomerRecordViewModel : INotifyPropertyChanged
    {
        private CustomerRecord _customerRecord;
        private bool _isReadOnly;
        private string _buttonContent;
        private string _FirstName;
        private string _LastName;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set 
            {
                _isReadOnly = value;
                OnPropertyChanged(); 
            }
        }
        public string ButtonContent
        {
            get { return _buttonContent; }
            set
            {
                _buttonContent = value;
                OnPropertyChanged();
            }
        }
        public CustomerRecord CustomersRecord
        {
            get { return _customerRecord; }
            set
            {
                _customerRecord = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                OnPropertyChanged();
            }
        }

        public CustomerRecordViewModel(string firstName, string lastName)
        {
            _customerRecord = new CustomerRecord(firstName, lastName);
            ButtonContent = "Edit";
            FirstName= firstName;
            LastName= lastName;
            _isReadOnly = true;
        }

        public void Edit()
        {
            IsReadOnly = false;
            ButtonContent = "Save";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
