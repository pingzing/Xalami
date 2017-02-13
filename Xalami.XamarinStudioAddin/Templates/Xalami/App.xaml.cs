using ${ProjectName}.Mvvm;
using ${ProjectName}.Styles;
using ${ProjectName}.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ${ProjectName}
{    
    public partial class App : Application
    {
        public ViewModelLocator Locator;

        public NavigationHost MainNavigationHost { get; set; }

        public App()
        {
            InitializeComponent();
            MainNavigationHost = new NavigationHost();
            Locator = new ViewModelLocator();
            
            SetupStyleOverrides();
            MergeFixedStyleDescendants();

            MainPage = new MainPage();
        }        

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        //See XAML file for explanation of what we're doing here.
        //All values are taken from the WP8.1 Phone themeresources.xaml or generic.xaml.
        private void SetupStyleOverrides()
        {
            //Global Label override
            Setter wpLabelFontSizeSetter = new Setter
            {
                Property = Label.FontSizeProperty,
                Value = 18.14
            };
            AddWindowsStyleOverride(Device.Styles.BodyStyle, typeof(Label), null, wpLabelFontSizeSetter);

            //BodyStyle
            Setter wpBodyFontSizeSetter = new Setter
            {
                Property = Label.FontSizeProperty,
                Value = 18.14
            };
            AddWindowsStyleOverride(Device.Styles.BodyStyle, typeof(Label), FixedStyleKeys.FixedBodyStyleKey,
                wpBodyFontSizeSetter);

            ////CaptionStyle
            Setter wpCaptionFontSizeSetter = new Setter
            {
                Property = Label.FontSizeProperty,
                Value = 16.14
            };
            Setter wpCaptionFontForegroundSetter = new Setter
            {
                Property = Label.TextColorProperty,
                Value = new Color(0, 0, 0, .43)
            };
            AddWindowsStyleOverride(Device.Styles.CaptionStyle, typeof(Label), FixedStyleKeys.FixedCaptionStyleKey,
                wpCaptionFontSizeSetter, wpCaptionFontForegroundSetter);

            //ListItemTextStyle
            Setter wpListItemFontSizeSetter = new Setter
            {
                Property = Label.FontSizeProperty,
                Value = 29.86
            };
            Setter wpListItemTextLineBreakSetter = new Setter
            {
                Property = Label.LineBreakModeProperty,
                Value = LineBreakMode.NoWrap
            };
            AddWindowsStyleOverride(Device.Styles.ListItemTextStyle, typeof(Label), FixedStyleKeys.FixedListItemTextKey,
                wpListItemFontSizeSetter, wpListItemTextLineBreakSetter);

            //ListItemDetailTextStyle
            Setter wpListItemDetailTextFontSizeSetter = new Setter
            {
                Property = Label.FontSizeProperty,
                Value = 16
            };
            Setter wpListItemDetailTextLineBreakSetter = new Setter
            {
                Property = Label.LineBreakModeProperty,
                Value = LineBreakMode.NoWrap
            };
            AddWindowsStyleOverride(Device.Styles.ListItemDetailTextStyle, typeof(Label),
                FixedStyleKeys.FixedListItemDetailTextKey,
                wpListItemDetailTextFontSizeSetter, wpListItemDetailTextLineBreakSetter);
        }

        //Null newStyleKey means that we'll create an implicit style
        private void AddWindowsStyleOverride(Style basedOnStyle, Type overrideType, string newStyleKey, params Setter[] setters)
        {
            Style newStyle;
            bool exists = false;
            if (newStyleKey != null && Current.Resources.ContainsKey(newStyleKey))
            {
                exists = true;
                newStyle = (Style)Current.Resources[newStyleKey];
            }
            else
            {
                newStyle = new Style(overrideType);
            }

            newStyle.BasedOn = basedOnStyle;

            //Just leave it an empty style BasedOn its parent on other platforms
            if (Device.OS == TargetPlatform.Windows && Device.Idiom == TargetIdiom.Phone)
            {
                foreach (var setter in setters)
                {
                    newStyle.Setters.Add(setter);
                }
            }

            if (newStyleKey == null)
            {
                Current.Resources.Add(newStyle);
            }
            else
            {
                if (exists)
                {
                    Current.Resources[newStyleKey] = newStyle;
                }
                else
                {
                    Current.Resources.Add(newStyleKey, newStyle);
                }
            }
        }

        private void MergeFixedStyleDescendants()
        {
            // The dictionary containing these styles _has_ to initilaized after we define our
            // "fixed" styles, because everything in that dict is BasedOn the "fixed" styles.
            FixedStyleDescendants fixedStyleDescendantsDict = new FixedStyleDescendants();
            fixedStyleDescendantsDict.MergeWithMainDictionary();
            fixedStyleDescendantsDict = null;
        }
    }
}

