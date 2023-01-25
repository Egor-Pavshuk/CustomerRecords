using CustomerRecords.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CustomerRecords.ViewModels
{
    public class CustomerRecordViewModel : INotifyPropertyChanged
    {
        private CustomerRecord _customerRecord;
        private bool _isReadOnly;
        private string _buttonContent;
        private string _firstName;
        private string _lastName;
        public RoutedEventHandler EditSaveButton { get; private set; }
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
        public CustomerRecord Record
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
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public CustomerRecordViewModel(string firstName, string lastName)
        {
            _customerRecord = new CustomerRecord(firstName, lastName);
            ButtonContent = "Edit";
            FirstName = firstName;
            LastName = lastName;
            _isReadOnly = true;
            EditSaveButton = Edit;
        }

        private void Edit(object sender, RoutedEventArgs e)
        {
            IsReadOnly = false;
            ButtonContent = "Save";

            EditSaveButton = Save;
        }
        private async void Save(object sender, RoutedEventArgs e)
        {
            var contentDialog = new ContentDialog()
            {
                Title = "Confirmation",
                Content = "Are you sure to save?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "Cancel",
            };
            var confirmationResult = await contentDialog.ShowAsync();

            if (confirmationResult == ContentDialogResult.Primary)
            {
                _customerRecord = new CustomerRecord(FirstName, LastName);
            }
            else
            {
                FirstName = _customerRecord.FirstName; 
                LastName = _customerRecord.LastName;
            }
            IsReadOnly = true;
            EditSaveButton = Edit;
            ButtonContent = "Edit";
            return;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
