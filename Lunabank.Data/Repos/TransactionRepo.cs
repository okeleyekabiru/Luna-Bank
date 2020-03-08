using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
            transaction.TransactionId = Guid.NewGuid();
            _context.Transactions.Add(transaction);
        }

        public async Task<Transactions> GetTransaction(Guid transactionId)
        {
            if (transactionId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(transactionId));
            }

            return await _context.Transactions.FindAsync(transactionId);
        }
    }
}
