using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace Xalami.TemplateGenerator
{
    public class VsGroupTemplateTask
    {

        #region ---- public properties  -------
        /// <summary>
        /// Gets or sets the UWP csproj file.
        /// </summary>
        /// <value>
        /// The csproj file.
        /// </value>
        public string UwpCsprojFile { get; set; }

        /// <summary>
        /// Gets or sets the NetStandard csproj file.
        /// </summary>
        /// <value>
        /// The csproj file.
        /// </value>
        public string NetStandardCsprojFile { get; set; }

        /// <summary>
        /// Gets or sets the Android csproj file.
        /// </summary>
        /// <value>
        /// The csproj file.
        /// </value>
        public string AndroidCsprojFile { get; set; }

        /// <summary>
        /// Gets or sets the iOS csproj file.
        /// </summary>
        /// <value>
        /// The csproj file.
        /// </value>        
        public string iOSCsprojFile { get; set; }        

        /// <summary>
        /// Gets or sets the name of the zip.
        /// </summary>
        /// <value>
        /// The name of the zip.
        /// </value>
        public string ZipName { get; set; }        

        /// <summary>
        /// Gets or sets the preview image path.
        /// </summary>
        /// <value>
        /// The preview image path.
        /// </value>
        public string PreviewImagePath { get; set; }

        /// <summary>
        /// Gets or sets the path to the icon images.
        /// </summary>
        /// <value>
        /// The path string that leads to the .ico or .png 32x32 image to be used for the icon.
        /// </value>
        public string IconPath { get; set; }


        /// <summary>
        /// Gets or sets the name of the project friendly.
        /// </summary>
        /// <value>
        /// The name of the project friendly.
        /// </value>
        public string ProjectFriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the target dir.
        /// </summary>
        /// <value>
        /// The target dir.
        /// </value>
        public string TargetDir { get; set; }

        #endregion

        public VsGroupTemplateTask(string uwpCsprojFile, string pclCsprojFile, string androidCsprojFile, string iosCsprojFile,
            string zipName, string previewImagePath, string iconPath, string projectFriendlyName,
            string targetDir)
        {
            UwpCsprojFile = uwpCsprojFile;
            NetStandardCsprojFile = pclCsprojFile;
            AndroidCsprojFile = androidCsprojFile;
            iOSCsprojFile = iosCsprojFile;            
            ZipName = zipName;
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

            bool uwpSuccess = new UwpVsTemplateTask().Run(UwpCsprojFile, TargetDir, ProjectFriendlyName, PreviewImagePath);
            Console.WriteLine("UWP VSTemplate processed. Success: " + uwpSuccess);                       

            bool androidSuccess = new AndroidVsTemplateTask().Run(AndroidCsprojFile, TargetDir, ProjectFriendlyName, PreviewImagePath);
            Console.WriteLine("Android VSTemplate processed. Success: " + androidSuccess);

            bool iosSuccess = new IosVsTemplateTask().Run(iOSCsprojFile, TargetDir, ProjectFriendlyName, PreviewImagePath);
            Console.WriteLine("Android VSTemplate processed. Success: " + iosSuccess);

            bool netstandardSuccess = new NetStandardVsTemplateTask().Run(NetStandardCsprojFile, TargetDir, ProjectFriendlyName, PreviewImagePath);
            Console.WriteLine("NetStandard VSTemplate process. Success: " + netstandardSuccess);

            ProcessVSTemplate(tempFolder);
            CopyEmbeddedFilesToOutput(tempFolder);
            ZipFiles(tempFolder, ZipName, TargetDir);

            return uwpSuccess && iosSuccess && androidSuccess && netstandardSuccess;
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

            //-- clean up the temporary folder
            Directory.Delete(tempFolder, true);
        }
    }
}
