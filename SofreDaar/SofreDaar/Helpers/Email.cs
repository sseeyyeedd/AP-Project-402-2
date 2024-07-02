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
        public static string GeneratePaymentCode()
        {
            Random random = new Random();
            return random.Next(100000000, 999999999).ToString();
        }
        public static async Task SendVerificationEmailAsync(string recipientEmail, string code)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("mail.sseeyyeedd.ir");
                mail.From = new MailAddress("mail@sseeyyeedd.ir");
                mail.To.Add(recipientEmail);
                mail.Subject = "Email Verification";
                mail.Body = "Your verification code is: " + code;
                smtpClient.Port = 587;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("mail@sseeyyeedd.ir", "]@Z-N,AnI=,u");
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);

                Console.WriteLine("Verification email sent.");
                
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending verification email: " + ex.Message);
            }
        }
    }
}
