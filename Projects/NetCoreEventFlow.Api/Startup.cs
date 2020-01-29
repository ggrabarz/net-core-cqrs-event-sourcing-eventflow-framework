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
using NetCoreEventFlow.ReadModel.DomainEventHandlers.Inventory;
using System;
using System.Linq;
using System.Reflection;

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
                .AddEvents(Assembly.Load("NetCoreEventFlow.Domain"), type => typeof(IAggregateEvent).IsAssignableFrom(type))
                .AddCommands(Assembly.Load("NetCoreEventFlow.Application"), type => typeof(ICommand).IsAssignableFrom(type))
                .AddCommandHandlers(Assembly.Load("NetCoreEventFlow.Application"), type => typeof(ICommandHandler).IsAssignableFrom(type))
                .AddSubscribers(Assembly.Load("NetCoreEventFlow.Application"), type => type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISubscribeSynchronousTo<,,>)))
                .AddQueryHandlers(Assembly.Load("NetCoreEventFlow.ReadModel"), type => typeof(IQueryHandler).IsAssignableFrom(type))
                .AddSnapshots(Assembly.Load("NetCoreEventFlow.Domain"), type => typeof(ISnapshot).IsAssignableFrom(type))
                .AddSnapshotUpgraders(Assembly.Load("NetCoreEventFlow.Domain"), type => type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ISnapshotUpgrader<,>)))
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
