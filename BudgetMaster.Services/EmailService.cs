using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace BudgetMaster.Services;

public class EmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly SmtpClient _smtpClient;
    public EmailService(ILogger<EmailService> logger, SmtpClient smtpClient)
    {
        _logger = logger;
        _smtpClient = smtpClient;
    }
}
