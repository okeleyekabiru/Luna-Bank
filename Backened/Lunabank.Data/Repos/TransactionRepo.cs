using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lunabank.Data.Entities;
using Lunabank.Data.Helper;
using Lunabank.Data.ResourceParameters;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PagedList<Transactions>> TransactionHistory(TransactionHistoryParameters parameters, string accountNumber)
        {
            var transactions = _context.Transactions as IQueryable<Transactions>;
            if (parameters.FromDate == null)
            {
                transactions = transactions.Where(t => t.AccountNumber == accountNumber).OrderByDescending(t => t.CreatedOn);
            }
            if (parameters.FromDate != null)
            {
                transactions = transactions.Where(t => t.AccountNumber == accountNumber && t.CreatedOn >= parameters.FromDate).OrderByDescending(t => t.CreatedOn);
            }

            return await PagedList<Transactions>.Create(transactions, parameters.PageNumber, parameters.PageSize);

        }

        
    }
}
