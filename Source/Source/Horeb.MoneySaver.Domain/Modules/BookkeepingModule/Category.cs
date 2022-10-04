using Horeb.Infrastructure.Data;

namespace Horeb.Domain.TransactionModule
{
    public class Category : BaseEntity
    {
        public Category(string name) : base() {            
            Name = name;
        }

        public string Name { get; set; }
        
        public int WalletId { get; set; }
        
        public CategoryType Type { get; set; }

        public decimal ConvertAmmountToExpenseOrIncome(decimal ammount)
        {
            if (ammount < 0) {
                return ammount * -1;
            }

            return Type == CategoryType.Expense ? ammount * -1 : ammount;
        }
    }

    public enum CategoryType
    {
        Income = 1,
        Expense = 2,
    }
}
