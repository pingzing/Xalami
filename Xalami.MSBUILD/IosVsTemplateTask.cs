using FutuFormTemplate.MSBUILD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Xalami.MSBUILD
{
    public class IosVsTemplateTask : XalamiTask
    {
        public override bool Run(string csprojPath, string targetDir, string projectFriendlyName, string previewImagePath)
        {
            CsprojFile = csprojPath;
            ProjectFriendlyName = projectFriendlyName;            
            PreviewImagePath = previewImagePath;

            tempFolder = Path.Combine(targetDir, Constants.TEMPFOLDER, Constants.IOSPLATFORMSUFFIX);
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }

            string projectFolder = Path.GetDirectoryName(CsprojFile);
            CopyProjectFilesToTempFolder(projectFolder, tempFolder);

            ReplaceNamespace(tempFolder);
            ProcessVSTemplate(tempFolder);
            OperateOnCsProj(tempFolder, CsprojFile, Constants.IOSPLATFORMSUFFIX);
            OperateOnPlist(Path.Combine(tempFolder, "Info.plist"));           
            
            return true;
        }

        protected override void ProcessVSTemplate(string tempFolder)
        {
            string xml = FileHelper.ReadFile(CsprojFile);
            string projectName = Path.GetFileName(CsprojFile);
            string projXml = GetProjectNode(xml, projectName);
            xml = Constants.IOSVSTEMPLATETEXT.Replace(Constants.PROJECTNODE, projXml);
            xml = xml.Replace(Constants.TEMPLATENAME, ProjectFriendlyName);                        

            string filePath = Path.Combine(tempFolder, Constants.IOSVSTEMPLATENAME);

            FileHelper.WriteFile(filePath, xml);
        }

        private void OperateOnPlist(string plistPath)
        {
            string plistText = FileHelper.ReadFile(plistPath);

            var replacements = new List<FindReplaceItem>();

            replacements.Add(new FindReplaceItem() { Pattern = @"<key>CFBundleDisplayName</key>
    <string>(.*?)</string>",
                Replacement = @"<key>CFBundleDisplayName</key>
    <string>$$ext_safeprojectname$$</string>" });

            replacements.Add(new FindReplaceItem() {Pattern = @"<key>CFBundleIdentifier</key>
    <string>com.yourcompany.(.*?)</string>",
            Replacement = @"<key>CFBundleIdentifier</key>
    <string>com.yourcompany.$$ext_safeprojectname$$</string>"
            });

            foreach (var item in replacements)
            {
                plistText = Regex.Replace(plistText, item.Pattern, item.Replacement);
            }

            FileHelper.WriteFile(plistPath, plistText);
        }
    }
}
