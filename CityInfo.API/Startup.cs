using CityInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CityInfo.API
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
      services.AddMvc()
              .AddMvcOptions( o => o.OutputFormatters.Add(
                              new XmlDataContractSerializerOutputFormatter()));
      // services.AddTransient<LocalMailService>();
      // services.AddTransient<IMailService,LocalMailService>();
      // services.AddTransient<IMailService, CloudMailService>();
#if DEBUG
      services.AddTransient<IMailService, LocalMailService>();
#else
      services.AddTransient<IMailService, CloudMailService>();
#endif
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole();
      loggerFactory.AddDebug();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseStatusCodePages();
      app.UseMvc();
    }
  }
}
