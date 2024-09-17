using System.Net.Mail;
using System.Net;

namespace E_commerce
{
    public class EmailHelper
    {
        private string to { get; set; }
        private string subject { get; set; }
        private string body { get; set; }
        public EmailHelper(string to, string subject, string body)
        {
            this.to = to;
            this.subject = subject;
            this.body = body;
        }
        public void Send()
        {
            try
            {

                var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential
                        ("55929d1642bc48", "74ca79a6660b09"),
                    EnableSsl = true
                };
                client.Send("from@example.com", to, subject, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
        }
    }
}
