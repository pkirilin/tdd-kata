using System;

namespace BankingKata
{
    public class Currency : ICurrency
    {
        private readonly int _amount;
        
        public Currency()
        {
            _amount = 0;
        }

        public Currency(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("Currency amount cannot be less than zero");

            _amount = amount;
        }

        public int GetAmount() => _amount;
    }
}