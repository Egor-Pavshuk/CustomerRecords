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
        private CustomerRecordViewModel _record;
        private int _selectedIndex = -1;
        private bool _isRemoveButtonEnable;

        public ObservableCollection<CustomerRecordViewModel> Records { get; set; }
        public string FirstName
        {
            get => _record.Record.FirstName;
            set
            {
                _record.Record.FirstName = value;
                OnPropertyChanged();
            }
        }
        public string LastName
        {
            get => _record.Record.LastName;
            set
            {
                _record.Record.LastName = value;
                OnPropertyChanged();
            }
        }
        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                if (_selectedIndex != -1)
                {
                    IsRemoveButtonEnable = true;
                }
                else
                    IsRemoveButtonEnable = false;

                OnPropertyChanged();
            }
        }
        public bool IsRemoveButtonEnable
        {
            get => _isRemoveButtonEnable;
            set
            {
                _isRemoveButtonEnable = value;
                OnPropertyChanged();
            }
        }

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
                    customersRecord.Record.Id = Records.Last().Record.Id + 1;
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

        public async void Remove()
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
                Records.RemoveAt(_selectedIndex);
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
