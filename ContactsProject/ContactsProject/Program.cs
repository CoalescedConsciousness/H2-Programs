using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ContactsProject.Data;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using ContactList.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<Repository, Repository>();

//builder.Services.AddScoped<IGenericRepository<Contact>, GenericRepository<Contact>>();

builder.Services.AddDbContext<ContactsProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContactsProjectContext")));

// Redirect landing page
builder.Services.AddMvc().AddRazorPagesOptions(options =>
{
    options.Conventions.AddPageRoute("/Contacts/Index", "");
});

// Culture info services
//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    var supCult = new[]
//    {
//        new CultureInfo("en-US"),
//        new CultureInfo("da-DK"),
//        new CultureInfo("ja-JP"),
//        new CultureInfo("ko-KR"),
//    };
//    options.DefaultRequestCulture = new RequestCulture("en-US");
//    options.SupportedCultures = supCult;
//    options.SupportedUICultures = supCult;
//});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithRedirects("Errors/{0}");
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithRedirects("Errors/{0}");
//app.UseRequestLocalization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();



app.UseAuthorization();

app.MapRazorPages();

app.Run();
