using CustomerRecords.ViewModels;

namespace CustomerRecords.Models
{
    public class CustomerRecord : BindableBase
    {
        private int id;
        private string firstName;
        private string lastName;
        private bool isEditMode;
        private bool isReadOnlyMode;
        private string buttonContent;
        public int Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
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
    }
}
