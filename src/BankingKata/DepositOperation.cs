using BankingKata.Abstractions;

namespace BankingKata
{
    public class DepositOperation : Operation
    {
        public DepositOperation(int amount) : base(amount)
        {
        }
        
        public override int Apply(IAccount account)
        {
            return account.Balance + Amount;
        }

        public override string GetVisualAmount()
        {
            return $"+{Amount}";
        }
    }
}