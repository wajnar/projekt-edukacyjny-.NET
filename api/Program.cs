var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
 app.UseSwagger();
 app.UseSwaggerUI();   
}

app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        Message = "Hello from .NET in Docker!",
        Time = DateTime.UtcNow
    });
});

app.MapGet("/hello/{name}", (string name) =>
{
    return Results.Ok(new
    {
        Message = $"Hello {name} .NET in Docker!",
        Time = DateTime.UtcNow
    });
});

app.Run();
