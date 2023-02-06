using CustomerRecords.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;

namespace CustomerRecords.ViewModels
{
    public class CustomerRecordsViewModel : BindableBase
    {
        private CustomerRecordViewModel record;
        private CustomerRecordViewModel selectedItem;

        public ObservableCollection<CustomerRecordViewModel> Records { get; set; }
        public string FirstName
        {
            get => record.Record.FirstName;
            set
            {
                if (record.Record.FirstName != value)
                {
                    record.Record.FirstName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string LastName
        {
            get => record.Record.LastName;
            set
            {
                if (record.Record.LastName != value)
                {
                    record.Record.LastName = value;
                    OnPropertyChanged();
                }
            }
        }
        public CustomerRecordViewModel SelectedItem
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

        public CustomerRecordsViewModel()
        {
            record = new CustomerRecordViewModel(string.Empty, string.Empty);
            Records = new ObservableCollection<CustomerRecordViewModel>();
        }

        public void AddToRecords()
        {
            if (FirstName.Length > 0 && LastName.Length > 0)
            {
                if (Records.Count > 0)
                {
                    var customersRecord = new CustomerRecordViewModel(FirstName, LastName);
                    customersRecord.DeleteRecord += Remove;
                    customersRecord.Record.Id = Records.Last().Record.Id + 1;
                    Records.Add(customersRecord);
                }
                else
                {
                    var customersRecord = new CustomerRecordViewModel(FirstName, LastName);
                    customersRecord.DeleteRecord += Remove;
                    Records.Add(customersRecord);
                }

                FirstName = string.Empty;
                LastName = string.Empty;
            }

        }

        public async void Remove(object sender, DeleteRecordEventArgs e)
        {
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
                Records.Remove(Records.First(r => r.Record.Id == e.RecordId));
                SelectedItem = null;
            }
        }
    }
}
