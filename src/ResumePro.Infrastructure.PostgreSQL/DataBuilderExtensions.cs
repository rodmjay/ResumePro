using Bespoke.Data.Builders;
using Bespoke.Data.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using ResumePro.Data.Contexts;

namespace ResumePro.Infrastructure.PostgreSQL;

public static class DataBuilderExtensions
{
    public static void UsePostgreSQLApplicationContext(this DataBuilder builder)
    {
        builder.UsePostgreSQL<ApplicationContext>(settings =>
        {
            settings.ConnectionStringName = "PostgreSQLConnection";
        });
    }
}
