using Sindile.InternUI.Configuration;
using Sindile.InternUI.Settings;
using System.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddService();
//builder.Services.Configure<IntegratedAPISettings>(Configuration.GetSection("Endpoints"));
builder.Services.Configure<IntegratedAPISettings>(builder.Configuration.GetSection("Endpoints"));

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//        .AddCookie(options =>
//        {
//          options.LoginPath = "/Account/Unauthorized/";
//          options.AccessDeniedPath = "/Account/Forbidden/";
//        })
//        .AddJwtBearer(options =>
//        {
//          options.Audience = "https://localhost:5001/";
//          options.Authority = "https://localhost:5001/";
//        });

//builder.Services.AddAuthentication()
//        .AddIdentityServerJwt();

IdentityModelEventSource.ShowPII = true; //Add this line
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication(options =>
{
  options.DefaultScheme = "Cookies";
  options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
      options.SignInScheme = "Cookies";
      options.Authority = "https://localhost:5001";
      options.RequireHttpsMetadata = false;
      options.ClientId = "adminUIclient";
      options.ResponseType = "code id_token";
      options.SaveTokens = true;

      options.GetClaimsFromUserInfoEndpoint = true;
      // options.Scope.Add("access");
      //options.Scope.Add("offline_access"); 
      options.ResponseType = "token id_token";
      options.SaveTokens = true;
      options.GetClaimsFromUserInfoEndpoint = true;
      // options.Scope.Add("access");
      //options.Scope.Add("offline_access");
    });

builder.Services.AddRazorPages();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
