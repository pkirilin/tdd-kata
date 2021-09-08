using System;

namespace BankingKata.Operations
{
    public abstract class Operation
    {
        public int Amount { get; }

        public DateTime Date { get; }

        protected Operation(int amount)
        {
            Amount = amount;
            Date = DateTime.Now;
        }
        
        public abstract int Apply(IAccount account);

        public abstract string GetVisualAmount();
    }
}