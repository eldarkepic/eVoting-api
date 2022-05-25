using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using eVoting.Server.Models;
using eVoting.Server.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using eVoting.Server.Models.DataSeeding;
using eVoting.Repositories;
using eVoting.Server.Infrastructure;
using eVoting.Server.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace eVoting.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("VotingConnection"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("eVoting.Server");
                });
            });

            services.AddIdentity<Voter, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
            }).AddEntityFrameworkStores<MyContext>()
              .AddDefaultTokenProviders();

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["AuthSettings:Audience"],
                    ValidIssuer = Configuration["AuthSettings:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:Key"])),
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddScoped<IUnitOfWork, EfUnitOfWork>();

            services.AddScoped(sp => new AuthOptions
            {
                Audience = Configuration["AuthSettings:Audience"],
                Issuer = Configuration["AuthSettings:Issuer"],
                Key = Configuration["AuthSettings:Key"],
            });

            services.AddScoped(sp =>
            {
                var httpContext = sp.GetService<IHttpContextAccessor>().HttpContext;

                var identityOptions = new Infrastructure.IdentityOptions();

                if (httpContext.User.Identity.IsAuthenticated)
                {
                    string id = httpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    string firstName = httpContext.User.FindFirst(ClaimTypes.GivenName).Value;
                    string lastName = httpContext.User.FindFirst(ClaimTypes.Surname).Value;
                    string email = httpContext.User.FindFirst(ClaimTypes.Email).Value;
                    string role = httpContext.User.FindFirst(ClaimTypes.Role).Value;

                    identityOptions.UserId = id;
                    identityOptions.Email = email;
                    identityOptions.FullName = $"{firstName} {lastName}";
                    identityOptions.IsAdmin = role == "Admin" ? true : false;
                }

                return identityOptions;
            });

            services.AddHttpContextAccessor();
            services.AddScoped<IVotersService, VoterService>();
            services.AddScoped<IVotelistsService, VotelistsService>();
            services.AddScoped<IVotesService, VotesService>();
            services.AddScoped<IPartiesService, PartiesService>();
            services.AddScoped<ICandidatesService, CandidatesService>();

            services.AddSwaggerGen();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<Voter> userManager,
                              RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var dataSeeding = new UserSeeding(userManager, roleManager);
            dataSeeding.SeedData().Wait();

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(swagger => 
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "eVoting API");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
