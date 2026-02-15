using ApiBankApp.Core.Interfaces;
using ApiBankApp.Core.Services;
using ApiBankApp.Data.Entities;
using ApiBankApp.Data.Interfaces;
using ApiBankApp.Data.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=BankAppData;Integrated Security=SSPI;TrustServerCertificate=True;";

builder.Services.AddDbContext<BankAppDataContext>(options =>

    options.UseSqlServer(connString)
);

// AutoMapper
builder.Services.AddAutoMapper(cfg => { }, typeof(Program).Assembly);

//Lägg till DI för alla repos och services
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ILoanRepo, LoanRepo>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<ITransactionRepo, TransactionRepo>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

//Konfigurera JWT
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt => {
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://BankApp",
        ValidAudience = "http://BankApp",
        IssuerSigningKey =
    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsAVerySecretJwtKey_ForBankApp123!"))
    };
});


builder.Services.AddControllers();

//Sätta upp swagger
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Sätt upp swagger 
app.UseSwagger();
app.UseSwaggerUI(endpoint =>
{
    endpoint.SwaggerEndpoint("/swagger/v1/swagger.json", "Inlamning_2BankApi");
});


app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
