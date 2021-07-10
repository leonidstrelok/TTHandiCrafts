using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using TTHandiCrafts.Infrastructure.Interfaces.Interfaces;
using TTHandiCrafts.Infrastructure.Interfaces.Models;
using TTHandiCrafts.Models;

namespace TTHandiCrafts.Services
{
    public class MailKitEmailSender : IEmailSender
    {
        readonly EmailOptions options;

        public MailKitEmailSender(IOptions<EmailOptions> options)
        {
            this.options = options.Value;
        }

        public async Task SendEmailAsync(Email email, CancellationToken cancellationToken = default)
        {
            var emailBody = new BodyBuilder
            {
                HtmlBody = email.Message
            };

            var mimeMessage = MimeMessage(email.EmailTo, email.Subject);

            mimeMessage.Body = emailBody.ToMessageBody();

            await Send(mimeMessage);
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mimeMessage = MimeMessage(email, subject);

            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {Text = htmlMessage};

            await Send(mimeMessage);
        }

        private MimeMessage MimeMessage(string email, string subject)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(MailboxAddress.Parse(options.Sender));
            mimeMessage.To.Add(MailboxAddress.Parse(email));
            mimeMessage.Subject = subject;


            return mimeMessage;
        }

        private async Task Send(MimeMessage mimeMessage)
        {
            using SmtpClient smtpClient = new SmtpClient();

            smtpClient.ServerCertificateValidationCallback += (s, c, h, e) => true;
            await smtpClient.ConnectAsync(options.SmtpServer, options.Port, options.UseSsl);
            await smtpClient.AuthenticateAsync(options.UserName, options.Password);
            await smtpClient.SendAsync(mimeMessage);
            await smtpClient.DisconnectAsync(true);
        }
    }
}