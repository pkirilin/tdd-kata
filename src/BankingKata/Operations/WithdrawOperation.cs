using System;

namespace BankingKata.Operations
{
    public class WithdrawOperation : Operation
    {
        public WithdrawOperation(ICurrency currency) : base(currency)
        {
        }

        public override int Apply(IAccount account)
        {
            var balance = account.GetBalance();
            var amount = Currency.GetAmount();
            
            if (balance < amount)
                throw new InvalidOperationException($"Cannot withdraw amount = {amount} from balance = {balance}");

            return balance - amount;
        }
        
        public override string GetVisualAmount()
        {
            return $"-{Currency.GetAmount()}";
        }
    }
}