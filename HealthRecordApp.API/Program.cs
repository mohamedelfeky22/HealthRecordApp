using HealthRecordApp.Authentication.Configuration;
using HealthRecordApp.DataService.Configuration;
using HealthRecordApp.DataService.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Add services to the container.

builder.Services.AddDbContext<AppDBContext >(
    options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//configure referesh token 
var key = Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:Secret"]);
var tokenValidationParameters = new TokenValidationParameters
{

    RequireExpirationTime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = builder.Configuration["JWTConfig:Issuer"],
    ValidAudience = builder.Configuration["JWTConfig:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(key),
    ValidateLifetime = true,
};

//Injecting into our DI Container
builder.Services.AddSingleton(tokenValidationParameters);

//Move Default authentication to JWT
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:Secret"]);
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = tokenValidationParameters;
});




builder.Services.AddDefaultIdentity<IdentityUser>(opt => { opt.SignIn.RequireConfirmedAccount = true;})
    .AddEntityFrameworkStores<AppDBContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection("JWTConfig"));

builder.Services.AddApiVersioning(opt =>
{
    //provide to clinet different api version that we have
    opt.ReportApiVersions = true;

    //this allow  the  api automatically provide deafult version
      opt.AssumeDefaultVersionWhenUnspecified=true;

      opt.DefaultApiVersion=ApiVersion.Default;
  }
    
    );


 //var 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();    
app.UseAuthorization();

app.MapControllers();

app.Run();
