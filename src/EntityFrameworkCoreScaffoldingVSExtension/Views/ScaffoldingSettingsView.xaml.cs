using Microsoft.VisualStudio.PlatformUI;

using System.Windows;
using System.Windows.Controls;

namespace EntityFrameworkCoreScaffoldingVSExtension
{
    public partial class ScaffoldingSettingsView : DialogWindow
    {
        public ScaffoldingSettingsView()
        {
            InitializeComponent();
            HasMaximizeButton = true;
            HasMinimizeButton = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            VS.MessageBox.Show("ScaffoldingSettingsControl", "Button clicked");
        }
    }
}
