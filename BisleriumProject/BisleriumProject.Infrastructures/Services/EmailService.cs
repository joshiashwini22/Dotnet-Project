using BisleriumProject.Application.Common.Interface.IRepositories;
using BisleriumProject.Application.Common.Interface.IServices;
using BisleriumProject.Application.Helpers;
using BisleriumProject.Domain.Auth;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;

namespace BisleriumProject.Infrastructures.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly IUserRepository _userRepository;
        public EmailService(IOptions<EmailConfiguration> emailConfig, IUserRepository userRepository)
        {
            _emailConfig = emailConfig.Value;
            _userRepository = userRepository;
        }
        public Response SendEmail(EmailMessage message, List<string> errors)
        {
            var emails = message.To.Select(x => x.Address).ToList();

            if (emails.Count == 0)
            {
                errors.Add("Please enter your email.");
                return new Response(null, errors, HttpStatusCode.BadRequest);
            }


            foreach (var email in emails)
            {
                //var user = _userRepository.GetAll(email).FirstOrDefault();

                var user = "";
                if (user == null)
                {
                    errors.Add($"User with email {email} does not exist.");
                    return new Response(null, errors, HttpStatusCode.BadRequest);
                }

                var emailMessage = CreateEmailMessage(message);
                Send(emailMessage);
            }

            return new Response($"Emails sent to {string.Join(", ", emails)}", null, HttpStatusCode.OK);
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);
            await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            var bodyBuilder = new BodyBuilder() { HtmlBody = message.Content };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);
                    client.Send(mailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}

