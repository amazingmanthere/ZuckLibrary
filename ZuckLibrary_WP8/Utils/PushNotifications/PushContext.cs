using Microsoft.Phone.Notification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZuckLibrary.Utils
{
    public class PushContext
    {
        private HttpNotificationChannel _notificationChannel;

        public static PushContext _current;
        public static Object LockObj = new Object();

        public static readonly string ChannelName = "QQBrowser";

        public event EventHandler<NotificationChannelErrorEventArgs> OnError;           // 发生错误
        public event EventHandler<NotificationChannelUriEventArgs> OnChannelUriUpdate;  // 通道Uri更新
        public event EventHandler<NotificationEventArgs> OnToastNotificationRecieved;   // 前台接收到Toast通知

        public static PushContext Current
        {
            get
            {
                if (_current == null)
                {
                    lock (LockObj)
                    {
                        if (_current == null)
                        {
                            _current = new PushContext();
                        }
                    }
                }

                return _current;
            }
        }

        public List<Uri> AllowedDomains = new List<Uri>()       // 允许的域名，用于瓷贴图片地址
        {
            new Uri("http://tp4.sinaimg.cn/")
        };

        /// <summary>
        /// 连接通道
        /// </summary>
        public void Connect()
        {
            if (IsChannelExist(PushContext.ChannelName))
            {
                Logger.Alert("notification channel was existed", _notificationChannel.ChannelUri.ToString());
                PrepareChannel();
            }
            else
            {
                CreateChannel(PushContext.ChannelName);

                BindToToastNotification();
                BindToTileNotification(AllowedDomains);
            }
        }

        /// <summary>
        /// 判断通道是否存在
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        public bool IsChannelExist(string channelName)
        {
            _notificationChannel = HttpNotificationChannel.Find(channelName);
            if (_notificationChannel == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 准备通道
        /// </summary>
        public void PrepareChannel()
        {
            if (_notificationChannel != null)
            {
                _notificationChannel.ChannelUriUpdated += NotificationChannel_ChannelUriUpdated;
                _notificationChannel.ErrorOccurred += NotificationChannel_ErrorOccurred;
                _notificationChannel.ShellToastNotificationReceived +=
                    NotificationChannel_ShellToastNotificationReceived;
            }
        }

        /// <summary>
        /// 创建通道
        /// </summary>
        /// <param name="channelName"></param>
        public void CreateChannel(string channelName)
        {
            _notificationChannel = new HttpNotificationChannel(channelName);

            PrepareChannel();

            _notificationChannel.Open();
        }

        /// <summary>
        /// 绑定Toast通知
        /// </summary>
        public void BindToToastNotification()
        {
            try
            {
                if (_notificationChannel != null && !_notificationChannel.IsShellToastBound)
                {
                    _notificationChannel.BindToShellToast();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 绑定Tile通知
        /// </summary>
        /// <param name="AllowedDomains"></param>
        public void BindToTileNotification(List<Uri> AllowedDomains = null)
        {
            try
            {
                if (_notificationChannel != null && !_notificationChannel.IsShellTileBound)
                {
                    if (AllowedDomains != null)
                    {
                        var listOfAllowedDomains = new Collection<Uri>(AllowedDomains);
                        _notificationChannel.BindToShellTile(listOfAllowedDomains);
                    }
                    else
                    {
                        _notificationChannel.BindToShellTile();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 解除Toast通知
        /// </summary>
        public void UnBindToToastNotification()
        {
            try
            {
                if (_notificationChannel.IsShellToastBound)
                {
                    _notificationChannel.UnbindToShellToast();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 解除Tile通知
        /// </summary>
        public void UnBindToTileNotification()
        {
            try
            {
                if (_notificationChannel.IsShellTileBound)
                {
                    _notificationChannel.UnbindToShellTile();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 是否注册了Toast通知
        /// </summary>
        /// <returns></returns>
        public bool IsToastNotificationBound()
        {
            if (_notificationChannel == null || !_notificationChannel.IsShellToastBound)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 是否注册了Tile通知
        /// </summary>
        /// <returns></returns>
        public bool IsTileNotificationBound()
        {
            if (_notificationChannel == null || !_notificationChannel.IsShellTileBound)
            {
                return false;
            }

            return true;
        }

        private void NotificationChannel_ShellToastNotificationReceived(object sender, NotificationEventArgs e)
        {
            StringBuilder message = new StringBuilder();
            string relativeUri = string.Empty;

            message.AppendFormat("Received Toast {0}:\n", DateTime.Now.ToShortTimeString());

            // Parse out the information that was part of the message.
            foreach (string key in e.Collection.Keys)
            {
                message.AppendFormat("{0}: {1}\n", key, e.Collection[key]);

                if (string.Compare(
                    key,
                    "wp:Param",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.CompareOptions.IgnoreCase) == 0)
                {
                    relativeUri = e.Collection[key];
                }
            }

            Logger.Alert("PushChannel_ShellToastNotificationReceived", message.ToString());

            // Display a dialog of all the fields in the toast.
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show(message.ToString());
            });
        }

        private void NotificationChannel_ErrorOccurred(object sender, NotificationChannelErrorEventArgs e)
        {
            string errorStr = String.Format("A push notification {0} error occurred.  {1} ({2}) {3}",
                e.ErrorType, e.Message, e.ErrorCode, e.ErrorAdditionalData);

            Logger.Alert("PushChannel_ErrorOccurred", errorStr);

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show(errorStr);
            });
        }

        private void NotificationChannel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            Logger.Alert("PushChannel_ChannelUriUpdated", _notificationChannel.ChannelUri.ToString());

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                // Display the new URI for testing purposes.   Normally, the URI would be passed back to your web service at this point.
                MessageBox.Show(String.Format("Channel Uri is {0}",
                    e.ChannelUri.ToString()));
            });
        }
    }
}
