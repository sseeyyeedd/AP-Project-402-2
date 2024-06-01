using System.Net;
using System.Net.Mail;

namespace SofreDaar.Helpers;

public class Email
{
    static string GenerateVerificationCode()
    {
        Random random = new Random();
        return random.Next(100000, 999999).ToString();
    }
    static void SendVerificationEmail(string recipientEmail, string code)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpClient = new SmtpClient("mail.sseeyyeedd.ir");
            mail.From = new MailAddress("mail@sseeyyeedd.ir");
            mail.To.Add(recipientEmail);
            mail.Subject = "Email Verification";
            mail.Body = "Your verification code is: " + code;
            smtpClient.Port = 465;
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