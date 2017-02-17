using System.IO;

namespace Xalami.TemplateGenerator
{
    public class PclVsTemplateTask : XalamiTaskBase
    {
        public override bool Run(string csprojPath, string targetDir, string projectFriendlyName, string previewImagePath)
        {
            CsprojFile = csprojPath;
            ProjectFriendlyName = projectFriendlyName;            
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

            return true;
        }

        protected override void ProcessVSTemplate(string tempFolder)
        {
            string xml = FileHelper.ReadFile(CsprojFile);
            string projectName = Path.GetFileName(CsprojFile);
            string projXml = GetProjectNode(xml, projectName);
            xml = Constants.PCLVSTEMPLATETEXT.Replace(Constants.PROJECTNODE, projXml);            

            string filePath = Path.Combine(tempFolder, Constants.PCLVSTEMPLATENAME);

            FileHelper.WriteFile(filePath, xml);
        }
    }
}
