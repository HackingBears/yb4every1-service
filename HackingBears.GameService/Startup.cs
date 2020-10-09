using System.Text.Json.Serialization;
using HackingBears.GameService.Core;
using HackingBears.GameService.Data;
using HackingBears.GameService.DataAccess;
using HackingBears.GameService.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HackingBears.GameService
{
    public sealed class Startup
    {
        #region Properties

        public IConfiguration Configuration { get; }

        #endregion

        #region Constructor

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Methods

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGamePlayEngine, GamePlayEngine>();
            services.AddScoped<IGameRepository, InMemoryGameRepository>();
            services.AddSingleton<IGamePlayManager, GamePlayManager>();
            services.AddMvc()
                .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHub<GameHub>("/app/gamehub");
                }
            );
        }

        #endregion
    }
}