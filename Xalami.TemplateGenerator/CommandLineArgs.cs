using CommandLine;
using CommandLine.Text;

namespace Xalami.TemplateGenerator
{    
    public class CommandLineArgs
    {
        private const string VsSetName = "vs";

        [Option('t', "target", DefaultValue = "vs",
            HelpText = "Specify the target addin/extension you'd like to build. 'vs' for Visual Studio .vsix, or 'xamarin' for a Xamarin Studio Addin.",
            Required = true)]
        public string Target { get; set; }

        [Option('p', "pclCsproj", HelpText = "The path to the the Xalami PCL's .csproj file.", Required = true)]
        public string PclCsprojPath { get; set; }
        
        [Option('a', "androidCsproj", HelpText = "The path to the Xalami Android project's .csproj file.", Required = true)]
        public string AndroidCsproj { get; set; }

        [Option('i', "iosCsproj", HelpText = "The path to the Xalami iOS project's .csproj file.", Required = true)]
        public string IosCsproj { get; set; }

        [Option('r', "previewImagePath", HelpText = "The path to the preview image used for the extension/addin. Should be a .png file.", Required = true)]
        public string PreviewImagePath { get; set; }

        [Option('c', "iconImagePath", HelpText = "The path to the icon used for the extension/addin. Should be a .png file.", Required = true)]
        public string IconImagePath { get; set; }

        [Option('f', "projectFriendlyName", HelpText = "The 'friendly' project name that will be used every user-facing.")]
        public string ProjectFriendlyName { get; set; }

        [Option('d', "targetDir", HelpText = "Where the generated template files should go. For the Xamarin Studio addin, this should be the Templates folder under the ../Xalami.XamarinStudioAddin/ folder. For the Visual Studio Extension, this should be in folder underneath ../Xalami.VsixInstaller/ProjectTemplates.")]
        public string TargetDir { get; set; }

        [Option('u', "uwpCsproj", HelpText = "The path to the Xalami UWP project's .csproj file.", MutuallyExclusiveSet = VsSetName, Required = false)]
        public string UwpCsproj { get; set; }

        [Option('w', "wp8Csproj", HelpText = "The path to the Xalami WP8 project's .csproj file.", MutuallyExclusiveSet = VsSetName, Required = false)]
        public string Wp8Csproj { get; set; }

        [Option('z' ,"zipName", HelpText = "The name of the output .zip file that will contain all of the project template files.", MutuallyExclusiveSet = VsSetName, Required = false)]
        public string ZipName { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
