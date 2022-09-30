using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.DataAccess;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using OngProject.Core.Models;
using OngProject.Services;
using OngProject.Services.Interfaces;
using OngProject.Core.Helper;

namespace OngProject
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
            services.AddScoped<IActivityBusiness, ActivityBusiness>();
            services.AddScoped<IAuthBusiness, AuthBusiness>();
            services.AddScoped<ICategoryBusiness, CategoryBusiness>();
            services.AddScoped<ICommentBusiness, CommentBusiness>();
            services.AddScoped<IContactsBusiness, ContactBusiness>();
            services.AddScoped<IEmailService, SendGridEmailService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMemberBusiness, MemberBusiness>();
            services.AddScoped<INewsBusiness, NewsBusiness>();
            services.AddScoped<IOrganizationBusiness, OrganizationBusiness>();
            services.AddScoped<IRoleBusiness, RoleBusiness>();
            services.AddScoped<ISlideBusiness, SlidesBusiness>();
            services.AddScoped<ITestimonialBusiness, TestimonialBusiness>();
            services.AddScoped<IUserBusiness, UserBusiness>();

            services.AddScoped<IImageStorageHerlper, ImageStorageHelper>();
            
        
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OngProject", Version = "v1" });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
            (
                Options => 
                {
                    Options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                }
            );
            //Injects the configuration to the AWS3 helper
            services.Configure<AWS3ConfigurationModel>(
                Configuration.GetSection(AWS3ConfigurationModel.AwsConfiguration));

            ////agrego EF
            services.AddDbContext<OngDbContext>(options =>
                      options.UseSqlServer(Configuration.GetConnectionString("ONG")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OngProject v1"));
            }


            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}