using System.Collections.Generic;
using BankingKata.History;

namespace BankingKata
{
    public interface IAccount
    {
        int GetBalance();

        IEnumerable<StatementItem> GetStatement();

        void Deposit(int amount);

        void Withdraw(int amount);
        
        string PrintStatement();
    }
}