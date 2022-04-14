using Horeb.MoneySaver.Persistency.EntityDataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Persistency
{
    public class DapDbContext: DbContext
    {
        public DapDbContext() {}

        public DapDbContext(DbContextOptions options)
            : base(options)
        {}

        public DbSet<WalletModel> Wallets { get; set; }
        public DbSet<TransactionCategoryModel> TransactionCategories { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }
    }
}
