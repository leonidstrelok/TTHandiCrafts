namespace TTHandiCrafts.Infrastructure.Interfaces.Models
{
    public class Email
    {
        public Email(string to, string subject)
        {
            EmailTo = to;
            Subject = subject;
        }

        public string EmailTo { get; }
        public string Subject { get; }
        public string Message { get; set; }
    }
}