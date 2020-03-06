using System;
using System.Collections.Generic;
using System.Text;
using Lunabank.Data.Entities;

namespace Lunabank.Data.Repos
{
  public  class TransactionRepo:ITransactionRepo
    {
        private readonly DataContext _context;

        public TransactionRepo(DataContext context)
        {
            _context = context;
        }
        public void create(Transactions transaction)
        {
            transaction.TransactionId = new Guid();
            _context.Transactions.Add(transaction);
        }
    }
}
