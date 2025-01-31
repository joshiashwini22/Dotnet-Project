﻿using BisleriumProject.Application.Common.Interface.IServices;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace BisleriumProject.Infrastructures.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string toDisplayName, string subject, string htmlBody)
        {
            var emailMessage = new MimeMessage();

            // Fetching 'From' display name and email from configuration
            string fromDisplayName = _configuration["EmailConfiguration:FromDisplayName"];
            string fromEmail = _configuration["EmailConfiguration:FromEmail"];
            emailMessage.From.Add(new MailboxAddress(fromDisplayName, fromEmail));

            // Assuming 'to' includes the full email address and 'toDisplayName' is provided
            emailMessage.To.Add(new MailboxAddress(toDisplayName, to));

            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlBody
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_configuration["EmailConfiguration:SetupServer"], int.Parse(_configuration["EmailConfiguration:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_configuration["EmailConfiguration:Username"], _configuration["EmailConfiguration:Password"]);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }



    }
}

