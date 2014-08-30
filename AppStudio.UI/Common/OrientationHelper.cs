using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Orientation = Microsoft.Phone.Controls.PageOrientation;

namespace AppStudio
{
    public static class OrientationHelper
    {
        private const String VisualStateGroupName = "PageOrientationStates";
        

        #region IsActive DP

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.RegisterAttached("IsActive", typeof (bool), typeof (OrientationHelper),
                                                new PropertyMetadata(default(bool), OnIsActivePropertyChanged));

        public static void SetIsActive(UIElement element, bool value)
        {
            element.SetValue(IsActiveProperty, value);
        }

        public static bool GetIsActive(UIElement element)
        {
            return (bool) element.GetValue(IsActiveProperty);
        }

        private static void OnIsActivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Control;
            if (control == null)
                return;

            SetupOrientationAwareControl(control, (bool)e.NewValue);

#if WINDOWS_PHONE
            //The control itself can be a page.In every case we need to retry until the containing page is loaded.
            control.Loaded += OnControlLoaded;
#endif

        }

        #endregion

        #region OrientationAwareControls Private DP

        private static readonly DependencyProperty OrientationAwareControlsProperty =
            DependencyProperty.RegisterAttached("OrientationAwareControls", typeof (IList<Control>),
                                                typeof (OrientationHelper), null);

        private static void SetOrientationAwareControls(DependencyObject element, IList<Control> value)
        {
            element.SetValue(OrientationAwareControlsProperty, value);
        }

        private static IList<Control> GetOrientationAwareControls(DependencyObject element)
        {
            return (IList<Control>) element.GetValue(OrientationAwareControlsProperty);
        }

        #endregion

        private static void SetupOrientationAwareControl(Control control, bool isActive)
        {
            var page = FindParentPage(control);
            if (page == null)
                return;
            if (isActive)
            {
                //Add the orientation aware control to the list stored on the Page
                if (GetOrientationAwareControls(page) == null)
                {
                    SetOrientationAwareControls(page, new List<Control>());
                    //Start listening for changes
                    page.OrientationChanged += PageOnOrientationChanged;
                }
                if (!GetOrientationAwareControls(page).Contains(control))
                    GetOrientationAwareControls(page).Add(control);
                UpdateOrientationAwareControls(new[] { control }, page.Orientation); //Update this control only

            }
            else
            {
                var orientationAwareControls = GetOrientationAwareControls(page);
                if (orientationAwareControls != null)
                {
                    if (orientationAwareControls.Contains(control))
                    {
                        orientationAwareControls.Remove(control);
                        control.Loaded -= OnControlLoaded;
                    }
                    //Do cleanup for this page if there are no more controls
                    if (!orientationAwareControls.Any())
                    {
                        SetOrientationAwareControls(page, null);
                        page.OrientationChanged -= PageOnOrientationChanged;
                    }
                }
            }
        }


        private static void OnControlLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var control = sender as Control;
            SetupOrientationAwareControl(control, GetIsActive(control));
        }

        private static PhoneApplicationPage FindParentPage(FrameworkElement el)
        {
            FrameworkElement parent = el;
            while(parent != null)
            {
                if (parent is PhoneApplicationPage)
                    return parent as PhoneApplicationPage;
                parent = parent.Parent as FrameworkElement;
            }
            return null;
        }

        private static void PageOnOrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            var page = sender as PhoneApplicationPage;
            UpdateOrientationAwareControls(GetOrientationAwareControls(page), page.Orientation); //Update all controls
        }

        private static void UpdateOrientationAwareControls(IEnumerable<Control> controls, Orientation orientation)
        {
            if (controls == null || !controls.Any())
                return;

            foreach (var control in controls)
            {
                VisualStateManager.GoToState(control, GetVisualStateName(orientation), true);
            }
        }

        private static String GetVisualStateName(Orientation orientation)
        {
            return orientation.ToString();
        }
    }
}
