using Horeb.Domain.TransactionModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horeb.MoneySaver.Service
{
    public interface ICategoryService: IBaseCrudService<TransactionCategory>
    {
    }
}
