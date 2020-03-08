using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lunabank.Data.Entities;

namespace Lunabank.Data.Repos
{
  public  interface ITransactionRepo
  {
      void create(Transactions transaction);
      Task<Transactions> GetTransaction(Guid transactionId);
  }
}
