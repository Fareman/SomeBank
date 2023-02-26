using Bank.Application.Dtos;
using Bank.Application.Interfaces;
using Bank.Application.Services;
using Bank.Domain.Interfaces;
using Bank.Infrastructure;
using Bank.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BankContext>(optionsBuilder =>
optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

app.MapGet("account/balance/{accountId:int}", async (int accountId, IAccountService accountService) =>
{
    var balance = await accountService.GetAccountBalanceAsync(accountId);
    return balance == null ? Results.NotFound() : Results.Ok(balance);
});

app.MapPost("account/balance", async (AccountDto accountDto, IAccountService accountService) =>
{
    try
    {
        var account = await accountService.CreateAccount(accountDto);
        return Results.Created($"balance/{account.Id}", account);
    }
    catch (Exception)
    {
        return Results.StatusCode(500);
    }
});

app.MapPut("account/balance/{accountId:int}", async (int accountId,
    decimal newBalance, IAccountService accountService) =>
{
    try
    {
        await accountService.ChangeBalance(accountId, newBalance);
        return Results.Ok();
    }
    catch (Exception)
    {
        return Results.StatusCode(500);
    }
});

app.MapPost("customer/register", async (CustomerDto dto, ICustomerService customerService) =>
{
    try
    {
        var customer = await customerService.RegisterCustomerAsync(dto);
        return Results.Created($"register/{customer.Id}", customer);
    }
    catch (Exception)
    {
        return Results.StatusCode(500);
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
