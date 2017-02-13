using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xalami.MSBUILD
{
    public static class EntryPoint
    {
        private const string VisualStudioTarget = "vs";
        private const string XamarinStudioTarget = "xamarin";

        public static void Main(string[] args)
        {
            CommandLineArgs opts = new CommandLineArgs();
            if (CommandLine.Parser.Default.ParseArguments(args, opts))
            {
                if (opts.Target == VisualStudioTarget)
                {
                    
                }
                else if (opts.Target == XamarinStudioTarget)
                {
                    if (!File.Exists(Path.GetFullPath(opts.PclCsprojPath)))
                    {
                        PrintFileFailureAndSetExitCode(opts.PclCsprojPath);                        
                        return;
                    }
                    if (!File.Exists(Path.GetFullPath(opts.AndroidCsproj)))
                    {
                        PrintFileFailureAndSetExitCode(opts.AndroidCsproj);                        
                        return;
                    }
                    if (!File.Exists(Path.GetFullPath(opts.IosCsproj)))
                    {
                        PrintFileFailureAndSetExitCode(opts.IosCsproj);
                        return;
                    }
                    if (!File.Exists(Path.GetFullPath(opts.PreviewImagePath)))
                    {
                        PrintFileFailureAndSetExitCode(opts.PreviewImagePath);
                        return;
                    }
                    if (!File.Exists(Path.GetFullPath(opts.IconImagePath)))
                    {
                        PrintFileFailureAndSetExitCode(opts.IconImagePath);
                        return;
                    }
                    if (!Directory.Exists(Path.GetFullPath(opts.TargetDir)))
                    {
                        PrintFileFailureAndSetExitCode(opts.TargetDir);
                        return;
                    }

                    var xamarinTemplateTask = new XsSolutionTemplateTask(opts.PclCsprojPath, opts.AndroidCsproj, opts.IosCsproj,
                        opts.PreviewImagePath, opts.IconImagePath, opts.ProjectFriendlyName, opts.TargetDir);
                    xamarinTemplateTask.Execute(); //!!!!
                }
                else
                {
                    PrintFailure(opts);
                }
            }
            else
            {
                PrintFailure(opts);
            }
        }

        public static void PrintFailure(CommandLineArgs opts)
        {
            Console.WriteLine("Parsing arguments failed.\nUsage: " + opts.GetUsage());
        }    
        
        public static void PrintFileFailureAndSetExitCode(string failedFile)
        {
            Console.WriteLine("Parsing arguments failed. Unable to find the file or folder: " + failedFile);
            Environment.ExitCode = 1;
        }
    }
}
