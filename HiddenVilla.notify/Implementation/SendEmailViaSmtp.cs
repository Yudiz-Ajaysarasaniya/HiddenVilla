using HiddenVilla.notify.Models;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVilla.notify.Implementation
{
    public class SendEmailViaSmtp : IDisposable
    {
        private readonly EmailConfigs emailConfig;
        private bool disposed = false;

        public SendEmailViaSmtp(EmailConfigs emailConfigs)
        {
            this.emailConfig = emailConfigs;
        }


        #region Send

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="receiverEmail">The first name to join.</param>
        /// <param name="subject">The last name to join.</param>
        /// <param name="body">The body to join.</param>
        /// <param name="emailCC">The emailcc name to join.</param>
        /// <param name="emailBCC">The emailbcc to join.</param>
        public void Send(string receiverEmail, string subject, string body, string? emailCC = null, string? emailBCC = null)
        {
            if (string.IsNullOrEmpty(receiverEmail))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(receiverEmail));
            }

            if (string.IsNullOrEmpty(this.emailConfig?.SenderEmail))
            {
                Exception exception = new Exception($"Please specify the Sender Email in AppSettings.");
                throw exception;
            }

            if (string.IsNullOrEmpty(this.emailConfig.SmtpAddress))
            {
                Exception exception = new Exception($"Please specify the SMTP Address in AppSettings.");
                throw exception;
            }

            if (string.IsNullOrEmpty(this.emailConfig.Password))
            {
                Exception exception = new Exception($"Please specify the Password in AppSettings.");
                throw exception;
            }

            if (this.emailConfig.Port == 0)
            {
                Exception exception = new Exception($"Please Specify the Port in AppSettings.");
                throw exception;
            }

            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(this.emailConfig.DisplayName, this.emailConfig.SenderEmail));
                message.To.Add(new MailboxAddress(string.Empty, receiverEmail));
                message.Subject = subject;

                // Add CC recipients
                if (!string.IsNullOrEmpty(emailCC))
                {
                    foreach (var cc in emailCC.Split(',').Select(e => e.Trim()))
                    {
                        message.Cc.Add(MailboxAddress.Parse(cc));
                    }
                }

                // Add BCC recipients
                if (!string.IsNullOrEmpty(emailBCC))
                {
                    foreach (var bcc in emailBCC.Split(',').Select(e => e.Trim()))
                    {
                        message.Bcc.Add(MailboxAddress.Parse(bcc));
                    }
                }

                message.Body = new TextPart("html") { Text = body };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect(this.emailConfig.SmtpAddress, this.emailConfig.Port, SecureSocketOptions.Auto);

                    // Note: Since you don't want to use Google OAuth 2.0
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    client.Authenticate(this.emailConfig.SenderEmail, this.emailConfig.Password);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error sending email: {ex}");
                throw; // Re-throw the exception after logging
            }
        }

        #endregion Send


        #region House Keeping

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this); // Ensures that the finalizer is not called after the object is disposed
        }

        /// <summary>
        /// Dispose of the managed and unmanaged resources.
        /// </summary>
        /// <param name="disposing">If true, dispose managed resources; otherwise, only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here
                    // For example, if you have any IDisposable members, dispose them here
                    // e.g. emailConfig.Dispose(); if it's IDisposable (assuming)
                }

                // Dispose unmanaged resources here if needed
                this.disposed = true;
            }
        }
        #endregion
    }
}
