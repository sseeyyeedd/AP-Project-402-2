using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SofreDaar.Helpers
{
    public class Email
    {
        public static string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public static async Task SendVerificationEmailAsync(string recipientEmail, string code)
        {
            try
            {
                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("mail@sseeyyeedd.ir"),
                    Subject = "Email Verification",
                    Body = "Your verification code is: " + code
                };
                mail.To.Add(recipientEmail);

                using (SmtpClient smtpClient = new SmtpClient("mail.sseeyyeedd.ir", 587)) // Using port 587 for SMTP
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("mail@sseeyyeedd.ir", "]@Z-N,AnI=,u");
                    smtpClient.EnableSsl = false; // Non-SSL configuration
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    // Logging the attempt to send email
                    Console.WriteLine($"Attempting to send email to {recipientEmail}");

                    await smtpClient.SendMailAsync(mail);

                    // Logging success
                    Console.WriteLine("Verification email sent successfully.");
                }

                Console.WriteLine("Verification email sent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending verification email: " + ex.Message);
            }
        }
    }
}
