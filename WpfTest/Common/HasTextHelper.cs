using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfTest.Common
{
    public class HasTextHelper
    {
        public static bool GetMonitorTextChange(DependencyObject obj)
        {
            return (bool)obj.GetValue(MonitorTextChangeProperty);
        }

        public static void SetMonitorTextChange(DependencyObject obj, bool value)
        {
            obj.SetValue(MonitorTextChangeProperty, value);
        }

        public static readonly DependencyProperty MonitorTextChangeProperty =
            DependencyProperty.RegisterAttached(
                "MonitorTextChange",
                typeof(bool),
                typeof(HasTextHelper),
                new PropertyMetadata(false, OnMonitorTextChange)
            );

        public static bool GetIsCheck(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCheckProperty);
        }

        public static void SetIsCheck(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCheckProperty, value);
        }

        public static readonly DependencyProperty IsCheckProperty =
            DependencyProperty.RegisterAttached(
                "IsCheck",
                typeof(bool),
                typeof(HasTextHelper),
                new PropertyMetadata(false)
            );

        private static void OnMonitorTextChange(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e
        )
        {
            TextBox textBox = (TextBox)d;
            if (textBox == null)
                return;

            if ((bool)e.OldValue)
                textBox.TextChanged -= TextBox_TextChanged;

            if ((bool)e.NewValue)
                textBox.TextChanged += TextBox_TextChanged;
        }

        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if(textBox.Text.Length > 0) 
                SetIsCheck(textBox, true);
            else 
                SetIsCheck(textBox, false);
        }
    }
}
