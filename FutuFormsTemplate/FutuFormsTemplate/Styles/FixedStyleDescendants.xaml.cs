using Xamarin.Forms;

namespace FutuFormsTemplate.Styles
{
    /// <summary>
    /// This resource dictionary contains any styles that descend from our "Fixed" styles, as described in App.xaml.
    /// </summary>
    public partial class FixedStyleDescendants : ResourceDictionary
    {
        public FixedStyleDescendants()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Merges the contents of this dictionary with the main App ResourceDictionary, then clears the contents of this dictionary.
        /// Will override entries already in the App's ResourceDictionary with the same key.
        /// Used as a workaround for the MergedWith directive not working correctly.
        /// </summary>
        public void MergeWithMainDictionary()
        {
            /* It seems as though attempting to merge this into the main Application ResourceDictionary
             * with a MergedWith directive at runtime causes it to fail to resolve StaticResources. So,
             * instead of using the MergedWith directive over in App.xaml.cs, we're just going to take the
             * resources from here, and add them to the main App ResourceDictionary.
             */

            foreach (var entry in this)
            {
                if (App.Current.Resources.ContainsKey(entry.Key))
                {
                    App.Current.Resources[entry.Key] = entry.Value;
                }
                else
                {
                    App.Current.Resources.Add(entry.Key, entry.Value);
                }
            }
            this.Clear();
        }
    }
}
