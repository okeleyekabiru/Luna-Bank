using System;
using System.Collections.Generic;
using System.Text;
using Lunabank.Data.Entities;

namespace Lunabank.Data.Repos
{
  public  interface ITransactionRepo
  {
      void create(Transactions transaction);
  }
}
