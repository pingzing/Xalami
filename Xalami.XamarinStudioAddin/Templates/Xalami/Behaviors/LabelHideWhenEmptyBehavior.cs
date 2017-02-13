using System;
using Xamarin.Forms;

namespace ${ProjectName}.Behaviors
{
    /// <summary>
    /// A behavior that will automatically hide a label if its Text property is empty or whitespace.
    /// </summary>
    public static class LabelHideWhenEmptyBehavior
    {
        /// <summary>
        /// The attached behavior that enables label-hiding when empty.
        /// </summary>
        public static readonly BindableProperty AttachBehaviorProperty = BindableProperty.CreateAttached(
            "AttachBehavior",
            typeof(bool),
            typeof(LabelHideWhenEmptyBehavior),
            false,
            propertyChanged: OnAttachBehaviorChanged);

        public static bool GetAttachBehavior(BindableObject view)
        {
            return (bool)view.GetValue(AttachBehaviorProperty);
        }

        public static void SetAttachBehavior(BindableObject view, bool value)
        {
            view.SetValue(AttachBehaviorProperty, value);
        }

        private static void OnAttachBehaviorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var label = bindable as Label;
            if (label == null)
            {
                return;
            }

            bool attachBehavior = (bool)newValue;
            if (attachBehavior)
            {
                //actually only interested in Text changing, but there's no TextChanged event for a Label.
                label.PropertyChanged += Label_PropertyChanged;
            }
            else
            {
                label.PropertyChanged -= Label_PropertyChanged;
            }
        }

        private static void Label_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //We listen for Renderer too, because initial assignment of the Text field doesn't actually seem to trigger a PropertyChanged for the Text property.
            if (!(e.PropertyName == nameof(Label.Text) || e.PropertyName == "Renderer"))
            {
                return;
            }
            Label label = sender as Label;
            if (label == null)
            {
                return;
            }

            if (String.IsNullOrWhiteSpace(label.Text))
            {
                label.IsVisible = false;
            }
            else
            {
                label.IsVisible = true;
            }

        }
    }
}

