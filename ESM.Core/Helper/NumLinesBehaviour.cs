using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace ESM.Core.Helper
{
    public class NumLinesBehaviour : Behavior<TextBlock>
    {
        TextBlock textBlock => AssociatedObject;

        public static readonly DependencyProperty MaxLinesProperty =
            DependencyProperty.RegisterAttached(
                "MaxLines",
                typeof(int),
                typeof(NumLinesBehaviour),
                new PropertyMetadata(default(int), OnMaxLinesPropertyChangedCallback));

        public static void SetMaxLines(DependencyObject element, int value)
        {
            element.SetValue(MaxLinesProperty, value);
        }

        public static int GetMaxLines(DependencyObject element)
        {
            return (int)element.GetValue(MaxLinesProperty);
        }

        private static void OnMaxLinesPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBlock element = d as TextBlock;
            element.MaxHeight = getLineHeight(element) * GetMaxLines(element);
        }

        public static readonly DependencyProperty MinLinesProperty =
            DependencyProperty.RegisterAttached(
                "MinLines",
                typeof(int),
                typeof(NumLinesBehaviour),
                new PropertyMetadata(default(int), OnMinLinesPropertyChangedCallback));

        public static void SetMinLines(DependencyObject element, int value)
        {
            element.SetValue(MinLinesProperty, value);
        }

        public static int GetMinLines(DependencyObject element)
        {
            return (int)element.GetValue(MinLinesProperty);
        }

        private static void OnMinLinesPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBlock element = d as TextBlock;
            element.MinHeight = getLineHeight(element) * GetMinLines(element);
        }

        private static double getLineHeight(TextBlock textBlock)
        {
            double lineHeight = textBlock.LineHeight;
            if (double.IsNaN(lineHeight))
                lineHeight = Math.Ceiling(textBlock.FontSize * textBlock.FontFamily.LineSpacing);
            return lineHeight;
        }
    }
}
