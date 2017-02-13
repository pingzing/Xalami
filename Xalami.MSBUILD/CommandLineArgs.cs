using CommandLine;
using CommandLine.Text;

namespace Xalami.MSBUILD
{
    public class CommandLineArgs
    {
        [Option('t', "target", DefaultValue = "vs",
            HelpText = "Specify the target addin/extension you'd like to build. 'vs' for Visual Studio .vsix, or 'xamarin' for a Xamarin Studio Addin.",
            Required = true)]
        public string Target { get; set; }

        [Option("pclCsproj", HelpText = "The path to the the Xalami PCL's .csproj file.", Required = true)]
        public string PclCsprojPath { get; set; }
        
        [Option("androidCsproj", HelpText = "The path to the Xalami Android project's .csproj file.", Required = true)]
        public string AndroidCsproj { get; set; }

        [Option("iosCsproj", HelpText = "The path to the Xalami iOS project's .csproj file.", Required = true)]
        public string IosCsproj { get; set; }

        [Option("previewImagePath", HelpText = "The path to the preview image used for the extension/addin. Should be a .png file.", Required = true)]
        public string PreviewImagePath { get; set; }

        [Option("iconImagePath", HelpText = "The path to the icon used for the extension/addin. Should be a .png file.", Required = true)]
        public string IconImagePath { get; set; }

        [Option("projectFriendlyName", HelpText = "The 'friendly' project name that will be used every user-facing.")]
        public string ProjectFriendlyName { get; set; }

        [Option("TargetDir", HelpText = "Where the generated template files should go. For the Xamarin Studio addin, this should be the Templates folder under the ../Xalami.XamarinStudioAddin/ folder. For the Visual Studio Extension, this should be in folder underneath ../Xalami.VsixInstaller/ProjectTemplates.")]
        public string TargetDir { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
