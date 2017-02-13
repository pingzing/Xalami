using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ${ProjectName}.XamlExtensions
{
    /// <summary>
    /// A XAML markup extension for easily resolving image paths in a cross-platform way.
    /// The provided path should just be the image's path relative to the default location
    /// (Resources on iOS, Resources on Android, [app root]/Assets/ on Windows).
    /// </summary>
    [ContentProperty("Path")]
    public class ImageExtension : IMarkupExtension
    {
        /// <summary>
        /// The image's file name, provided that it can be found in the standard location on the platform
        /// (Resources on iOS, Resources on Android, [app root]/Assets/ on Windows).
        /// </summary>
        public string Path { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (String.IsNullOrWhiteSpace(Path))
            {
                return "";
            }

            if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            {
                return System.IO.Path.Combine("Assets/", Path);
            }
            else
            {
                return Path;
            }
            
        }
    }
}

