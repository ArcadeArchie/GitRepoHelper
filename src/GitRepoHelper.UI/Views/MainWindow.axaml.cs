using Avalonia.Controls;
using Avalonia.Interactivity;
using GitRepoHelper.Models;
using System;
using System.Linq;

namespace GitRepoHelper.UI.Views
{
    public partial class MainWindow : Window
    {
        private readonly TabControl _tabs;
        public MainWindow()
        {
            InitializeComponent();
            _tabs = this.FindControl<TabControl>("Tabs");
            ListBoxItem.DoubleTappedEvent.AddClassHandler<ListBoxItem>(ItemClickedHandler);
        }

        void ItemClickedHandler(object sender, RoutedEventArgs e)
        {
            if (sender is not ListBoxItem listItem || listItem.DataContext is not WatchedPath item)
                return;
            if(_tabs.Items is Avalonia.Collections.AvaloniaList<object> items)
            {
                var tab = items.Select(x => (x as TabItem)?.DataContext as WatchedPath).FirstOrDefault(x => x?.Path == item.Path);
                if (tab != null)
                {
                    _tabs.SelectedIndex = items.Select(x => (x as TabItem).DataContext)?.ToList<object>().IndexOf(tab) ?? 0;
                    
                }
                else
                    items.Add(new TabItem { DataContext = item, Header = item.DisplayName ?? $"Watched Dir{items.Count}" });
            }
        }
    }
}
