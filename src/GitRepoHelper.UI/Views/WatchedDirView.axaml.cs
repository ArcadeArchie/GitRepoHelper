using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GitRepoHelper.UI.Views
{
    public partial class WatchedDirView : UserControl
    {
        public WatchedDirView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
