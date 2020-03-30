using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LunaBank.Api.Services
{
    public interface IEmailService
    {
        void SendMail(string email, string message, string subject);
    }
}
