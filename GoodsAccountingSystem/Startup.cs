using AutoMapper;
using GoodsAccountingSystem.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Threading.Tasks;

namespace GoodsAccountingSystem
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
            services.AddAutoMapper(typeof(Startup));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false; // true
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //services.ConfigureApplicationCookie(options =>
            //    options.Events.OnRedirectToLogin = context =>
            //    {
            //        context.Response.StatusCode = 401;
            //        return Task.CompletedTask;
            //    }
            //);

            services.ConfigureMySqlContext(Configuration)
                .ConfigureIdentity()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
            services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
            services.AddMvc(options =>
            {
                var F = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                var L = F.Create("ModelBindingMessages", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(
                    (x) => L["The value '{0}' is invalid."]);
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
                    (x) => L["The field {0} must be a number."]);
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(
                    (x) => L["A value for the '{0}' property was not provided.", x]);
                options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(
                    () => L["11111111111111111111111111111111"]);
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
                    (x, y) => L["The value '{0}' is not valid for {1}.", x, y]);
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(
                    () => L["A value is required."]);
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(
                    (x) => L["The supplied value is invalid for {0}.", x]);
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    (x) => L["Null value is invalid.", x]);
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddDataAnnotationsLocalization()
            .AddViewLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("ru-RU") };
                options.DefaultRequestCulture = new RequestCulture("en-US", "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("ru") };
            app.UseRequestLocalization(new RequestLocalizationOptions()
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("en")),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Goods/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Goods}/{action=Index}/{id?}");
            });
        }
    }
}
