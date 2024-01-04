using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using SiteTask.Controllers.ErrorDistribution;
using SiteTask.Controllers.Mail.Send;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler((error) =>
{
    error.Run(async (content) =>
    {
        content.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        content.Response.ContentType = "text/plain";

        var exceptionHandlerFeature = content.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionHandlerFeature.Error;

        ISendEmailController sendEmailController = new SendEmailController();
        IErrorDistributionController errorDistributionController = new ErrorDistributionController(sendEmailController);
        await errorDistributionController.GetError(exception);
    });
});


app.Use(async (context, next) =>
{
    if (false)
    {
        context.Response.StatusCode = 404;
        context.Response.ContentType = "text/html; charset-utf-8";
        await context.Response.WriteAsync("<h1> NOT FOUND </h1>");
    }
    
    await next.Invoke();
});

app.Run();