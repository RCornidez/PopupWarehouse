using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Windows;

public partial class App : Application
{
    public static IConfiguration Configuration { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        Configuration = builder.Build();

        base.OnStartup(e);
    }
}

