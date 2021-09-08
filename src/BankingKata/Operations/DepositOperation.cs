namespace BankingKata.Operations
{
    public class DepositOperation : Operation
    {
        public DepositOperation(int amount) : base(amount)
        {
        }
        
        public override int Apply(IAccount account)
        {
            return account.GetBalance() + Amount;
        }

        public override string GetVisualAmount()
        {
            return $"+{Amount}";
        }
    }
}