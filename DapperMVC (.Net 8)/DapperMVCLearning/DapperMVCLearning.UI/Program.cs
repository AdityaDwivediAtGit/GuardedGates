using DapperMVCLearning.Data.DataAccess;
using DapperMVCLearning.Data.Repository;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// AddTransient<ISqlDataAccess, SqlDataAccess>():
// This method call registers a transient service in the DI container.
// Transient means that a new instance of SqlDataAccess will be created each time it is requested. 
// By registering this service,
// you are telling the DI container that whenever an ISqlDataAccess is requested,
// it should provide an instance of SqlDataAccess.
// This allows you to easily swap out the implementation of ISqlDataAccess in the future
// without needing to change the consuming classes,
// as long as the new implementation also implements ISqlDataAccess.
builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddTransient<IPersonRepository, PersonRepository>();

// Inject IDbConnection, with implementation from SqlConnection class.
//builder.Services.AddTransient<IDbConnection>((sp) => new SqlConnection(builder.Configuration.GetConnectionString("SqlServerConnection")));

// adding auth
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
    };
});
builder.Services.AddCors(p => p.AddPolicy("Cors Policy for flask at port 1234", build => build.WithOrigins("http://127.0.0.1:1234").AllowAnyMethod().AllowAnyHeader()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
