using Avalonia.Controls;
using Avalonia.Notification;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitRepoHelper.UI.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject
    {
        public Window? Parent { get; set; }
        public INotificationMessageManager NotificationManager { get; } = new NotificationMessageManager();
    }
}
