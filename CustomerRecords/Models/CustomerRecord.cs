namespace CustomerRecords.Models
{
    public class CustomerRecord
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CustomerRecord(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
