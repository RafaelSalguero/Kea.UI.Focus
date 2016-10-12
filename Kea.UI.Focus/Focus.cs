using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Kea.UI
{
    public static class Focus
    {
        /// <summary>
        /// Dependency property that focus the first control of the window when is set to true
        /// </summary>
        #region FirstControlFocusProperty
        public static readonly DependencyProperty FirstControlFocusProperty
            = DependencyProperty.RegisterAttached("FirstControlFocus", typeof(bool), typeof(Focus), new PropertyMetadata(false, FirstControlFocusChanged));

        public static bool GetFirstControlFocus(DependencyObject obj)
        {
            return (bool)obj.GetValue(FirstControlFocusProperty);
        }

        public static void SetFirstControlFocus(DependencyObject obj, bool value)
        {
            obj.SetValue(FirstControlFocusProperty, value);
        }

        private static void FirstControlFocusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //We take the dependency object d "sender" as a FrameworkElement, because it implements the event "Loaded"
            var element = d as FrameworkElement;

            //If the element is null, we do not do anything
            if (element == null)
                return;

            //If the value of the dependency property is set to true, the windows moves the focus to the next item
            if ((bool)e.NewValue) element.Loaded += MoveToFirstElement;
            else element.Loaded -= MoveToFirstElement;
        }

        private static void MoveToFirstElement(object sender, RoutedEventArgs e)
        {
            //If the "Enter" key was pressed then:
            //We take the object "sender" as a UIElement
            var element = sender as FrameworkElement;

            //Finally we move the focus to the next element
            if (element != null) element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        #endregion

        /// <summary>
        /// Dependency property that when is set to true, focus the next control when the "Enter" key is pressed
        /// </summary>
        #region AdvanceOnEnterKeyProperty
        public static readonly DependencyProperty AdvanceOnEnterKeyProperty
            = DependencyProperty.RegisterAttached("AdvanceOnEnterKey", typeof(bool), typeof(Focus), new PropertyMetadata(false, AdvanceOnEnterKeyChanged));

        //Faltan los metodos Get[nombre propiedad] y Set[nombre propiedad]

        public static bool GetAdvanceOnEnterKey(DependencyObject obj)
        {
            return (bool)obj.GetValue(AdvanceOnEnterKeyProperty);
        }

        public static void SetAdvanceOnEnterKey(DependencyObject obj, bool value)
        {
            obj.SetValue(AdvanceOnEnterKeyProperty, value);
        }

        private static void AdvanceOnEnterKeyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //We take the dependency object d "sender" as a UIElement, because it implements the event "PreviewKeyDown"
            var element = d as UIElement;

            //If the element is null, we do not do anything
            if (element == null)
                return;

            //If the value of the dependency property is set to true, we subscribe the method "MoveToNextElemnt" to the event "PreviewKeyDown"
            if ((bool)e.NewValue) element.PreviewKeyDown += MoveToNextElement;
            //If the value of the dependency property is set to false, we unsubscribe the method "MoveToNextElemnt" to the event "PreviewKeyDown", for security reasons
            else element.PreviewKeyDown -= MoveToNextElement;
        }

        static void MoveToNextElement(object sender, KeyEventArgs e)
        {
            //Determine if the pressed key was the "Enter" key, if not, we are not going to do anything
            if (!e.Key.Equals(Key.Enter)) return;

            //If the "Enter" key was pressed then:
            //We take the object "sender" as a UIElement
            var element = sender as UIElement;

            var ignorar = (sender is TextBoxBase) && ((TextBoxBase)sender).AcceptsReturn;

            if (!ignorar)
            {
                //We take the object actually being focus by the keyword, this is not always the element who trigger the event KeyDown
                var elementWithFocus = Keyboard.FocusedElement as UIElement;
                //We set the Handled property of the event "PreviewKeyDown" to true, that is intended to no other control do nothing when this key is pressed
                e.Handled = true;
                //Finally we move the focus to the next element
                if (elementWithFocus != null) elementWithFocus.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }
        #endregion
    }
}
