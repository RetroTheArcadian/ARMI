using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ARMI.Pages
{
    public partial class EmulatorsPage : UserControl
    {
        public EmulatorsPage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
