namespace EntityFrameworkCoreScaffoldingVSExtension
{
    [Command(PackageIds.RunScaffoldingCommand)]
    internal sealed class RunScaffoldingCommand : BaseCommand<RunScaffoldingCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            await VS.MessageBox.ShowWarningAsync("RunScaffoldingCommand", "Button clicked");
        }
    }
}
