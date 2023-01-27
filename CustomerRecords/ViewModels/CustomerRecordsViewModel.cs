using CustomerRecords.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;

namespace CustomerRecords.ViewModels
{
    public class CustomerRecordsViewModel : INotifyPropertyChanged
    {
        private CustomerRecordViewModel record;
        private int selectedIndex = -1;
        private bool isRemoveButtonEnable;

        public ObservableCollection<CustomerRecordViewModel> Records { get; set; }
        public string FirstName
        {
            get => record.Record.FirstName;
            set
            {
                record.Record.FirstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get => record.Record.LastName;
            set
            {
                record.Record.LastName = value;
                OnPropertyChanged();
            }
        }
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                if (selectedIndex != -1)
                {
                    IsRemoveButtonEnable = true;
                }
                else
                {
                    IsRemoveButtonEnable = false;
                }

                OnPropertyChanged();
            }
        }
        public bool IsRemoveButtonEnable
        {
            get => isRemoveButtonEnable;
            set
            {
                isRemoveButtonEnable = value;
                OnPropertyChanged();
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
                SelectedIndex = -1;
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
