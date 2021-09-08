using System;

namespace BankingKata.Operations
{
    public class WithdrawOperation : Operation
    {
        public WithdrawOperation(int amount) : base(amount)
        {
        }

        public override int Apply(IAccount account)
        {
            var balance = account.GetBalance();
            
            if (balance < Amount)
                throw new InvalidOperationException($"Cannot withdraw amount = {Amount} from balance = {balance}");

            return balance - Amount;
        }
        
        public override string GetVisualAmount()
        {
            return $"-{Amount}";
        }
    }
}