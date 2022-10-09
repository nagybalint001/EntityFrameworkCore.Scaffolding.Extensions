using EntityFrameworkCoreScaffoldingVSExtension.Services;
using EntityFrameworkCoreScaffoldingVSExtension.Views;

namespace EntityFrameworkCoreScaffoldingVSExtension;

[Command(PackageIds.OpenConfigCommand)]
internal sealed class OpenConfigCommand : BaseCommand<OpenConfigCommand>
{
    protected override async Task ExecuteAsync(OleMenuCmdEventArgs e)
    {
        var settings = await ConfigurationService.LoadSettingsAsync();
        settings ??= ConfigurationService.GetInitialModel();

        var view = new ScaffoldingSettingsDialog(new ScaffoldingSettingsVM(settings));
        view.ShowModal();
    }
}
