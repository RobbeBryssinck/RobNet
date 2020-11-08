using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using BotnetJobs.API.Models.v1;
using BotnetJobs.API.Validators.v1;
using BotnetJobs.Data.Database;
using BotnetJobs.Data.Repository.v1;
using BotnetJobs.Domain.Entities;
using BotnetJobs.Messaging.Send.Options.v1;
using BotnetJobs.Messaging.Send.Sender.v1;
using BotnetJobs.Service.v1.Command;
using BotnetJobs.Service.v1.Query;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BotnetJobs.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));

            services.AddDbContext<BotnetJobContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("BotnetJobsDB")));

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Botnet jobs API",
                    Description = "An API managing the jobs executed on botnets",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext = actionContext as ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IBotnetJobRepository, BotnetJobRepository>();

            services.AddTransient<IValidator<CreateBotnetJobModel>, CreateBotnetJobModelValidator>();
            services.AddTransient<IValidator<UpdateBotnetJobModel>, UpdateBotnetJobModelValidator>();

            services.AddTransient<IBotnetJobUpdateSender, BotnetJobUpdateSender>();

            services.AddTransient<IRequestHandler<CreateBotnetJobCommand, BotnetJob>, CreateBotnetJobCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateBotnetJobCommand, BotnetJob>, UpdateBotnetJobCommandHandler>();
            services.AddTransient<IRequestHandler<GetBotnetJobByIdQuery, BotnetJob>, GetBotnetJobByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetBotnetJobByBotnetIdQuery, BotnetJob>, GetBotnetJobByBotnetIdQueryHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Botnet jobs API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
