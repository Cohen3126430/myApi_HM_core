using System.Net;
using System.Net.Mail;

namespace Middleware.Middlewares;
public class MyErrorMiddleware
{
    private RequestDelegate n;

    public MyErrorMiddleware(RequestDelegate next)
    {
        this.n = next;
    }

    public async Task Invoke(HttpContext c)
    {
        c.Items["success"] = false;
        bool success = false;
        try
        {
            await n(c);
            c.Items["success"] = true;
        }
        catch (ApplicationException ex)
        {
            c.Response.StatusCode = 400;
            await c.Response.WriteAsync(ex.Message);
        }
        catch (Exception e)
        {
            try
            {
                // קוד לשליחת המייל
                MailMessage mail = new MailMessage("y05271907@gmail.com", "9745544b@gmail.com", $"תקלה בשרת {e.Message}", "פנה לתמיכה התכנית");

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("y05271907@gmail.com", "y089741623");
                    smtp.EnableSsl = true;

                    Console.WriteLine("before send mail");
                    smtp.Send(mail);
                    Console.WriteLine("after send mail");
                    Console.WriteLine("המייל נשלח בהצלחה");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("שגיאה בשליחת המייל: " + ex.Message);
            }

        }
    }
}

public static partial class MiddlewareExtensions
{
    public static WebApplication UseMyErrorMiddleware(
        this WebApplication app)
    {
        app.UseMiddleware<MyErrorMiddleware>();
        return app;
    }
}
