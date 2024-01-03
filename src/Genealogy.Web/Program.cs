using Genealogy.Api;
using Genealogy.Infrastructure;
using Serilog;

//var loggerBuilder = new LoggerConfiguration();
//loggerBuilder.WriteTo.Console();
//loggerBuilder.WriteTo.Debug();

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
