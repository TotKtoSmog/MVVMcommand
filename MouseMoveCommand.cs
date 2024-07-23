using System.Windows;
using System.Windows.Input;

namespace MVVMcommand
{
    public class MouseMoveCommand
    {
        public static readonly DependencyProperty MouseEnterCommandProperty =
        DependencyProperty.RegisterAttached(
            "MouseEnterCommand",
            typeof(ICommand),
            typeof(MouseMoveCommand),
            new PropertyMetadata(null, OnMouseEnterCommandChanged));

        public static readonly DependencyProperty MouseLeaveCommandProperty =
            DependencyProperty.RegisterAttached(
                "MouseLeaveCommand",
                typeof(ICommand),
                typeof(MouseMoveCommand),
                new PropertyMetadata(null, OnMouseLeaveCommandChanged));

        public static void SetMouseEnterCommand(UIElement element, ICommand value)
            => element.SetValue(MouseEnterCommandProperty, value);

        public static ICommand GetMouseEnterCommand(UIElement element)
            => (ICommand)element.GetValue(MouseEnterCommandProperty);

        public static void SetMouseLeaveCommand(UIElement element, ICommand value)
            => element.SetValue(MouseLeaveCommandProperty, value);

        public static ICommand GetMouseLeaveCommand(UIElement element)
            => (ICommand)element.GetValue(MouseLeaveCommandProperty);

        private static void OnMouseEnterCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                element.MouseEnter += (sender, args) =>
                {
                    var command = GetMouseEnterCommand(element);
                    if (command != null && command.CanExecute(null))
                    {
                        command.Execute(null);
                    }
                };
            }
        }

        private static void OnMouseLeaveCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is UIElement element)
            {
                element.MouseLeave += (sender, args) =>
                {
                    var command = GetMouseLeaveCommand(element);
                    if (command != null && command.CanExecute(null))
                    {
                        command.Execute(null);
                    }
                };
            }
        }
    }
}