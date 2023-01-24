using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerRecords.Models
{
    public class CustomerRecord
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CustomerRecord(string firstName, string lastName)
        {
            FirstName= firstName;
            LastName= lastName;
        }
    }
}
