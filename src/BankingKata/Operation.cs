using System;

namespace BankingKata
{
    public class Operation
    {
        public DateTime Date { get; set; }

        public OperationType Type { get; set; }

        public int Amount { get; set; }

        public int Balance { get; set; }
    }
}