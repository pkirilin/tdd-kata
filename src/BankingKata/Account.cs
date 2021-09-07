using System;
using System.Collections.Generic;
using System.Text;

namespace BankingKata
{
    public class Account
    {
        private int _balance;
        private readonly List<Operation> _operations;

        public Account()
        {
            _balance = 0;
            _operations = new List<Operation>();
        }
        
        public void Deposit(int amount)
        {
            _balance += amount;
            _operations.Add(new Operation
            {
                Date = DateTime.Now,
                Type = OperationType.Deposit,
                Amount = amount,
                Balance = _balance
            });
        }
        
        public void Withdraw(int amount)
        {
            _balance -= amount;
            _operations.Add(new Operation
            {
                Date = DateTime.Now,
                Type = OperationType.Withdraw,
                Amount = amount,
                Balance = _balance
            });
        }

        public string PrintStatement()
        {
            var statementBuilder = new StringBuilder();
            
            foreach (var operation in _operations)
            {
                var signSymbol = operation.Type == OperationType.Deposit ? '+' : '-';

                statementBuilder.AppendFormat("Date: {0:yyyy-MM-dd}, amount: {1}{2}, balance: {3}\n",
                    operation.Date,
                    signSymbol,
                    operation.Amount,
                    operation.Balance);
            }

            return statementBuilder.ToString();
        }
    }
}
