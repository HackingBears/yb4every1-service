using System;
using System.Linq;
using System.Text.Json.Serialization;
using HackingBears.GameService.Core;
using HackingBears.GameService.Data;
using HackingBears.GameService.DataAccess;
using HackingBears.GameService.Domain;
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

            VotingManager vm = new VotingManager(12);
            Random rnd = new Random();
            for (int i = 0; i < 200; i++)
            {
                int pId = rnd.Next(0, 12);
                Domain.Voting v = new Domain.Voting()
                {
                    FrameNumber = 1,
                    PlayerId = pId,
                    GameId = 1,
                    UserId = Guid.NewGuid().ToString(),
                    GameAction = new Domain.GameAction()
                    {
                        Action = (Domain.Action)rnd.Next(1, 2),
                        Direction = (Domain.Direction)rnd.Next(1, 9)
                    }
                };
                vm.AddVoting(v);
            }
            

            System.Collections.Generic.List<VotingResult> results = vm.GetResult(1);

            Domain.Voting v2 = new Voting()
            {
                FrameNumber = 1,
                PlayerId = 2,
                GameId = 1,
                UserId = vm.Votings.First().UserId,
                GameAction = new GameAction()
                {
                    Action = (Domain.Action)rnd.Next(1, 2),
                    Direction = (Direction)rnd.Next(1, 9)
                }
            };
            vm.AddVoting(v2);
        }

        #endregion
    }
}