using ForumMotor_13BC_H.Data;
using ForumMotor_13BC_H.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Path to your custom login page
    options.LogoutPath = "/Account/Logout"; // Path to your custom logout page
    options.AccessDeniedPath = "/Account/AccessDenied"; // Path for access denied page
});
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddAreaFolderRouteModelConvention(
        areaName: "Identity",
        folderPath: "/Account",
        model =>
        {
            foreach (var selector in model.Selectors)
            {
                // Update the template to use /Account instead of /Identity/Account
                selector.AttributeRouteModel.Template = AttributeRouteModel.CombineTemplates("Account", selector.AttributeRouteModel.Template.Replace("Identity", ""));
            }
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.Run();
