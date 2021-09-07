using System.Collections.Generic;
using System.Text;
using BankingKata.Abstractions;

namespace BankingKata
{
    public class Account : IAccount
    {
        private readonly List<OperationHistoryRecord> _operationsHistory;

        public Account()
        {
            Balance = 0;
            _operationsHistory = new List<OperationHistoryRecord>();
        }

        public int Balance { get; private set; }

        public void Deposit(int amount)
        {
            PerformOperation(new DepositOperation(amount));
        }
        
        public void Withdraw(int amount)
        {
            PerformOperation(new WithdrawOperation(amount));
        }

        public string PrintStatement()
        {
            var statementBuilder = new StringBuilder();
            
            foreach (var historyRecord in _operationsHistory)
            {
                statementBuilder.AppendFormat("Date: {0:yyyy-MM-dd}, amount: {1}, balance: {2}\n",
                    historyRecord.Operation.Date,
                    historyRecord.Operation.GetVisualAmount(),
                    historyRecord.CurrentBalance);
            }

            return statementBuilder.ToString();
        }

        private void PerformOperation(Operation operation)
        {
            Balance = operation.Apply(this);
            _operationsHistory.Add(new OperationHistoryRecord(operation, Balance));
        }
    }
}
