using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using FutuFormTemplate.MSBUILD;

namespace FutuFormsTemplate.MSBUILD
{
    /// <summary>
    /// A custom MSBuild Task that generates a Project Template ZIP file
    /// and copies to the specified location. This is used to covert the existing
    /// Template 10 projects into project templates for deployment via the
    /// the VSIX.
    /// </summary>
    public class UwpVsTemplateTask : FutuFormsTemplateTask
    {     
        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns></returns>
        public override bool Run(string csprojPath, string targetDir, string projectFriendlyName, string projectDescription, string previewImagePath)
        {
            CsprojFile = csprojPath;
            ProjectFriendlyName = projectFriendlyName;
            ProjectDescription = projectDescription;
            PreviewImagePath = previewImagePath;

            tempFolder = Path.Combine(targetDir, Constants.TEMPFOLDER, "UWP");
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }

            string projectFolder = Path.GetDirectoryName(CsprojFile);
            CopyProjectFilesToTempFolder(projectFolder, tempFolder);

            ReplaceNamespace(tempFolder);
            FileHelper.DeleteKey(tempFolder);
            FileHelper.DeletePackagesConfig(tempFolder);
            FileHelper.DeleteProjectDotJson(tempFolder);
            ProcessVSTemplate(tempFolder);
            OperateOnCsProj(tempFolder, CsprojFile, true);
            OperateOnManifest(Path.Combine(tempFolder, "Package.appxmanifest"));
            CopyEmbeddedFilesToOutput(tempFolder);
            
            return true;
        }       

        /// <summary>
        /// Operates the on manifest.
        /// </summary>
        /// <param name="manifestFile">The manifest file.</param>
        private void OperateOnManifest(string manifestFile)
        {
            string manifestText = FileHelper.ReadFile(manifestFile);

            var replacements = new List<FindReplaceItem>();

            replacements.Add(new FindReplaceItem() { Pattern = "<mp:PhoneIdentity(.*?)/>", Replacement = @"<mp:PhoneIdentity PhoneProductId=""$$guid9$$"" PhonePublisherId=""00000000-0000-0000-0000-000000000000""/>" });
            replacements.Add(new FindReplaceItem() { Pattern = "<DisplayName>(.*?)</DisplayName>", Replacement = @"<DisplayName>$$projectname$$</DisplayName>" });
            replacements.Add(new FindReplaceItem() { Pattern = "<PublisherDisplayName>(.*?)</PublisherDisplayName>", Replacement = @"<PublisherDisplayName>$$XmlEscapedPublisher$$</PublisherDisplayName>" });
            replacements.Add(new FindReplaceItem() { Pattern = @"Executable=""(.*?)""", Replacement = @"Executable=""$$targetnametoken$$.exe""" });
            replacements.Add(new FindReplaceItem() { Pattern = @"EntryPoint=""(.*?)""", Replacement = @"EntryPoint=""$$safeprojectname$$.App""" });
            replacements.Add(new FindReplaceItem() { Pattern = @"DisplayName=""(.*?)""", Replacement = @"DisplayName=""$$projectname$$.App""" });
            replacements.Add(new FindReplaceItem() { Pattern = @"EntryPoint=""(.*?)""", Replacement = @"EntryPoint=""$$projectname$$.App""" });

            foreach (var item in replacements)
            {
                manifestText = Regex.Replace(manifestText, item.Pattern, item.Replacement);
            }

            manifestText = ReplaceIdentityNode(manifestText);

            FileHelper.WriteFile(manifestFile, manifestText);
        }       

        /// <summary>
        /// Replaces the identity node.
        /// </summary>
        /// <param name="manifestText">The manifest text.</param>
        /// <returns></returns>
        private string ReplaceIdentityNode(string manifestText)
        {
            string findText = @"<Identity";
            if (!manifestText.Contains(findText))
            {
                return manifestText;
            }

            string identityReplacementText = @"<Identity
    Name=""$guid9$""
    Publisher = ""$XmlEscapedPublisherDistinguishedName$""
    Version = ""1.0.0.0"" /> ";

            int findTextIndex, start, end;
            string firstHalf, lastHalf;

            findTextIndex = manifestText.IndexOf(findText);

            start = findTextIndex;
            end = manifestText.IndexOf("/>", findTextIndex);
            firstHalf = manifestText.Substring(0, start);
            lastHalf = manifestText.Substring(end + 2);
            return firstHalf + identityReplacementText + lastHalf;
        }

        protected override void ProcessVSTemplate(string tempFolder)
        {
            string xml = FileHelper.ReadFile(CsprojFile);
            string projectName = Path.GetFileName(CsprojFile);
            string projXml = GetProjectNode(xml, projectName);
            xml = Constants.UWPVSTEMPLATETEXT.Replace(Constants.PROJECTNODE, projXml);
            xml = xml.Replace(Constants.TEMPLATENAME, ProjectFriendlyName);
            xml = xml.Replace(Constants.TEMPLATEDESCRIPTION, ProjectDescription);
            string previewFileName = Path.GetFileName(PreviewImagePath);
            xml = xml.Replace(Constants.PREVIEWIMAGEFILE, previewFileName);

            string filePath = Path.Combine(tempFolder, Constants.UWPVSTEMPLATENAME);

            FileHelper.WriteFile(filePath, xml);
        }
    }
}
