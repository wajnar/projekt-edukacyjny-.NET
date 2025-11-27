using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? throw new InvalidOperationException("Brak connection stringa");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Statyczne pliki + fallback na index.html
app.UseStaticFiles();
app.MapFallbackToFile("/index.html");

// API – tylko ścieżki zaczynające się od /hello, /todos itd.
app.MapGet("/hello/{name}", (string name) =>
{
    return Results.Ok(new
    {
        Message = $"Hello {name} .NET in Docker!",
        Time = DateTime.UtcNow
    });
});

app.MapGet("/todos", async (AppDbContext db) =>
{
    var todos = await db.Todos
        .OrderByDescending(t => t.CreatedAt)
        .ToListAsync();

    return Results.Ok(todos);
});

app.MapGet("/todos/{id:int}", async (int id, AppDbContext db) =>
{
    var todo = await db.Todos.FindAsync(id);
    return todo is null ? Results.NotFound() : Results.Ok(todo);
});

app.MapPost("/todos", async (TodoItem dto, AppDbContext db) =>
{
    dto.CreatedAt = DateTime.UtcNow;

    if (dto.Deadline.HasValue){
        dto.Deadline = DateTime.SpecifyKind(dto.Deadline.Value, DateTimeKind.Utc);
    }

    db.Todos.Add(dto);
    await db.SaveChangesAsync();
    return Results.Created($"/todos/{dto.Id}", dto);
});

app.MapPut("/todos/{id:int}", async (int id, TodoItem dto, AppDbContext db) =>
{
    var existing = await db.Todos.FindAsync(id);
    if (existing is null) return Results.NotFound();

    existing.Title = dto.Title;
    existing.IsDone = dto.IsDone;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/todos/{id:int}", async (int id, AppDbContext db) =>
{
    var existing = await db.Todos.FindAsync(id);
    if (existing is null) return Results.NotFound();

    db.Todos.Remove(existing);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
