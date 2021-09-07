using BankingKata.Abstractions;

namespace BankingKata
{
    public class WithdrawOperation : Operation
    {
        public WithdrawOperation(int amount) : base(amount)
        {
        }

        public override int Apply(IAccount account)
        {
            return account.Balance - Amount;
        }
        
        public override string GetVisualAmount()
        {
            return $"-{Amount}";
        }
    }
}