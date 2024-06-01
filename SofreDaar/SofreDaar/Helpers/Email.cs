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
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("youremail@gmail.com");
            mail.To.Add(recipientEmail);
            mail.Subject = "Email Verification";
            mail.Body = "Your verification code is: " + code;
            smtpClient.Port = 587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("youremail@gmail.com", "yourpassword");
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