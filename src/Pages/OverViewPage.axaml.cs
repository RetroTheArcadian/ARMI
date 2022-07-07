using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ARMI.Pages
{
    public partial class OverViewPage : UserControl
    {
        public OverViewPage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
