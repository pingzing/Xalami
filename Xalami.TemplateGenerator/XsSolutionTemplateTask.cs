using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xalami.TemplateGenerator
{
    public class XsSolutionTemplateTask
    { 
        /// <summary>
        /// Gets or sets the PCL csproj file.
        /// </summary>
        /// <value>
        /// The csproj file.
        /// </value>        
        public string PclCsprojFile { get; }

        /// <summary>
        /// Gets or sets the Android csproj file.
        /// </summary>
        /// <value>
        /// The csproj file.
        /// </value>        
        public string AndroidCsprojFile { get; }

        /// <summary>
        /// Gets or sets the iOS csproj file.
        /// </summary>
        /// <value>
        /// The csproj file.
        /// </value>        
  
        public string iOSCsprojFile { get; }

        /// <summary>
        /// Gets or sets the preview image path.
        /// </summary>
        /// <value>
        /// The preview image path.
        /// </value>      
        public string PreviewImagePath { get; }

        /// <summary>
        /// Gets or sets the path to the icon images.
        /// </summary>
        /// <value>
        /// The path string that leads to the .ico or .png 32x32 image to be used for the icon.
        /// </value>
        public string IconPath { get; }


        /// <summary>
        /// Gets or sets the name of the project friendly.
        /// </summary>
        /// <value>
        /// The name of the project friendly.
        /// </value>
        public string ProjectFriendlyName { get; }        

        /// <summary>
        /// Gets or sets the target dir.
        /// </summary>
        /// <value>
        /// The target dir.
        /// </value>
        public string TargetDir { get; }

        public XsSolutionTemplateTask(string pclCsprojFile, string androidCsProjFile, string iosCsprojFile, string previewImagePath,
            string iconPath, string projectFriendlyName, string targetDir)
        {
            PclCsprojFile = pclCsprojFile;
            AndroidCsprojFile = androidCsProjFile;
            iOSCsprojFile = iosCsprojFile;
            PreviewImagePath = previewImagePath;
            IconPath = iconPath;
            ProjectFriendlyName = projectFriendlyName;
            TargetDir = targetDir;
        }

        public bool Execute()
        {            
            string tempFolder = Path.Combine(TargetDir, Constants.TEMPFOLDER);

            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }

            string pclFilesNode = XalamiXsTaskHelper.PrepareProjectFolder(Path.Combine(TargetDir, Constants.TEMPFOLDER, "Xalami"), PclCsprojFile, "Xalami");
            string androidFilesNode = XalamiXsTaskHelper.PrepareProjectFolder(Path.Combine(TargetDir, Constants.TEMPFOLDER, Constants.ANDROIDPLATFORMSUFFIX), AndroidCsprojFile, Constants.ANDROIDPLATFORMSUFFIX);
            string iosFilesNode = XalamiXsTaskHelper.PrepareProjectFolder(Path.Combine(TargetDir, Constants.TEMPFOLDER, Constants.IOSPLATFORMSUFFIX), iOSCsprojFile, Constants.IOSPLATFORMSUFFIX);

            string solutionXptXml = Constants.XPTXMLTEXT.Replace("$pclFilesNode", pclFilesNode);
            solutionXptXml = solutionXptXml.Replace("$androidFilesNode", androidFilesNode);
            solutionXptXml = solutionXptXml.Replace("$iosFilesNode", iosFilesNode);
            solutionXptXml = solutionXptXml.Replace(Constants.PREVIEWIMAGEKEY, Path.GetFileName(PreviewImagePath));
            solutionXptXml = solutionXptXml.Replace(Constants.ICONKEY, Path.GetFileName(IconPath));            

            string xptXmlFileName = "Xalami.Solution.xpt.xml";
            string xptXmlPath = Path.Combine(tempFolder, xptXmlFileName);
            FileHelper.WriteFile(xptXmlPath, solutionXptXml);

            FileHelper.DeleteCsproj(tempFolder);

            FileHelper.DirectoryCopy(tempFolder, TargetDir, true, true);
            try
            {
                Directory.Delete(tempFolder, true);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Unable to delete temp folder at {tempFolder}. Error: " + ex.Message);
            }

            return true;
        }
    }
}
