namespace BankingKata.Abstractions
{
    public interface IAccount
    {
        int Balance { get; }

        void Deposit(int amount);

        void Withdraw(int amount);
        
        string PrintStatement();
    }
}