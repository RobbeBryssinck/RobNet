using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bots.Data.Database;
using Bots.Data.Repository.v1;
using Bots.Domain.Entities;
using Bots.Messaging.Receive.Options.v1;
using Bots.Messaging.Receive.Receiver.v1;
using Bots.Service.Command;
using Bots.Service.Query;
using Bots.Service.Services;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Bots.API
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
            services.AddOptions();

            services.Configure<RabbitMqConfiguration>(Configuration.GetSection("RabbitMq"));

            services.AddDbContext<BotsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BotsDB")));

            services.AddMvc().AddFluentValidation();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Bots Api",
                    Description = "A simple API to manage bots and botnets.",
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(IBotnetStatusUpdateService).Assembly);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IBotRepository, BotRepository>();
            services.AddScoped<IBotnetRepository, BotnetRepository>();

            services.AddTransient<IRequestHandler<CreateBotCommand, Bot>, CreateBotCommandHandler>();
            services.AddTransient<IRequestHandler<DeleteBotCommand, Bot>, DeleteBotCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateBotCommand, Bot>, UpdateBotCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateBotnetCommand>, UpdateBotnetCommandHandler>();
            services.AddTransient<IRequestHandler<GetBotByIdQuery, Bot>, GetBotByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetBotnetByIdQuery, Botnet>, GetBotnetByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetBotsByBotnetIdSlicedQuery, List<Bot>>, GetBotsByBotnetIdSlicedQueryHandler>();
            services.AddTransient<IBotnetStatusUpdateService, BotnetStatusUpdateService>();

            services.AddHostedService<BotnetStatusUpdateReceiver>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bots API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
