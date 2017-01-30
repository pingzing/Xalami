using FutuFormTemplate.MSBUILD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FutuFormsTemplate.MSBUILD
{
    public class IosVsTemplateTask : FutuFormsTemplateTask
    {
        public override bool Run(string csprojPath, string targetDir, string projectFriendlyName, string projectDescription, string previewImagePath)
        {
            CsprojFile = csprojPath;
            ProjectFriendlyName = projectFriendlyName;
            ProjectDescription = projectDescription;
            PreviewImagePath = previewImagePath;

            tempFolder = Path.Combine(targetDir, Constants.TEMPFOLDER, "iOS");
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }

            string projectFolder = Path.GetDirectoryName(CsprojFile);
            CopyProjectFilesToTempFolder(projectFolder, tempFolder);

            ReplaceNamespace(tempFolder);
            ProcessVSTemplate(tempFolder);
            OperateOnCsProj(tempFolder, CsprojFile);
            OperateOnPlist(Path.Combine(tempFolder, "Info.plist"));
            CopyEmbeddedFilesToOutput(tempFolder);
            
            return true;
        }

        protected override void ProcessVSTemplate(string tempFolder)
        {
            string xml = FileHelper.ReadFile(CsprojFile);
            string projectName = Path.GetFileName(CsprojFile);
            string projXml = GetProjectNode(xml, projectName);
            xml = Constants.IOSVSTEMPLATETEXT.Replace(Constants.PROJECTNODE, projXml);
            xml = xml.Replace(Constants.TEMPLATENAME, ProjectFriendlyName);
            xml = xml.Replace(Constants.TEMPLATEDESCRIPTION, ProjectDescription);
            string previewFileName = Path.GetFileName(PreviewImagePath);
            xml = xml.Replace(Constants.PREVIEWIMAGEFILE, previewFileName);

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
    <string>$$safeprojectname$$</string>" });

            replacements.Add(new FindReplaceItem() {Pattern = @"<key>CFBundleIdentifier</key>
    <string>com.yourcompany.(.*?)</string>",
            Replacement = @"<key>CFBundleIdentifier</key>
    <string>com.yourcompany.$$safeprojectname$$</string>"
            });

            foreach (var item in replacements)
            {
                plistText = Regex.Replace(plistText, item.Pattern, item.Replacement);
            }

            FileHelper.WriteFile(plistPath, plistText);
        }
    }
}
