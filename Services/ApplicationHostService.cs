using MFAWPF.Views;
using MFAWPF.Views.UI;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace MFAWPF.Services;

public class ApplicationHostService(IServiceProvider serviceProvider) : IHostedService
{
    private Window _window;

    /// <summary>
    /// Triggered when the application host is ready to start the service.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await HandleActivationAsync();
    }

    /// <summary>
    /// Triggered when the application host is performing a graceful shutdown.
    /// </summary>
    /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    /// <summary>
    /// Creates main window during activation.
    /// </summary>
    async private Task HandleActivationAsync()
    {
        await Task.CompletedTask;

        if (!Application.Current.Windows.OfType<RootView>().Any())
        {
            _window = (serviceProvider.GetService(typeof(RootView)) as Window)!;
            _window!.Show();

        }

        await Task.CompletedTask;
    }
}
