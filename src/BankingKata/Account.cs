using System.Collections.Generic;
using BankingKata.History;
using BankingKata.Operations;

namespace BankingKata
{
    public class Account : IAccount
    {
        private int _balance;
        private readonly List<StatementItem> _statementItems;

        public Account()
        {
            _balance = 0;
            _statementItems = new List<StatementItem>();
        }

        public int GetBalance() => _balance;

        public IEnumerable<StatementItem> GetStatement() => _statementItems;

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
            return FormatStatement(new ConsoleStatementFormatter());
        }

        private void PerformOperation(Operation operation)
        {
            _balance = operation.Apply(this);
            _statementItems.Add(new StatementItem(operation, _balance));
        }

        private string FormatStatement(IStatementFormatter statementFormatter)
        {
            return statementFormatter.Format(this);
        }
    }
}
