using HiddenVilla.notify.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVilla.notify.Implementation
{
    public class Messages : IMessages
    {
        private readonly EmailConfigs emailConfigs;
        private bool disposed = false;

        public Messages(IOptions<EmailConfigs> emailConfigs)
        {
            this.emailConfigs = emailConfigs.Value;
        }

        private EmailConfigs EmailConfigs => this.emailConfigs;

        public void SendEmail(string email, string subject, string body)
        {
            using SendEmailViaSmtp smtp = new SendEmailViaSmtp(this.EmailConfigs);
            smtp.Send(email, subject, body);
        }


        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

    }
}
