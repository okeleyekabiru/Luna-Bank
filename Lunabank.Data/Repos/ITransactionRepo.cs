using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lunabank.Data.Entities;
using Lunabank.Data.Helper;
using Lunabank.Data.Models;
using Lunabank.Data.ResourceParameters;

namespace Lunabank.Data.Repos
{
  public  interface ITransactionRepo
  {
      void create(Transactions transaction);
      Task<Transactions> GetTransaction(Guid transactionId);
      Task<PagedList<Transactions>> TransactionHistory(TransactionHistoryParameters transactionHistoryParameters, string accountNumber);
  }
}
