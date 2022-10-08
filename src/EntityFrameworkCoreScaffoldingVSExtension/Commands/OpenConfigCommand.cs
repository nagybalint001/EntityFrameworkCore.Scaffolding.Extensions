namespace EntityFrameworkCoreScaffoldingVSExtension
{
    [Command(PackageIds.OpenConfigCommand)]
    internal sealed class OpenConfigCommand : BaseCommand<OpenConfigCommand>
    {
        protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
        {
            var view = new ScaffoldingSettingsView();
            view.ShowModal();
        }
    }
}
