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
