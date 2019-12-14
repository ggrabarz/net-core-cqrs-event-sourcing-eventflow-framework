using Autofac;
using EventFlow;
using EventFlow.Aggregates;
using EventFlow.AspNetCore.Extensions;
using EventFlow.AspNetCore.Middlewares;
using EventFlow.Autofac.Extensions;
using EventFlow.Commands;
using EventFlow.Extensions;
using EventFlow.Queries;
using EventFlow.Snapshots;
using EventFlow.Subscribers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreEventFlow.Api.App_Infrastructure.ExtensionMethods;
using NetCoreEventFlow.Api.Core.ReadModel;
using System;
using System.Linq;

namespace NetCoreEventFlow.Api
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
            services.AddControllers();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            EventFlowOptions.New
                .Configure(configure =>
                {
                    configure.CancellationBoundary = EventFlow.Configuration.Cancellation.CancellationBoundary.BeforeCommittingEvents;
                    configure.DelayBeforeRetryOnOptimisticConcurrencyExceptions = TimeSpan.FromMilliseconds(100);
                    configure.IsAsynchronousSubscribersEnabled = false;
                    configure.NumberOfRetriesOnOptimisticConcurrencyExceptions = 4;
                    configure.PopulateReadModelEventPageSize = 200;
                    configure.ThrowSubscriberExceptions = false;
                })
                .UseAutofacContainerBuilder(builder)
                .AddAspNetCore()
                .AddEvents(typeof(Startup).Assembly, type => typeof(IAggregateEvent).IsAssignableFrom(type))
                .AddCommands(typeof(Startup).Assembly, type => typeof(ICommand).IsAssignableFrom(type))
                .AddCommandHandlers(typeof(Startup).Assembly, type => typeof(ICommandHandler).IsAssignableFrom(type))
                .AddSubscribers(typeof(Startup).Assembly, type => type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscribeSynchronousTo<,,>)))
                .AddQueryHandlers(typeof(Startup).Assembly, type => typeof(IQueryHandler).IsAssignableFrom(type))
                .AddSnapshots(typeof(Startup).Assembly, type => typeof(ISnapshot).IsAssignableFrom(type))
                .AddSnapshotUpgraders(typeof(Startup).Assembly, type => type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISnapshotUpgrader<,>)))
                .UseConsoleLog()
                .ConfigureEventStore(Configuration)
                .UseInMemorySnapshotStore()
                .UseInMemoryReadStoreFor<InventoryItemReadModel>()
                .UseInMemoryReadStoreFor<InventoryItemDetailsReadModel>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseMiddleware<CommandPublishMiddleware>();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
