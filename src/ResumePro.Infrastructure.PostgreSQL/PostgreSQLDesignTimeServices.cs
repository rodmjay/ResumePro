using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace ResumePro.Infrastructure.PostgreSQL
{
    public class PostgreSQLDesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection services)
        {
            // Add PostgreSQL design time services
            services.AddEntityFrameworkNpgsql();
        }
    }
}
