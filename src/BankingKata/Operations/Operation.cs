using System;

namespace BankingKata.Operations
{
    public abstract class Operation
    {
        protected ICurrency Currency { get; }

        public DateTime Date { get; }

        protected Operation(ICurrency currency)
        {
            Currency = currency;
            Date = DateTime.Now;
        }
        
        public abstract int Apply(IAccount account);

        public abstract string GetVisualAmount();
    }
}