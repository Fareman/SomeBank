using Bank.DB;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BankContext>(optionsBuilder =>
optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

var app = builder.Build();

app.MapGet("/balance/{id:int}", async (int id, IJurisRepository repo) =>
{
    try
    {
        var client = await repo.GetClientAsync(id);
        if (client == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(client);
    }
    catch (Exception ex)
    {
        return Results.BadRequest("Failed");
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
