using BankingKata.Operations;

namespace BankingKata.History
{
    public class StatementItem
    {
        public Operation Operation { get; }

        public int CurrentBalance { get; }

        public StatementItem(Operation operation, int currentBalance)
        {
            Operation = operation;
            CurrentBalance = currentBalance;
        }
    }
}