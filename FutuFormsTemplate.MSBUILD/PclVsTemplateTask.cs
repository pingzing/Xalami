using FutuFormTemplate.MSBUILD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutuFormsTemplate.MSBUILD
{
    public class PclVsTemplateTask : FutuFormsTemplateTask
    {
        public override bool Run(string csprojPath, string targetDir, string projectFriendlyName, string projectDescription, string previewImagePath)
        {
            CsprojFile = csprojPath;
            ProjectFriendlyName = projectFriendlyName;
            ProjectDescription = projectDescription;
            PreviewImagePath = previewImagePath;

            tempFolder = Path.Combine(targetDir, Constants.TEMPFOLDER, "PCL");
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }

            string projectFolder = Path.GetDirectoryName(CsprojFile);
            CopyProjectFilesToTempFolder(projectFolder, tempFolder);

            ReplaceNamespace(tempFolder);
            FileHelper.DeleteKey(tempFolder);
            ProcessVSTemplate(tempFolder);
            OperateOnCsProj(tempFolder, CsprojFile, Constants.PCLPLATFORMSUFFIX);
            CopyEmbeddedFilesToOutput(tempFolder);

            return true;
        }

        protected override void ProcessVSTemplate(string tempFolder)
        {
            string xml = FileHelper.ReadFile(CsprojFile);
            string projectName = Path.GetFileName(CsprojFile);
            string projXml = GetProjectNode(xml, projectName);
            xml = Constants.PCLVSTEMPLATETEXT.Replace(Constants.PROJECTNODE, projXml);
            xml = xml.Replace(Constants.TEMPLATENAME, ProjectFriendlyName);
            xml = xml.Replace(Constants.TEMPLATEDESCRIPTION, ProjectDescription);
            string previewFileName = Path.GetFileName(PreviewImagePath);
            xml = xml.Replace(Constants.PREVIEWIMAGEFILE, previewFileName);

            string filePath = Path.Combine(tempFolder, Constants.PCLVSTEMPLATENAME);

            FileHelper.WriteFile(filePath, xml);
        }
    }
}
