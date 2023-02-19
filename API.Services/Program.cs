using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

using Todo.API.Models;
using Todo.API.Utilities;

var builder = WebApplication.CreateBuilder(args);

#region --- Database ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
}
);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
#endregion

#region --- Identity ---
builder.Services.AddDefaultIdentity<IdentityUser>(
    options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
    })
    // Add role services to identity
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
#endregion

#region --- Authentication ---
//builder.Services
//    .AddAuthentication(options => {
//        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//        //options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
//    })
//    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => {
//        options.LoginPath = "/Account/Unauthorized/";
//        options.AccessDeniedPath = "/Account/Forbidden/";
//    });

//builder.Services.AddAuthentication()
//    .AddIdentityServerJwt();
#endregion

#region --- Dependency Injection ---
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();