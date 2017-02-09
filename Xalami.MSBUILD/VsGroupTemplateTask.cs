using FutuFormTemplate.MSBUILD;
using Microsoft.Build.Framework;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace Xalami.MSBUILD
{
    /// <summary>
    /// A custom MSBuild Task that generates a Project Template ZIP file
    /// and copies to the specified location. This is used to covert the existing
    /// Xalami projects into project templates for deployment via the
    /// the VSIX.
    /// </summary>
    public class VsGroupTemplateTask : Microsoft.Build.Utilities.Task
    {

        #region ---- public properties  -------
        /// <summary>
        /// Gets or sets the UWP csproj file.
        /// </summary>
        /// <value>
        /// The csproj file.
        /// </value>
        [Required]
        public string UwpCsprojFile { get; set; }

        /// <summary>
        /// Gets or sets the PCL csproj file.
        /// </summary>
        /// <value>
        /// The csproj file.
        /// </value>
        [Required]
        public string PclCsprojFile { get; set; }

        /// <summary>
        /// Gets or sets the Android csproj file.
        /// </summary>
        /// <value>
        /// The csproj file.
        /// </value>
        [Required]
        public string AndroidCsprojFile { get; set; }

        /// <summary>
        /// Gets or sets the iOS csproj file.
        /// </summary>
        /// <value>
        /// The csproj file.
        /// </value>        
        [Required]
        public string iOSCsprojFile { get; set; }

        /// <summary>
        /// Gets or sets the WP8 csproj file.
        /// </summary>
        /// <value>
        /// The csproj file.
        /// </value>
        [Required]
        public string Wp8CsprojFile { get; set; }

        /// <summary>
        /// Gets or sets the name of the zip.
        /// </summary>
        /// <value>
        /// The name of the zip.
        /// </value>
        [Required]
        public string ZipName { get; set; }        

        /// <summary>
        /// Gets or sets the preview image path.
        /// </summary>
        /// <value>
        /// The preview image path.
        /// </value>
        [Required]
        public string PreviewImagePath { get; set; }

        /// <summary>
        /// Gets or sets the path to the icon images.
        /// </summary>
        /// <value>
        /// The path string that leads to the .ico or .png 32x32 image to be used for the icon.
        /// </value>
        [Required]
        public string IconPath { get; set; }


        /// <summary>
        /// Gets or sets the name of the project friendly.
        /// </summary>
        /// <value>
        /// The name of the project friendly.
        /// </value>
        [Required]
        public string ProjectFriendlyName { get; set; }

        public string TargetDir2 { get; set; }

        /// <summary>
        /// Gets or sets the target dir.
        /// </summary>
        /// <value>
        /// The target dir.
        /// </value>
        public string TargetDir { get; set; }

        #endregion

        public override bool Execute()
        {
            string tempFolder = Path.Combine(TargetDir, Constants.TEMPFOLDER);
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }            

            bool uwpSuccess = new UwpVsTemplateTask().Run(UwpCsprojFile, TargetDir, ProjectFriendlyName, PreviewImagePath);
            this.Log.LogMessage("UWP VSTemplate processed. Success: " + uwpSuccess);
            
            bool wp8Success = new Wp8VsTemplateTask().Run(Wp8CsprojFile, TargetDir, ProjectFriendlyName, PreviewImagePath);
            this.Log.LogMessage("WP8 VSTemplate processed. Success: " + wp8Success);

            bool androidSuccess = new AndroidVsTemplateTask().Run(AndroidCsprojFile, TargetDir, ProjectFriendlyName, PreviewImagePath);
            this.Log.LogMessage("Android VSTemplate processed. Success: " + androidSuccess);

            bool iosSuccess = new IosVsTemplateTask().Run(iOSCsprojFile, TargetDir, ProjectFriendlyName, PreviewImagePath);
            this.Log.LogMessage("Android VSTemplate processed. Success: " + iosSuccess);

            bool pclSuccess = new PclVsTemplateTask().Run(PclCsprojFile, TargetDir, ProjectFriendlyName, PreviewImagePath);
            this.Log.LogMessage("PCL VSTemplate process. Success: " + pclSuccess);

            ProcessVSTemplate(tempFolder);
            CopyEmbeddedFilesToOutput(tempFolder);
            ZipFiles(tempFolder, ZipName, TargetDir);

            return uwpSuccess && wp8Success && iosSuccess && androidSuccess && pclSuccess;
        }

        private void ProcessVSTemplate(string tempFolder)
        {
            string xml = Constants.GROUPTEMPLATETEXT;
            xml = xml.Replace(Constants.PREVIEWIMAGEKEY, Path.GetFileName(PreviewImagePath));
            xml = xml.Replace(Constants.ICONKEY, Path.GetFileName(IconPath));
            string filePath = Path.Combine(tempFolder, Constants.GROUPTEMPLATENAME);            
            FileHelper.WriteFile(filePath, xml);            
        }

        /// <summary>
        /// Copies the embedded files to output.
        /// </summary>
        /// <param name="targetDir">The target dir.</param>
        protected void CopyEmbeddedFilesToOutput(string targetDir)
        {
            string[] names = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            foreach (var item in names)
            {
                using (var s = Assembly.GetExecutingAssembly().GetManifestResourceStream(item))
                {
                    var targetFile = Path.Combine(targetDir, Path.GetFileName(item.Substring(item.LastIndexOf("EmbeddedFiles.") + 14)));

                    using (var fileStream = File.Create(targetFile))
                    {
                        s.Seek(0, SeekOrigin.Begin);
                        s.CopyTo(fileStream);
                    }
                }
            }

        }

        /// <summary>
        /// Zips the files.
        /// </summary>
        /// <param name="tempFolder">The temporary folder.</param>
        /// <param name="zipName">Name of the zip.</param>
        /// <param name="targetDir">The target dir.</param>
        private void ZipFiles(string tempFolder, string zipName, string targetDir)
        {
            string zipFileName = Path.Combine(targetDir, ZipName);

            if (File.Exists(zipFileName))
            {
                File.Delete(zipFileName);
            }
            ZipFile.CreateFromDirectory(tempFolder, zipFileName);

            //-- now second one...
            if (!string.IsNullOrWhiteSpace(TargetDir2))
            {
                string zipFileName2 = Path.Combine(TargetDir2, ZipName);
                File.Copy(zipFileName, zipFileName2, true);
            }

            //-- clean up the temporary folder
            Directory.Delete(tempFolder, true);
        }
    }
}
