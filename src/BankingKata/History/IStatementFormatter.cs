namespace BankingKata.History
{
    public interface IStatementFormatter
    {
        string Format(IAccount account);
    }
}