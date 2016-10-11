using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kea.UI
{
    public static class Focus
    {
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
        }
    }
}
