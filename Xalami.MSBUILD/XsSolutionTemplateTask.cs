using FutuFormTemplate.MSBUILD;
using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xalami.MSBUILD
{
    public class XsSolutionTemplateTask : Microsoft.Build.Utilities.Task
    {
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

        /// <summary>
        /// Gets or sets the target dir.
        /// </summary>
        /// <value>
        /// The target dir.
        /// </value>
        public string TargetDir { get; set; }

        public override bool Execute()
        {
            string tempFolder = Path.Combine(TargetDir, Constants.TEMPFOLDER);

            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }
        }
    }
}
