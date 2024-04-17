using Microsoft.AspNetCore.Mvc;
using ToniAuto2003.ModelBinders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToniAuto2003.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ToniAutoDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ToniAutoDbContextConnection' not found.");


builder.Services.AddApplicationDbContext(builder.Configuration);
builder.Services.AddApplicationIdentity(builder.Configuration);

builder.Services.AddControllersWithViews(option =>
{
    option.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
    option.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
});

builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error/500");
    app.UseStatusCodePagesWithRedirects("/Home/Error?statuscode={0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Car Details",
        pattern: "/Car/Details/{id}/{model}",
        defaults: new { Controller="Car",Action="Details" }
    );
    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();

});

app.Run();
