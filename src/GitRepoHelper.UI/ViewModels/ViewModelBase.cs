using Avalonia.Notification;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GitRepoHelper.UI.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject
    {
        public INotificationMessageManager NotificationManager { get; } = new NotificationMessageManager();


        protected INotificationMessage CreateMsg(string title, string message, Action<INotificationMessageButton>? onOk = null, Action<INotificationMessageButton>? onIgnore = null)
        {
            var msgBuilder =
            NotificationManager.CreateMessage()
                .Accent("#1751C3")
                .HasHeader(title)
                .HasMessage(message);
            if (onOk != null)
                msgBuilder
                .WithButton("Ok", onOk);
            if (onIgnore != null)
                msgBuilder
                .Dismiss().WithButton("Ignore", onIgnore);
            return msgBuilder.Queue();
        }

        protected INotificationMessage CreateWarningMsg(string message, Action<INotificationMessageButton>? onOk = null, Action<INotificationMessageButton>? onIgnore = null) =>
            CreateMsg("Warning", message, onOk, onIgnore ?? DefaultIgnore);

        private void DefaultIgnore(INotificationMessageButton btn) { }
    }
}
