using BisleriumProject.Application.Helpers;
using BisleriumProject.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BisleriumProject.Application.Common.Interface.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string toDisplayName, string subject, string htmlBody);
    }
}
