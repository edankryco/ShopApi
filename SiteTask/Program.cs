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

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Front", "Zyablic");
    await next.Invoke();
});

app.Run();