using CustomerRecords.Commands;
using CustomerRecords.Events;
using CustomerRecords.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.Networking;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CustomerRecords.ViewModels
{
    public class CustomerRecordsViewModel : BindableBase
    {
        private CustomerRecord editableRecord;
        private CustomerRecord selectedItem;
        private string firstNameField = string.Empty;
        private string lastNameField = string.Empty;
        private ICommand editSaveButtonCommand;
        public ICommand DeleteCommand { get; set; }
        public ObservableCollection<CustomerRecord> Records { get; set; }
        public string FirstNameField
        {
            get => firstNameField;
            set
            {
                if (firstNameField != value)
                {
                    firstNameField = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LastNameField
        {
            get => lastNameField;
            set
            {
                if (lastNameField != value)
                {
                    lastNameField = value;
                    OnPropertyChanged();
                }
            }
        }
        public CustomerRecord SelectedItem
        {
            get => selectedItem;
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand EditSaveButtonCommand
        {
            get => editSaveButtonCommand;
            set
            {
                if (editSaveButtonCommand != value)
                {
                    editSaveButtonCommand = value;
                    OnPropertyChanged();
                }
            }
        }

        public CustomerRecordsViewModel()
        {
            editableRecord = null;
            Records = new ObservableCollection<CustomerRecord>();
            EditSaveButtonCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Remove);
        }

        public void AddToRecords()
        {
            if (FirstNameField.Length > 0 && LastNameField.Length > 0)
            {
                if (Records.Count > 0)
                {
                    var customersRecord = new CustomerRecord
                    {
                        FirstName = FirstNameField,
                        LastName = LastNameField,
                        IsEditMode = false,
                        IsReadOnlyMode = true,
                        ButtonContent = "Edit",
                        Id = Records.Last().Id + 1
                    };
                    Records.Add(customersRecord);
                }
                else
                {
                    var customersRecord = new CustomerRecord()
                    {
                        FirstName = FirstNameField, 
                        LastName = LastNameField,
                        IsEditMode = false,
                        IsReadOnlyMode = true,
                        ButtonContent = "Edit"
                    };
                    Records.Add(customersRecord);
                }

                FirstNameField = string.Empty;
                LastNameField = string.Empty;
            }

        }

        public async void Remove(object sender)
        {
            var customerRecord = sender as CustomerRecord;
            var contentDialog = new ContentDialog()
            {
                Title = "Confirmation",
                Content = "Are you sure to delete?",
                PrimaryButtonText = "Yes",
                CloseButtonText = "Cancel",
            };

            var confirmationResult = await contentDialog.ShowAsync();
            if (confirmationResult == ContentDialogResult.Primary)
            {
                if (editableRecord != null && customerRecord.Id == editableRecord.Id)
                {
                    EditSaveButtonCommand = new RelayCommand(Edit);
                    editableRecord = null;
                }
                Records.Remove(customerRecord);
                SelectedItem = null;
            }
        }
        private void Edit(object sender)
        {
            var selectedCustomerRecord = sender as CustomerRecord;            
            var customerRecord = Records.First(r => r.Id == selectedCustomerRecord.Id);
            editableRecord = new CustomerRecord()
            {
                Id = customerRecord.Id,
                FirstName = customerRecord.FirstName,
                LastName = customerRecord.LastName
            };
            customerRecord.IsReadOnlyMode = false;
            customerRecord.IsEditMode = true;
            customerRecord.ButtonContent = "Save";
            EditSaveButtonCommand = new RelayCommand(Save);
        }
        private async void Save(object sender)
        {
            var newRecord = sender as CustomerRecord;
            if (newRecord.Id != editableRecord.Id)
            {
                return;
            }
            if (newRecord.FirstName == string.Empty || newRecord.LastName == string.Empty)
            {
                return;
            }

            var currentRecord = Records.FirstOrDefault(r => r.Id == editableRecord.Id);
            if (newRecord.FirstName != editableRecord.FirstName || newRecord.LastName != editableRecord.LastName)
            {                
                var contentDialog = new ContentDialog()
                {
                    Title = "Confirmation",
                    Content = "Are you sure to save?",
                    PrimaryButtonText = "Yes",
                    CloseButtonText = "Cancel",
                };
                var confirmationResult = await contentDialog.ShowAsync();

                if (confirmationResult != ContentDialogResult.Primary)
                {
                    currentRecord.FirstName = editableRecord.FirstName;
                    currentRecord.LastName = editableRecord.LastName;
                }
            }

            editableRecord = null;
            currentRecord.IsReadOnlyMode = true;
            currentRecord.IsEditMode = false;
            EditSaveButtonCommand = new RelayCommand(Edit);
            currentRecord.ButtonContent = "Edit";
        }
    }
}
