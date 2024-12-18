using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVilla.notify
{
    public interface IMessages
    {
        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// <param name="email">The last name to join.</param>
        /// <param name="subject">The subject to join.</param>
        /// <param name="body">The body to join.</param>
        /// </summary>
        void SendEmail(string email, string subject, string body);
    }
}
