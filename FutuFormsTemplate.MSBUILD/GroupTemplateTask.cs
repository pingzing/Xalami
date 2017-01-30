using FutuFormTemplate.MSBUILD;
using Microsoft.Build.Framework;
using System.IO;
using System.IO.Compression;

namespace FutuFormsTemplate.MSBUILD
{
    public class GroupTemplateTask : Microsoft.Build.Utilities.Task
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
        /// Gets or sets the project description.
        /// </summary>
        /// <value>
        /// The project description.
        /// </value>
        [Required]
        public string ProjectDescription { get; set; }

        /// <summary>
        /// Gets or sets the preview image path.
        /// </summary>
        /// <value>
        /// The preview image path.
        /// </value>
        [Required]
        public string PreviewImagePath { get; set; }


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

            bool uwpSuccess = new UwpVsTemplateTask().Run(UwpCsprojFile, TargetDir, ProjectFriendlyName, ProjectDescription, PreviewImagePath);
            this.Log.LogMessage("UWP VSTemplate processed. Success: " + uwpSuccess);
            
            bool wp8Success = new Wp8VsTemplateTask().Run(Wp8CsprojFile, TargetDir, ProjectFriendlyName, ProjectDescription, PreviewImagePath);
            this.Log.LogMessage("WP8 VSTemplate processed. Success: " + wp8Success);

            bool androidSuccess = new AndroidVsTemplateTask().Run(AndroidCsprojFile, TargetDir, ProjectFriendlyName, ProjectDescription, PreviewImagePath);
            this.Log.LogMessage("Android VSTemplate processed. Success: " + androidSuccess);

            bool iosSuccess = new IosVsTemplateTask().Run(iOSCsprojFile, TargetDir, ProjectFriendlyName, ProjectDescription, PreviewImagePath);
            this.Log.LogMessage("Android VSTemplate processed. Success: " + iosSuccess);

            bool pclSuccess = new PclVsTemplateTask().Run(PclCsprojFile, TargetDir, ProjectFriendlyName, ProjectDescription, PreviewImagePath);
            this.Log.LogMessage("PCL VSTemplate process. Success: " + pclSuccess);

            ProcessVSTemplate(tempFolder);
            ZipFiles(tempFolder, ZipName, TargetDir);

            return uwpSuccess && wp8Success && iosSuccess && androidSuccess && pclSuccess;
        }

        private void ProcessVSTemplate(string tempFolder)
        {                       
            string filePath = Path.Combine(tempFolder, Constants.GROUPTEMPLATENAME);
            FileHelper.WriteFile(filePath, Constants.GROUPTEMPLATETEXT);
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
