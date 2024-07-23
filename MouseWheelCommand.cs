using System.Windows;
using System.Windows.Input;

namespace MVVMcommand
{
    public class MouseWheelCommand
    {
        public static readonly DependencyProperty MouseWheelCommandProperty =
        DependencyProperty.RegisterAttached(
            "MouseWheelCommand",
            typeof(ICommand),
            typeof(MouseWheelCommand),
            new PropertyMetadata(null, OnMouseWheelCommandChanged));

        public static void SetMouseWheelCommand(UIElement element, ICommand value)
            => element.SetValue(MouseWheelCommandProperty, value);

        public static ICommand GetMouseWheelCommand(UIElement element)
            => (ICommand)element.GetValue(MouseWheelCommandProperty);

        private static void OnMouseWheelCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                element.MouseWheel += (sender, args) =>
                {
                    var command = GetMouseWheelCommand(element);
                    if (command != null && command.CanExecute(args))
                    {
                        command.Execute(args);
                    }
                };
            }
        }
    }
}