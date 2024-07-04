using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using SofreDaar.Models;

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
        public static async Task SendBrnzeSubscriptionPaymentEmailAsync(string recipientEmail)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("mail.sseeyyeedd.ir");
                mail.From = new MailAddress("mail@sseeyyeedd.ir");
                mail.To.Add(recipientEmail);
                mail.Subject = "سفره دار";
                mail.Body = " سرویس برنزی شما فعال شد. سرویس ویژه برنزی به قیمت 100 تومان، امکان 2 رزرو در ماه با زمان رزرو یک ساعت و آستانه جریمه نیم ساعته را ارائه میدهد.";
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
        public static async Task SendSilverSubscriptionPaymentEmailAsync(string recipientEmail)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("mail.sseeyyeedd.ir");
                mail.From = new MailAddress("mail@sseeyyeedd.ir");
                mail.To.Add(recipientEmail);
                mail.Subject = "سفره دار";
                mail.Body = " سرویس نقره ای شما فعال شد. سرویس ویژه نقره ای به قیمت 150 تومان، امکان 5 رزرو در ماه با زمان رزرو یک ساعت و نیمه و آستانه جریمه نیم ساعته را ارائه میدهد.";
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
        public static async Task SendGoldSubscriptionPaymentEmailAsync(string recipientEmail)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("mail.sseeyyeedd.ir");
                mail.From = new MailAddress("mail@sseeyyeedd.ir");
                mail.To.Add(recipientEmail);
                mail.Subject = "سفره دار";
                mail.Body = " سرویس طلایی شما فعال شد. سرویس ویژه طلایی به قیمت 300 تومان، امکان 15 رزرو در ماه با زمان رزرو سه ساعت و آستانه جریمه 15 دقیقه ای را ارائه میدهد.";
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
        public static async Task SendOrderPaymentEmailAsync(string recipientEmail,OrderItem orderItem,string code)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient("mail.sseeyyeedd.ir");
                mail.From = new MailAddress("mail@sseeyyeedd.ir");
                mail.To.Add(recipientEmail);
                mail.Subject = "سفره دار";
                mail.Body = "";
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
