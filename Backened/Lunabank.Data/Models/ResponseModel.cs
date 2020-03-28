using System;
using System.Collections.Generic;
using System.Text;

namespace Lunabank.Data.Models
{
    public class ResponseModel<T>
    {
        public string Status { get; set; }
        public T Data { get; set; }
    }
}
