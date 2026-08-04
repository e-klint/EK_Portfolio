
using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using TheBookParlour.Core.Helpers;
using TheBookParlour.Core.Interfaces;
using TheBookParlour.Core.Services;
using TheBookParlour.Data;
using TheBookParlour.Data.Interfaces;
using TheBookParlour.Data.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

string connString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<BookshopContext>(options => options.UseSqlServer(connString));

//DI
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IBookRepo, BookRepo>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IGenreRepo, GenreRepo>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<ICartRepo, CartRepo>();
builder.Services.AddScoped<ICartService, CartService>();

//Scalar settings
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
});

builder.Services.AddAuthorization()
    .AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Lägg till Application Insights
// Connection string hämtas automatiskt från konfigurationen
//builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

//// Azure Key Vault
//builder.Configuration.AddAzureKeyVault(
//    new Uri("https://kv-lab-e-klint.vault.azure.net/"),
//    new DefaultAzureCredential());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{   
    //Scalar
    app.MapOpenApi();
    app.MapScalarApiReference(options => options
        .AddPreferredSecuritySchemes("Bearer"));     
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

//Använde koden nedan för att manuellt få fram password hash
//till admin-använadren som skapas samtidigt som databasen.

//var hasher = new PasswordHasher();
//string hash = hasher.Hash("admin123");
//Console.WriteLine(hash);

app.Run();
