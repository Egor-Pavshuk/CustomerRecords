using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerRecords.Events
{
    public class DeleteRecordEventArgs
    {
        private readonly int id;
        public DeleteRecordEventArgs(int recordId)
        {
            id = recordId;
        }

        public int RecordId { get => id; }
    }
}
