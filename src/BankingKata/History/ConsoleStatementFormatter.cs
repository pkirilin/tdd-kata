using System.Text;

namespace BankingKata.History
{
    public class ConsoleStatementFormatter : IStatementFormatter
    {
        public string Format(IAccount account)
        {
            var statementBuilder = new StringBuilder();
            
            foreach (var statementItem in account.GetStatement())
            {
                statementBuilder.AppendFormat("Date: {0:yyyy-MM-dd}, amount: {1}, balance: {2}\n",
                    statementItem.Operation.Date,
                    statementItem.Operation.GetVisualAmount(),
                    statementItem.CurrentBalance);
            }
            
            return statementBuilder.ToString();
        }
    }
}