using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace ResumePro.Core.Configuration
{
    public class CloudRoleNameTelemetryInitializer(string roleName) : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            if (string.IsNullOrEmpty(telemetry.Context.Cloud.RoleName)) telemetry.Context.Cloud.RoleName = roleName;
        }
    }
}
