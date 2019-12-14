using System;
using System.Data.Common;
using EventFlow;
using EventFlow.EventStores.EventStore.Extensions;
using EventFlow.Extensions;
using EventFlow.MetadataProviders;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Microsoft.Extensions.Configuration;

namespace NetCoreEventFlow.Api.App_Infrastructure.ExtensionMethods
{
    public static class EventFlowOptionsExtensions
    {
        public static IEventFlowOptions ConfigureEventStore(this IEventFlowOptions options, IConfiguration configuration)
        {
            var eventStoreUrl = configuration["EventStore:Url"];
            var connectionString = $"ConnectTo={eventStoreUrl}; HeartBeatTimeout=500";
            var eventStoreUri = GetUriFromConnectionString(connectionString);
            var connectionSettings = ConnectionSettings.Create()
                .EnableVerboseLogging()
                .KeepReconnecting()
                .KeepRetrying()
                .SetDefaultUserCredentials(new UserCredentials("admin", "changeit"))
                .Build();
            var eventFlowOptions = options
                .AddMetadataProvider<AddGuidMetadataProvider>()
                .UseEventStoreEventStore(eventStoreUri, connectionSettings);
            return eventFlowOptions;
        }

        private static Uri GetUriFromConnectionString(string connectionString)
        {
            var builder = new DbConnectionStringBuilder { ConnectionString = connectionString };
            var connectTo = (string)builder["ConnectTo"];
            return connectTo == null ? null : new Uri(connectTo);
        }
    }
}
