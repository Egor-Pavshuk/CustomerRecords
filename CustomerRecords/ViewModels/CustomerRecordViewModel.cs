using CustomerRecords.Events;
using CustomerRecords.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
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
        public event PropertyChangedEventHandler PropertyChanged;

        public RoutedEventHandler EditSaveButton { get; private set; }
        public event EventHandler<DeleteRecordEventArgs> DeleteRecord;

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
        public void RemoveRecord()
        {
            OnDeleteRecord(this, new DeleteRecordEventArgs(_customerRecord.Id));
        }       
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private void OnDeleteRecord(object sender, DeleteRecordEventArgs e)
        {
            var temp = Volatile.Read(ref DeleteRecord);
            if (temp != null)
            {
                temp(sender, e);
            }
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
    }
}
