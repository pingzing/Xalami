using FutuFormTemplate.MSBUILD;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System;

namespace Xalami.MSBUILD
{
    public class AndroidVsTemplateTask : XalamiTask
    {
        public override bool Run(string csprojPath, string targetDir, string projectFriendlyName, string previewImagePath)
        {
            CsprojFile = csprojPath;
            ProjectFriendlyName = projectFriendlyName;            
            PreviewImagePath = previewImagePath;

            tempFolder = Path.Combine(targetDir, Constants.TEMPFOLDER, Constants.ANDROIDPLATFORMSUFFIX);
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }

            string projectFolder = Path.GetDirectoryName(CsprojFile);
            CopyProjectFilesToTempFolder(projectFolder, tempFolder);

            ReplaceNamespace(tempFolder);
            ProcessVSTemplate(tempFolder);
            OperateOnCsProj(tempFolder, CsprojFile, Constants.ANDROIDPLATFORMSUFFIX);
            OperateOnManifest(Path.Combine(tempFolder, "Properties/AndroidManifest.xml"));            
            
            return true;
        }

        protected override void ProcessVSTemplate(string tempFolder)
        {
            string xml = FileHelper.ReadFile(CsprojFile);
            string projectName = Path.GetFileName(CsprojFile);
            string projXml = GetProjectNode(xml, projectName);
            xml = Constants.ANDROIDVSTEMPLATETEXT.Replace(Constants.PROJECTNODE, projXml);
            xml = xml.Replace(Constants.TEMPLATENAME, ProjectFriendlyName);                        

            string filePath = Path.Combine(tempFolder, Constants.ANDROIDVSTEMPLATENAME);

            FileHelper.WriteFile(filePath, xml);
        }

        private void OperateOnManifest(string manifestPath)
        {
            string manifestText = FileHelper.ReadFile(manifestPath);

            var replacements = new List<FindReplaceItem>();

            replacements.Add(new FindReplaceItem() { Pattern = @"<application android:label=""(.*?).Droid""></application>", Replacement = @"<application android:label=""$$ext_safeprojectname$$.Droid""></application>" });

            foreach (var item in replacements)
            {
                manifestText = Regex.Replace(manifestText, item.Pattern, item.Replacement);
            }

            FileHelper.WriteFile(manifestPath, manifestText);
        }
    }
}
