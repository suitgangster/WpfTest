using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfTest.Common
{
    public class PasswordHelper
    {
        public static string GetPassword(DependencyObject obj)
        {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject obj, string value)
        {
            obj.SetValue(PasswordProperty, value);
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached(
                "Password",
                typeof(string),
                typeof(PasswordHelper),
                new PropertyMetadata("", OnPropertyChanged)
            );

        public static bool GetAttach(DependencyObject obj)
        {
            return (bool)obj.GetValue(AttachProperty);
        }

        public static void SetAttach(DependencyObject obj, bool value)
        {
            obj.SetValue(AttachProperty, value);
        }

        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached(
                "Attach",
                typeof(bool),
                typeof(PasswordHelper),
                new PropertyMetadata(false, OnPasswordChange)
            );

        static bool _updatating = false;

        private static void OnPasswordChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox password)
            {
                if ((bool)e.NewValue)
                {
                    password.PasswordChanged += Password_PasswordChanged;
                }
                else if ((bool)e.OldValue)
                {
                    password.PasswordChanged -= Password_PasswordChanged;
                }
            }
        }

        private static void Password_PasswordChanged(object sender, RoutedEventArgs e)
       {
            if (sender is PasswordBox password)
            {
                _updatating = true;
                SetPassword(password, password.Password);
                _updatating = false;
            }
        }

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox password)
            {
                if (!_updatating)
                {
                    password.Password = e.NewValue?.ToString() ?? "";
                }
            }
        }
    }
}
