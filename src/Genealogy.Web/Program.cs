using Genealogy.Api;
using Genealogy.Infrastructure;
using Microsoft.AspNetCore.Localization;
using Serilog;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args)
                            .AddAuth()
                            .AddInfrastructure();

builder.Services
       .AddControllersWithViews()
       .AddJsonOptions(options => options.JsonSerializerOptions.SetGenealogyDefault().AddGenealogyConverters());
builder.Services.AddRazorPages();
builder.Services.AddMemoryCache();

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

var app = builder.Build();

var defaultCulture = new CultureInfo("sv-SE");
app.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture(defaultCulture),
        SupportedCultures = new List<CultureInfo> { defaultCulture, },
        SupportedUICultures = new List<CultureInfo> { defaultCulture, }
    });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuth();

app.UseSerilogRequestLogging();

app.MapControllers();
app.MapRazorPages();

app.Run();
