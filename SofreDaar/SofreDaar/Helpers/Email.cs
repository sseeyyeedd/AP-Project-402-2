using System.Net;
using System.Net.Mail;
using System.Text;
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
        public static void SendOrderItemEmail(string recipientEmail,List<OrderItem> orderItems)
        {
            try
            {
             MailMessage mail = new MailMessage();
             SmtpClient smtpClient = new SmtpClient("mail.sseeyyeedd.ir");
        
             // Set email sender and recipient
             mail.From = new MailAddress("mail@sseeyyeedd.ir");
             mail.To.Add(recipientEmail); // Replace with the actual recipient email
        
             // Set email subject
             mail.Subject = "Order Item Details";
        
             // Create the HTML body
             StringBuilder sb = new StringBuilder();
             sb.Append("<html>");
             sb.Append("<body>");
             sb.Append("<h2>Order Item Details</h2>");
             sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border-collapse: collapse;'>");
             sb.Append("<thead>");
             sb.Append("<tr>");
             sb.Append("<th>Food Name</th>");
             sb.Append("<th>Quantity</th>");
             sb.Append("<th>Unit Price</th>");
             sb.Append("<th>Total Price</th>");
             sb.Append("</tr>");
             sb.Append("</thead>");
             sb.Append("<tbody>");
        
             // Add order item details to the table
             
                foreach (OrderItem item in orderItems) {
                    sb.Append("<tr>");
                    sb.Append($"<td>{item.Food.Name}</td>");
                    sb.Append($"<td>{item.Count}</td>");
                    sb.Append($"<td>{item.Food.Price}</td>");
                    sb.Append($"<td>{item.Food.Price * item.Count}</td>");
                    sb.Append("</tr>");
                }
            
        
             sb.Append("</tbody>");  
             sb.Append("</table>");
             sb.Append("</body>");
             sb.Append("</html>");
        
             // Set the email body
             mail.Body = sb.ToString();
             mail.IsBodyHtml = true;
        
             // Configure the SMTP client
             smtpClient.Port = 587;
             smtpClient.UseDefaultCredentials = false;
             smtpClient.Credentials = new NetworkCredential("mail@sseeyyeedd.ir", "]@Z-N,AnI=,u"); // Replace with your email and password
             smtpClient.EnableSsl = true;
        
             // Send the email
             smtpClient.Send(mail);
        
             Console.WriteLine("Order item email sent.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending order item email: " + ex.Message);
            }
}


        
    }
}
