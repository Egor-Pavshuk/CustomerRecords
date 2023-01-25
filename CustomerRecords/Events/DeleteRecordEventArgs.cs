using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRecords.Events
{
    public class DeleteRecordEventArgs
    {
        private readonly int _id;
        public DeleteRecordEventArgs(int id)
        {
            _id = id;
        }

        public int RecordId { get => _id; }
    }
}
