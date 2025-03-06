// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.

// builder.Services.AddControllers();
// // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();

using myApi.Services;
using Middleware.Middlewares;
using myApi.MiddleWare;
// using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// הוספת שירותי ה-Controllers
builder.Services.AddControllers();
builder.Services.AddGiftConst();

// הוספת Swagger
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
// });

var app = builder.Build();

// קביעת המידלוואר לשרת את Swagger כנקודת JSON
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI(c =>
//     {
//         c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//         c.RoutePrefix = string.Empty; // קבעי את Swagger UI בשורש האפליקציה
//     });
// }
app.UseMyLog();
app.UseMyErrorMiddleware();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
