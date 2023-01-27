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
        private CustomerRecord customerRecord;
        private bool isReadOnly;
        private string buttonContent;
        private string firstName;
        private string lastName;
        public event PropertyChangedEventHandler PropertyChanged;

        public RoutedEventHandler EditSaveButton { get; private set; }
        public event EventHandler<DeleteRecordEventArgs> DeleteRecord;

        public bool IsReadOnly
        {
            get => isReadOnly;
            set
            {
                isReadOnly = value;
                OnPropertyChanged();
            }
        }
        public string ButtonContent
        {
            get => buttonContent;
            set
            {
                buttonContent = value;
                OnPropertyChanged();
            }
        }
        public CustomerRecord Record
        {
            get => customerRecord;
            set
            {
                customerRecord = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }
        public CustomerRecordViewModel(string firstName, string lastName)
        {
            customerRecord = new CustomerRecord(firstName, lastName);
            ButtonContent = "Edit";
            FirstName = firstName;
            LastName = lastName;
            isReadOnly = true;
            EditSaveButton = Edit;
        }
        public void RemoveRecord()
        {
            OnDeleteRecord(this, new DeleteRecordEventArgs(customerRecord.Id));
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
                customerRecord = new CustomerRecord(FirstName, LastName);
            }
            else
            {
                FirstName = customerRecord.FirstName;
                LastName = customerRecord.LastName;
            }
            IsReadOnly = true;
            EditSaveButton = Edit;
            ButtonContent = "Edit";
            return;
        }
    }
}
