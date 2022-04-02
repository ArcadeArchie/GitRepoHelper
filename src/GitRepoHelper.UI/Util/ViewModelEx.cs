using Avalonia.Notification;
using GitRepoHelper.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitRepoHelper.UI.Util
{
    public static class ViewModelEx
    {

        public static INotificationMessage CreateMsg(this ViewModelBase vm, string title, string message, Action<INotificationMessageButton>? onOk = null, Action<INotificationMessageButton>? onIgnore = null)
        {
            var msgBuilder =
            vm.NotificationManager.CreateMessage()
                .Accent("#1751C3")
                .HasHeader(title)
                .HasMessage(message);
            if (onOk != null)
                msgBuilder
                .Dismiss().WithButton("Ok", onOk);
            if (onIgnore != null)
                msgBuilder
                .Dismiss().WithButton("Ignore", onIgnore);
            return msgBuilder.Queue();
        }

        public static INotificationMessage CreateWarningMsg(this ViewModelBase vm, string message, Action<INotificationMessageButton>? onOk = null, Action<INotificationMessageButton>? onIgnore = null) =>
            CreateMsg(vm, "Warning", message, onOk ?? DefaultOk, onIgnore ?? DefaultIgnore);

        private static void DefaultOk(INotificationMessageButton btn) { }
        private static void DefaultIgnore(INotificationMessageButton btn) { }
    }
}
