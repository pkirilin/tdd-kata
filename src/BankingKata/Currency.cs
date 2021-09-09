using System;

namespace BankingKata
{
    public class Currency
    {
        public int Amount { get; }

        public Currency()
        {
            Amount = 0;
        }

        public Currency(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Currency amount cannot be less than zero");
            
            Amount = amount;
        }
    }
}