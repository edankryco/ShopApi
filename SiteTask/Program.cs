using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.HttpOverrides;
using SiteTask.Controllers.ErrorDistribution;
using SiteTask.Controllers.Mail.Send;

var isActive = false;

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
        content.Response.ContentType = "text/plain; charset-utf-8";

        var exceptionHandlerFeature = content.Features.Get<IExceptionHandlerFeature>();
        if (exceptionHandlerFeature != null)
        {
            var exception = exceptionHandlerFeature.Error;

            if (exception.Message.Contains("MySQL hosts"))
                isActive = true;

            ISendEmailController sendEmailController = new SendEmailController();
            IErrorDistributionController errorDistributionController =
                new ErrorDistributionController(sendEmailController);
            await errorDistributionController.GetError(exception);
        }
    });
});

app.Use(async (context, next) =>
{
    if (isActive)
    {
        context.Response.StatusCode = 503;
        context.Response.ContentType = "text/html; charset-utf-8";
        await context.Response.WriteAsync("<h1>503 Problem with MySql</h1>");
    }

    await next.Invoke();
});

app.UseForwardedHeaders(new ForwardedHeadersOptions()
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                       ForwardedHeaders.XForwardedProto
});

app.Use(async (context, next) =>
{
    if (context.Connection.RemoteIpAddress != null)
    {
        var ip = context.Connection.RemoteIpAddress.ToString();
        await context.Response.WriteAsync(ip);
    }

    await next.Invoke();
});

app.Run();