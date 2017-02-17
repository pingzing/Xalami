using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xalami.TemplateGenerator
{
    public static class EntryPoint
    {
        private const string VisualStudioTarget = "vs";
        private const string XamarinStudioTarget = "xamarin";
        private enum TargetPlatform { VisualStudio, XamarinStudio };

        public static void Main(string[] args)
        {            
            if (args.Last().EndsWith("\"")) //An extra quotation mark
            {
                string str = args.Last();
                str = str.Substring(0, str.Length - 1);
                args[args.Length - 1] = str;
            }            
            CommandLineArgs opts = new CommandLineArgs();
            if (CommandLine.Parser.Default.ParseArguments(args, opts))
            {
                if (opts.Target == VisualStudioTarget)
                {
                    ValidateArgs(opts, TargetPlatform.VisualStudio);
                    var visualStudioTemplateTask = new VsGroupTemplateTask(opts.UwpCsproj, opts.PclCsprojPath, opts.AndroidCsproj,
                        opts.IosCsproj, opts.Wp8Csproj, opts.ZipName, opts.PreviewImagePath, opts.IconImagePath, opts.ProjectFriendlyName,
                        opts.TargetDir);

                    visualStudioTemplateTask.Execute();
                }
                else if (opts.Target == XamarinStudioTarget)
                {
                    ValidateArgs(opts, TargetPlatform.XamarinStudio);
                    var xamarinTemplateTask = new XsSolutionTemplateTask(opts.PclCsprojPath, opts.AndroidCsproj, opts.IosCsproj,
                        opts.PreviewImagePath, opts.IconImagePath, opts.ProjectFriendlyName, opts.TargetDir);

                    xamarinTemplateTask.Execute(); 
                }                
            }            
        }

        private static void ValidateArgs(CommandLineArgs opts, TargetPlatform target)
        {
            if (!ValidateInputFilePaths(
                    opts.PclCsprojPath,
                    opts.AndroidCsproj,
                    opts.IosCsproj, 
                    opts.PreviewImagePath,
                    opts.IconImagePath
                ))
            {
                return;
            }
          
            if (!Directory.Exists(Path.GetFullPath(opts.TargetDir)))
            {
                PrintFileFailureAndSetExitCode(opts.TargetDir);
                return;
            }

            if (target == TargetPlatform.VisualStudio 
                && !ValidateInputFilePaths(opts.UwpCsproj, opts.Wp8Csproj))
            {
                return;
            }
        }

        private static bool ValidateInputFilePaths(params string[] paths)
        {
            bool success = false;
            foreach (string path in paths)
            {
                if (!File.Exists(Path.GetFullPath(path)))
                {
                    PrintFileFailureAndSetExitCode(path);
                    return false;
                }
            }
            success = true;
            return success;
        }        
        
        private static void PrintFileFailureAndSetExitCode(string failedFile)
        {
            Console.WriteLine("Parsing arguments failed. Unable to find the file or folder: " + failedFile);
            Environment.ExitCode = 1;
        }        
    }
}
