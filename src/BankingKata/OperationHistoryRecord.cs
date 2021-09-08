using BankingKata.Operations;

namespace BankingKata
{
    public class OperationHistoryRecord
    {
        public Operation Operation { get; }

        public int CurrentBalance { get; set; }

        public OperationHistoryRecord(Operation operation, int currentBalance)
        {
            Operation = operation;
            CurrentBalance = currentBalance;
        }
    }
}