using System.Diagnostics;
using System.Net.NetworkInformation;

namespace ResumePro.Api.Services
{
    public class AngularAppService : IDisposable
    {
        private readonly ILogger<AngularAppService> _logger;
        private readonly IHostEnvironment _environment;
        private Process? _angularProcess;
        private bool _disposed = false;

        public AngularAppService(ILogger<AngularAppService> logger, IHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public void StartAngularApp()
        {
            // Only run in development environment
            if (!_environment.IsDevelopment())
            {
                _logger.LogInformation("Angular app auto-start is disabled in non-development environments");
                return;
            }

            try
            {
                // Check if port 4200 is available
                if (!IsPortAvailable(4200))
                {
                    _logger.LogInformation("Port 4200 is already in use. Angular app will not be started automatically.");
                    return;
                }

                // Get the Angular app directory
                string angularAppDirectory = Path.Combine(Directory.GetCurrentDirectory(), "..", "ResumeProApp");
                angularAppDirectory = Path.GetFullPath(angularAppDirectory);

                if (!Directory.Exists(angularAppDirectory))
                {
                    _logger.LogWarning($"Angular app directory not found at {angularAppDirectory}");
                    return;
                }

                // Start the Angular app
                _logger.LogInformation("Starting Angular app...");
                
                _angularProcess = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "npm",
                        Arguments = "start",
                        WorkingDirectory = angularAppDirectory,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    }
                };

                _angularProcess.OutputDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        _logger.LogInformation($"Angular: {args.Data}");
                    }
                };

                _angularProcess.ErrorDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        _logger.LogError($"Angular Error: {args.Data}");
                    }
                };

                try
                {
                    _angularProcess.Start();
                    _angularProcess.BeginOutputReadLine();
                    _angularProcess.BeginErrorReadLine();

                    _logger.LogInformation("Angular app started successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to start Angular app process");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to start Angular app");
            }
        }

        private bool IsPortAvailable(int port)
        {
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            var tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();

            foreach (var endpoint in tcpConnInfoArray)
            {
                if (endpoint.Port == port)
                {
                    return false;
                }
            }

            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_angularProcess != null && !_angularProcess.HasExited)
                    {
                        try
                        {
                            _logger.LogInformation("Stopping Angular app...");
                            _angularProcess.Kill(true);
                            _angularProcess.Dispose();
                            _logger.LogInformation("Angular app stopped successfully");
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Failed to stop Angular app");
                        }
                    }
                }

                _disposed = true;
            }
        }
    }
}
