namespace myApi.MiddleWare;
using System.Diagnostics;

public class MyLogMiddleWare
{
    private RequestDelegate next;

    public MyLogMiddleWare(RequestDelegate next)
    {
        this.next=next;
    }

    public async Task Invoke(HttpContext contex)
    {
        // await contex.Response.WriteAsync($"My Log Middleware start\n");
        var sw =new Stopwatch();
        sw.Start();
        await next(contex);
        Console.WriteLine($"{contex.Request.Path}.{contex.Request.Method} took {sw.ElapsedMilliseconds}ms."
                            + $" Succes: {contex.Items["success"]}");
        // await contex.Response.WriteAsync("My Log Middleware end\n");
    }
}
    public static class MyLogMiddleWareHelper
    {
        public static void UseMyLog(this IApplicationBuilder a)
        {
            a.UseMiddleware<MyLogMiddleWare>();
        }
    }
