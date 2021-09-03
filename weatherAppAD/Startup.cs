using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;

namespace weatherAppAD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(Configuration.GetSection("AzureAd"));
            //    .EnableTokenAcquisitionToCallDownstreamApi(new string[] { "Read" })
            //    //.AddMicrosoftGraph(Configuration.GetSection("GraphBeta")).
            //    .AddDownstreamWebApi("WeatherAPI", Configuration.GetSection("WeatherAPI_AD"))
            //.AddInMemoryTokenCaches();

            //    services
            //.AddAuthentication(options =>
            //    {
            //        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //    })
            //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.Authority = "https://localhost:44384/"; // local identity server url
            //    options.ClientId = "aa3ad0e2-2659-4ca2-905c-05b3ac7dd777";
            //    options.ClientSecret = "-6i~ECW.tosb85Li06~nrM~T7K.uKfPh5h";
            //    options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
            //    options.SaveTokens = true;
            //    options.GetClaimsFromUserInfoEndpoint = true;
            //    options.Scope.Add("Data.read");
            //});

            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddRazorPages()
                 .AddMicrosoftIdentityUI();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }
            else
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}