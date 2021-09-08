namespace BankingKata.Operations
{
    public class WithdrawOperation : Operation
    {
        public WithdrawOperation(int amount) : base(amount)
        {
        }

        public override int Apply(IAccount account)
        {
            return account.GetBalance() - Amount;
        }
        
        public override string GetVisualAmount()
        {
            return $"-{Amount}";
        }
    }
}