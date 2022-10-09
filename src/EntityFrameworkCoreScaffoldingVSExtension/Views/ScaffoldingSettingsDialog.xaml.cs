using EntityFrameworkCoreScaffoldingVSExtension.Views;

using Microsoft.VisualStudio.PlatformUI;

namespace EntityFrameworkCoreScaffoldingVSExtension
{
    public partial class ScaffoldingSettingsDialog : DialogWindow
    {
        public ScaffoldingSettingsDialog(ScaffoldingSettingsVM vm)
        {
            InitializeComponent();
            DataContext = vm;
            vm.CloseAction ??= new Action(Close);
        }
    }
}
