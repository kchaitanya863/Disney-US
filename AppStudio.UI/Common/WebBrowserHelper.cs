using System;
using System.Windows;
using Microsoft.Phone.Controls;

namespace AppStudio
{
    public class WebBrowserHelper
    {
        public static readonly DependencyProperty HtmlProperty =
            DependencyProperty.RegisterAttached("Html", 
            typeof(string), 
            typeof(WebBrowserHelper), 
            new PropertyMetadata(null, OnHtmlChanged));

        public static string GetHtml(DependencyObject obj)
        {
            return (string)obj.GetValue(HtmlProperty);
        }

        public static void SetHtml(DependencyObject obj, string value)
        {
            obj.SetValue(HtmlProperty, value);
        }

        private static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser webBrowser = d as WebBrowser;
            if (webBrowser != null)
            {
                if (e.NewValue == null)
                {
                    webBrowser.Navigate(new Uri("about:blank"));
                }
                else 
                {
                    webBrowser.NavigateToString(string.Format("<!DOCTYPE html><html><head><meta name='viewport' content='width=device-width, height=device-height, initial-scale=1, user-scalable=no'/></head><body bgcolor=\"black\"><iframe frameborder=\"0\" width=\"100%\" height=\"100%\" src=\"{0}\"/></body></html>", e.NewValue));
                }
            }
        }
    }
}
