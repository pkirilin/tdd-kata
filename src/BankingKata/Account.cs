using System.Collections.Generic;
using System.Text;
using BankingKata.Operations;

namespace BankingKata
{
    public class Account : IAccount
    {
        private int _balance;
        private readonly List<OperationHistoryRecord> _operationsHistory;

        public Account()
        {
            _balance = 0;
            _operationsHistory = new List<OperationHistoryRecord>();
        }

        public int GetBalance() => _balance;

        public void Deposit(int amount)
        {
            // TODO: validate amount
            PerformOperation(new DepositOperation(amount));
        }
        
        public void Withdraw(int amount)
        {
            // TODO: validate amount
            PerformOperation(new WithdrawOperation(amount));
        }

        public string PrintStatement()
        {
            // TODO: move logic to printer class
            
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
            _balance = operation.Apply(this);
            _operationsHistory.Add(new OperationHistoryRecord(operation, _balance));
        }
    }
}
