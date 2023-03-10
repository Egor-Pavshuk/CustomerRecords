using CustomerRecords.Events;
using CustomerRecords.Models;
using System;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CustomerRecords.ViewModels
{
    public class CustomerRecordViewModel : BindableBase
    {
        private CustomerRecord customerRecord;
        private bool isEditMode;
        private bool isReadOnlyMode;
        private string buttonContent;
        private string firstName;
        private string lastName;

        public RoutedEventHandler EditSaveButton { get; private set; }
        public event EventHandler<DeleteRecordEventArgs> RecordDeleted;

        public bool IsEditMode
        {
            get => isEditMode;
            set
            {
                if (isEditMode != value)
                {
                    isEditMode = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool IsReadOnlyMode
        {
            get => isReadOnlyMode;
            set
            {
                if (isReadOnlyMode != value)
                {
                    isReadOnlyMode = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ButtonContent
        {
            get => buttonContent;
            set
            {
                if (buttonContent != value)
                {
                    buttonContent = value;
                    OnPropertyChanged();
                }
            }
        }
        public CustomerRecord Record
        {
            get => customerRecord;
            set
            {
                if (customerRecord != value)
                {
                    customerRecord = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName
        {
            get => firstName;
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged();
                }
            }
        }
        public CustomerRecordViewModel(string firstName, string lastName)
        {
            customerRecord = new CustomerRecord
            {
                FirstName = firstName,
                LastName = lastName
            };
            ButtonContent = "Edit";
            EditSaveButton = Edit;
            FirstName = firstName;
            LastName = lastName;
            isEditMode = false;
            isReadOnlyMode = true;
        }
        public void RemoveRecord()
        {
            OnRecordDeleted(this, new DeleteRecordEventArgs(customerRecord.Id));
        }
        private void OnRecordDeleted(object sender, DeleteRecordEventArgs e)
        {
            var temp = Volatile.Read(ref RecordDeleted);
            if (temp != null)
            {
                temp(sender, e);
            }
        }
        private void Edit(object sender, RoutedEventArgs e)
        {
            IsReadOnlyMode = false;
            IsEditMode = true;
            ButtonContent = "Save";
            EditSaveButton = Save;
        }
        private async void Save(object sender, RoutedEventArgs e)
        {
            if (firstName != customerRecord.FirstName || lastName != customerRecord.LastName)
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
                    customerRecord = new CustomerRecord
                    {
                        FirstName = firstName,
                        LastName = lastName
                    };
                }
                else
                {
                    FirstName = customerRecord.FirstName;
                    LastName = customerRecord.LastName;
                }
            }
            IsReadOnlyMode = true;
            IsEditMode = false;
            EditSaveButton = Edit;
            ButtonContent = "Edit";
            return;
        }
    }
}
