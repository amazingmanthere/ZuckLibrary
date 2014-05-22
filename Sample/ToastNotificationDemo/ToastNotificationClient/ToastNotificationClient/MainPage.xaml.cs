using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Notification;
using Microsoft.Phone.Shell;
using ToastNotificationClient.Resources;
using ZuckLibrary.Utils;

namespace ToastNotificationClient
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            var pushContext = PushContext.Current;
            pushContext.Connect();

            this.Loaded += MainPage_Loaded;
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateControl();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            var pushContext = PushContext.Current;

            if (ToastBtn == sender)
            {
                if (pushContext.IsToastNotificationBound())
                {
                    pushContext.UnBindToToastNotification();
                }
                else
                {
                    pushContext.BindToToastNotification();
                }
            }
            else if (TileBtn == sender)
            {
                if (pushContext.IsTileNotificationBound())
                {
                    pushContext.UnBindToTileNotification();
                }
                else
                {
                    pushContext.BindToTileNotification();
                }
            }

            UpdateControl();
        }

        private void UpdateControl()
        {
            var pushContext = PushContext.Current;
            if (pushContext.IsToastNotificationBound())
            {
                ToastBtn.Content = "解除Toast通知";
            }
            else
            {
                ToastBtn.Content = "注册Toast通知";
            }

            if (pushContext.IsTileNotificationBound())
            {
                TileBtn.Content = "解除Tile通知";
            }
            else
            {
                TileBtn.Content = "注册Tile通知";
            }
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}