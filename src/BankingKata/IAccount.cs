namespace BankingKata
{
    public interface IAccount
    {
        int GetBalance();

        void Deposit(int amount);

        void Withdraw(int amount);
        
        string PrintStatement();
    }
}