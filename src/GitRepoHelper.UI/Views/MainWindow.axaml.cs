using Avalonia.Controls;
using Avalonia.Interactivity;
using GitRepoHelper.Models;
using GitRepoHelper.UI.ViewModels;
using System;
using System.Linq;

namespace GitRepoHelper.UI.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;
        private readonly TabControl _tabs;
        public MainWindow()
        {
            DataContext = _viewModel = new MainWindowViewModel() { Parent = this };
            InitializeComponent();
            _tabs = this.FindControl<TabControl>("Tabs");
            ListBoxItem.DoubleTappedEvent.AddClassHandler<ListBoxItem>(ItemClickedHandler);
        }

        //protected override async void OnInitialized()
        //{
        //    base.OnInitialized();
        //    await _viewModel.OnAppearingAsync();
        //}

        void ItemClickedHandler(object sender, RoutedEventArgs e)
        {
            if (sender is not ListBoxItem listItem || listItem.DataContext is not WatchedPath item)
                return;
            if (_tabs.Items is Avalonia.Collections.AvaloniaList<object> items)
            {
                var tab = items.Select(x => (x as TabItem)?.DataContext as WatchedPath).FirstOrDefault(x => x?.Path == item.Path);
                if (tab != null)
                {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    _tabs.SelectedIndex = items.Select(x => (x as TabItem).DataContext)?.ToList<object>().IndexOf(tab) ?? 0;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

                }
                else
                    items.Add(new TabItem { Header = item.DisplayName ?? $"Watched Dir{items.Count}", Content = new WatchedDirView { DataContext = new WatchedDirViewModel(item) } });
            }
        }

        public void OnRemoveTab(object? sender, RoutedEventArgs e)
        {
            if (sender is not null && sender is Button btn)
                if (btn.Parent is not null && btn.Parent.Parent is TabItem item &&
                    _tabs.Items is Avalonia.Collections.AvaloniaList<object> items)
                    items.Remove(item);
        }

    }
}
