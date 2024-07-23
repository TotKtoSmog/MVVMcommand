using System.Windows;
using System.Windows.Input;

namespace MVVMcommand
{
    public class MouseButtonCommand
    {
        public static readonly DependencyProperty MouseDownCommandProperty =
        DependencyProperty.RegisterAttached(
            "MouseDownCommand",
            typeof(ICommand),
            typeof(MouseButtonCommand),
            new PropertyMetadata(null, OnMouseDownCommandChanged));

        public static readonly DependencyProperty MouseUpCommandProperty =
            DependencyProperty.RegisterAttached(
                "MouseUpCommand",
                typeof(ICommand),
                typeof(MouseButtonCommand),
                new PropertyMetadata(null, OnMouseUpCommandChanged));

        public static void SetMouseDownCommand(UIElement element, ICommand value)
            => element.SetValue(MouseDownCommandProperty, value);

        public static ICommand GetMouseDownCommand(UIElement element)
            => (ICommand)element.GetValue(MouseDownCommandProperty);

        public static void SetMouseUpCommand(UIElement element, ICommand value)
            => element.SetValue(MouseUpCommandProperty, value);

        public static ICommand GetMouseUpCommand(UIElement element)
            => (ICommand)element.GetValue(MouseUpCommandProperty);

        private static void OnMouseDownCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                element.MouseDown += (sender, args) =>
                {
                    var command = GetMouseDownCommand(element);
                    if (command != null && command.CanExecute(args))
                    {
                        command.Execute(args);
                    }
                };
            }
        }

        private static void OnMouseUpCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                element.MouseUp += (sender, args) =>
                {
                    var command = GetMouseUpCommand(element);
                    if (command != null && command.CanExecute(args))
                    {
                        command.Execute(args);
                    }
                };
            }
        }
    }
}