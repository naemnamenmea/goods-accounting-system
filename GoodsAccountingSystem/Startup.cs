using AutoMapper;
using GoodsAccountingSystem.Helpers;
using GoodsAccountingSystem.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

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
                options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(
                    (x) => L["A value for the '{0}' property was not provided.", x]);
                options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(
                    () => L["A value is required."]);
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    (x) => L["Null value is invalid.", x]);
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor( // +
                    (x, y) => L["The value '{0}' is not valid for {1}.", x, y]);
                options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(
                    (x) => L["The supplied value is invalid for {0}.", x]);
                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
                    (x) => L["The field {0} must be a number."]);
                options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(
                    (x) => L["The value '{0}' is invalid."]);
                options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(
                    () => L["A non-empty request body is required."]);
                options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(
                    (x) => L["The value '{0}' is not valid.", x]);
                options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(
                    () => L["The supplied value is invalid."]);
                options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(
                    () => L["The field must be a number."]);
            })
            .AddDataAnnotationsLocalization(o =>
            {
                o.DataAnnotationLocalizerProvider = (type, factory) =>
                  factory.Create(typeof(SharedResource));
            })
            //.AddViewLocalization(o => { o.ResourcesPath = "Resources"; })
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[] { new CultureInfo("en-US"), new CultureInfo("ru-RU") };
                options.DefaultRequestCulture = new RequestCulture("ru-RU", "ru-RU");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                //options.RequestCultureProviders = new List<IRequestCultureProvider>();

                //options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                //{
                //    var userLangs = context.Request.Headers["Accept-Language"].ToString();
                //    var firstLang = userLangs.Split(',').FirstOrDefault();
                //    var defaultLang = string.IsNullOrEmpty(firstLang) ? "ru" : firstLang;
                //    return Task.FromResult(new ProviderCultureResult(defaultLang, defaultLang));
                //}));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Goods/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
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