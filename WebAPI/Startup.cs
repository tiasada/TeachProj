using Domain.Classrooms;
using Infra;
using Domain.Common;
using Domain.Users;
using Domain.Teachers;
using Domain.Students;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Domain.Grades;
using Domain.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using WebAPI;
using Microsoft.IdentityModel.Tokens;
using Domain.ClassDays;

namespace TeachProj
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
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "any", 
                builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });

            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped(typeof (IRepository<>), typeof (DatabaseRepository<>));
            services.AddScoped(typeof (IRelationalRepository<>), typeof (RelationalRepository<>));
            services.AddScoped(typeof (IService<>), typeof (Service<>));
            services.AddScoped<ITeachersRepository, TeachersRepository>();
            services.AddScoped<ITeachersService, TeachersService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IClassroomsRepository, ClassroomsRepository>();
            services.AddScoped<IClassroomsService, ClassroomsService>();
            services.AddScoped<IStudentsRepository, StudentsRepository>();
            services.AddScoped<IStudentsService, StudentsService>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IGradesRepository, GradesRepository>();
            services.AddScoped<IGradesService, GradesService>();
            services.AddScoped<IClassDaysRepository, ClassDaysRepository>();
            services.AddScoped<IClassDaysService, ClassDaysService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var db = new TeachContext())
            {
                db.Database.Migrate();
            }
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("any");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
