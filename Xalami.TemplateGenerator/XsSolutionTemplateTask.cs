using System;
using System.IO;

namespace Xalami.TemplateGenerator
{
    public class XsSolutionTemplateTask : Microsoft.Build.Utilities.Task
    { 
        /// <summary>
        /// Gets or sets the NetStandard csproj file.
        /// </summary>
        /// <value>
        /// The csproj file.
        /// </value>        
        public string NetStandardCsprojFile { get; }

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

        public override bool Execute()
        {
            bool allSet = VerifyAllPropertiesSet();
            if (!allSet)
            {
                Log.LogError("Missing one or more essential properties. Aborting.");
                return false;
            }

            string tempFolder = Path.Combine(TargetDir, Constants.TEMPFOLDER);

            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }

            string netStandardFilesNode = XalamiXsTaskHelper.PrepareProjectFolder(Path.Combine(TargetDir, Constants.TEMPFOLDER, "Xalami"), NetStandardCsprojFile, "Xalami");
            string androidFilesNode = XalamiXsTaskHelper.PrepareProjectFolder(Path.Combine(TargetDir, Constants.TEMPFOLDER, Constants.ANDROIDPLATFORMSUFFIX), AndroidCsprojFile, Constants.ANDROIDPLATFORMSUFFIX);
            string iosFilesNode = XalamiXsTaskHelper.PrepareProjectFolder(Path.Combine(TargetDir, Constants.TEMPFOLDER, Constants.IOSPLATFORMSUFFIX), iOSCsprojFile, Constants.IOSPLATFORMSUFFIX);

            string solutionXptXml = Constants.XPTXMLTEXT.Replace("$netstandardFilesNode", netStandardFilesNode);
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
                Log.LogMessage($"Unable to delete temp folder at {tempFolder}. Error: " + ex.Message);
            }

            return true;
        }        

        private bool VerifyAllPropertiesSet()
        {
            bool allSet = true;
            allSet = VerifyPropertySet(NetStandardCsprojFile, nameof(NetStandardCsprojFile));
            allSet = VerifyPropertySet(AndroidCsprojFile, nameof(AndroidCsprojFile));
            allSet = VerifyPropertySet(iOSCsprojFile, nameof(iOSCsprojFile));
            allSet = VerifyPropertySet(PreviewImagePath, nameof(PreviewImagePath));
            allSet = VerifyPropertySet(IconPath, nameof(IconPath));
            allSet = VerifyPropertySet(ProjectFriendlyName, nameof(ProjectFriendlyName));
            allSet = VerifyPropertySet(TargetDir, nameof(TargetDir));

            return allSet;
        }

        private bool VerifyPropertySet(string propertyValue, string propertyName)
        {
            if (String.IsNullOrWhiteSpace(propertyValue))
            {
                Log.LogError($"Property: {propertyName} was null, or whitespace.");
                return false;
            }

            return true;
        }
    }
}
