using Horeb.Infrastructure.Data;

namespace Horeb.Domain.TransactionModule
{
    public class TransactionCategory : BaseEntity
    {
        public TransactionCategory(string name) : base() {            
            Name = name;
        }

        public string Name { get; set; }
        
        public int WalletId { get; set; }
        
        public bool IsIncome { get; set; }

        public decimal ConvertAmmountToExpenseOrIncome(decimal ammount)
        {
            if (ammount < 0) {
                return ammount * -1;
            }

            return IsIncome ? ammount : ammount * -1;
        }
    }
}
