using Horeb.Domain.TransactionModule;
using System;


namespace Horeb.MoneySaver.Service
{
    public interface IBookkeepingService
    {        
        Task RecordTransactionAsync(Transaction transaction);      
    }
}
