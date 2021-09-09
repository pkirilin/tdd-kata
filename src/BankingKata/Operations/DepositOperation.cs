namespace BankingKata.Operations
{
    public class DepositOperation : Operation
    {
        public DepositOperation(ICurrency currency) : base(currency)
        {
        }
        
        public override int Apply(IAccount account)
        {
            return account.GetBalance() + Currency.GetAmount();
        }

        public override string GetVisualAmount()
        {
            return $"+{Currency.GetAmount()}";
        }
    }
}