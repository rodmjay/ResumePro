using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ResumePro.Core.Interceptors
{

    public class ConnectionTrackingInterceptor : DbConnectionInterceptor
    {
        private static int _openConnections = 0;

        public static int OpenConnections => _openConnections;

        public override void ConnectionOpened(DbConnection connection, ConnectionEndEventData eventData)
        {
            Interlocked.Increment(ref _openConnections);
            base.ConnectionOpened(connection, eventData);
        }

        public override async Task ConnectionOpenedAsync(DbConnection connection, ConnectionEndEventData eventData, CancellationToken cancellationToken = default)
        {
            Interlocked.Increment(ref _openConnections);
            await base.ConnectionOpenedAsync(connection, eventData, cancellationToken);
        }

        public override void ConnectionClosed(DbConnection connection, ConnectionEndEventData eventData)
        {
            Interlocked.Decrement(ref _openConnections);
            base.ConnectionClosed(connection, eventData);
        }
    }
}
