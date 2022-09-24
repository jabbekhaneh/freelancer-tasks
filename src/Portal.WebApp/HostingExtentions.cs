using Microsoft.EntityFrameworkCore;
using Portal.ApplicationServices.Users;
using Portal.Domain;
using Portal.Domain.Users.Contracts;
using Portal.EF;
using Portal.EF.Users;

namespace Portal.WebApp;

public static class HostingExtentions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        #region IOC
        builder.Services.AddTransient<UnitOfWork, EFUnitOfWork>();
        builder.Services.AddTransient<UserRepository, EFUserRepository>();
        builder.Services.AddTransient<UserService, UserApplicationServices>();

        #endregion

        #region DatabaseConfigs 

        builder.Services.AddDbContext<EFdbApplication>(options =>
        {
            options.UseSqlServer("data source =.; initial catalog =dbFreeLancerContext; integrated security = True; MultipleActiveResultSets=True",
                 serverDbContextOptionsBuilder =>
                 {
                     var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
                     serverDbContextOptionsBuilder.CommandTimeout(minutes);
                     serverDbContextOptionsBuilder.EnableRetryOnFailure();

                 });

        });


        #endregion

        builder.Services.AddControllersWithViews();
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {

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
        return app;
    }
}
