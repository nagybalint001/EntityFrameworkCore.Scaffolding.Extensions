using EntityFrameworkCoreScaffoldingVSExtension.Models;
using EntityFrameworkCoreScaffoldingVSExtension.Services;

using System.Diagnostics;
using System.IO;
using System.Linq;

namespace EntityFrameworkCoreScaffoldingVSExtension;

[Command(PackageIds.RunScaffoldingCommand)]
internal sealed class RunScaffoldingCommand : BaseCommand<RunScaffoldingCommand>
{
    private Process _currentProcess;
    private bool _hasError = false;
    private OutputWindowPane _outputPane;

    protected override async Task InitializeCompletedAsync()
    {
        _outputPane = await VS.Windows.CreateOutputWindowPaneAsync("DB Scaffolding");
    }

    protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
    {
        if (_currentProcess != null)
        {
            await VS.MessageBox.ShowWarningAsync("Scaffolding in progress!", "You cannot start multiple scaffolding processes.");
            return;
        }

        var settings = await ConfigurationService.LoadSettingsAsync();
        var project = await VS.Solutions.GetActiveProjectAsync();
        var projectDirectory = Path.GetDirectoryName(project.FullPath);

        if (settings == null)
        {
            await VS.MessageBox.ShowWarningAsync("Scaffolding failed!", $"{ConfigurationService.SettingsFileName} couldn't be found.");
            await VS.StatusBar.ShowMessageAsync("Scaffolding failed!");
            await _outputPane.WriteLineAsync($"Scaffolding failed!\n{ConfigurationService.SettingsFileName} couldn't be found.");
            return;
        }

        var command = CreateCommandString(settings);

        _hasError = false;
        _currentProcess = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = "dotnet",
                Arguments = command,
                WorkingDirectory = projectDirectory,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            },
            EnableRaisingEvents = true,
        };

        _currentProcess.Exited += OnFinished;
        _currentProcess.OutputDataReceived += OnMessageRecieved;
        _currentProcess.ErrorDataReceived += OnErrorRecieved;

        await _outputPane.ClearAsync();
        await _outputPane.ActivateAsync();
        await VS.StatusBar.StartAnimationAsync(StatusAnimation.Sync);
        await VS.StatusBar.ShowMessageAsync("Scaffolding started!");
        await _outputPane.WriteLineAsync("Scaffolding started!");
        await _outputPane.WriteLineAsync($"dotnet {command}");

        _currentProcess.Start();
        _currentProcess.BeginOutputReadLine();
        _currentProcess.BeginErrorReadLine();
    }

    private static string CreateCommandString(ScaffoldingSettings settings)
    {
        var command = $"ef dbcontext scaffold \"{settings.DataSource}\" {settings.Provider}";

        if (!string.IsNullOrEmpty(settings.ContextName))
            command += $" --context {settings.ContextName}";

        if (!string.IsNullOrEmpty(settings.ContextDirectory))
            command += $" --context-dir {settings.ContextDirectory}";

        if (!string.IsNullOrEmpty(settings.OutputDirectory))
            command += $" --output-dir {settings.OutputDirectory}";

        if (!string.IsNullOrEmpty(settings.ContextNamespace))
            command += $" --context-namespace {settings.ContextNamespace}";

        if (!string.IsNullOrEmpty(settings.Namespace))
            command += $" --namespace {settings.Namespace}";

        if (!string.IsNullOrEmpty(settings.Schemas))
            command += string.Join("", settings.Schemas.Split(',').Select(x => $" --schema {x}"));

        if (!string.IsNullOrEmpty(settings.Tables))
            command += string.Join("", settings.Tables.Split(',').Select(x => $" --table {x}"));

        if (!string.IsNullOrEmpty(settings.ProjectDirectory))
            command += $" --project {settings.ProjectDirectory}";

        if (!string.IsNullOrEmpty(settings.StartupProjectDirectory))
            command += $" --startup-project {settings.StartupProjectDirectory}";

        if (!string.IsNullOrEmpty(settings.TargetFramework))
            command += $" --framework {settings.TargetFramework}";

        if (!string.IsNullOrEmpty(settings.Configuration))
            command += $" --configuration";

        if (!string.IsNullOrEmpty(settings.TargetRuntime))
            command += $" --runtime {settings.TargetRuntime}";

        if (settings.DataAnnotations)
            command += " --data-annotations";

        if (settings.UseDatabaseNames)
            command += " --use-database-names";

        if (settings.NoOnConfiguring)
            command += " --no-onconfiguring";

        if (settings.NoPluralize)
            command += " --no-pluralize";

        if (settings.Force)
            command += " --force";

        if (settings.NoBuild)
            command += " --no-build";

        if (settings.Verbose)
            command += " --verbose";

        return command;
    }

    private async void OnFinished(object sender, EventArgs e)
    {
        if (_currentProcess.ExitCode != 0)
            _hasError = true;

        var message = _hasError ? "Scaffolding failed!" : "Scaffolding succeeded!";
        await VS.StatusBar.EndAnimationAsync(StatusAnimation.Sync);
        await _outputPane.WriteLineAsync(message);
        await VS.StatusBar.ShowMessageAsync(message);

        try
        {
            _currentProcess.Dispose();
        }
        finally
        {
            _currentProcess = null;
        }
    }

    private async void OnMessageRecieved(object sender, DataReceivedEventArgs e)
    {
        if (e.Data == null)
            return;

        await _outputPane.WriteLineAsync(e.Data);
    }

    private async void OnErrorRecieved(object sender, DataReceivedEventArgs e)
    {
        if (e.Data == null)
            return;

        _hasError = true;

        await _outputPane.WriteLineAsync(e.Data);
    }
}
