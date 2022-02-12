using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoviesApi.Domain.Models.User;
using MoviesApi.Domain.Repositories;
using MoviesApi.Domain.Repositories.FilmCopyRepository;
using MoviesApi.Domain.Repositories.FilmRepository;
using MoviesApi.Domain.Repositories.FilmStudioRepository;
using MoviesApi.Domain.Repositories.UserRepository;
using MoviesApi.Persistence;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApi
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
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Movies")));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddAuthentication()
              .AddCookie()
              .AddJwtBearer(cfg =>
              {
                  cfg.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidIssuer = Configuration["Tokens:Issuer"],
                      ValidAudience = Configuration["Tokens:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                  };
              });



          
            services.AddTransient<AppDbContext>();
            services.AddControllers().AddNewtonsoftJson(); 
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddCors();
            /*
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilmStudio", Version = "v1" });

            });
            */

            services.AddScoped<IFilmStudioRepository, FilmStudioRepository>();
            services.AddScoped<IFilmRepository, FilmRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFilmCopyRepository, FilmCopyRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              //  app.UseSwagger();
               // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FilmStudio v1"));
            }
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

           
        

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
