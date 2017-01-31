using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.IO;
using System.Xml.Linq;
using System.Collections;
using System.Xml.XPath;
using System.Xml;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO.Compression;
using FutuFormTemplate.MSBUILD;
using System.Web;

namespace FutuFormsTemplate.MSBUILD
{

    //todo: make this build the UWP, WP8, iOS, and Android tempates, then bundle 'em all together.

    /// <summary>
    /// A custom MSBuild Task that generates a Project Template ZIP file
    /// and copies to the specified location. This is used to covert the existing
    /// Template 10 projects into project templates for deployment via the
    /// the VSIX.
    /// </summary>
    public abstract class FutuFormsTemplateTask
    {
        #region ---- Private Variables ----------------

        protected string tempFolder;
        protected ItemFolder topFolder;

        #endregion

        public string CsprojFile { get; protected set; }   
        public string ProjectFriendlyName { get; protected set; }
        public string ProjectDescription { get; protected set; }
        public string PreviewImagePath { get; protected set; }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <returns></returns>
        public abstract bool Run(string csprojPath, string targetDir, string projectFriendlyName, string projectDescription, string previewImagePath);

        /// <summary>
        /// Replaces the namespace.
        /// </summary>
        /// <param name="tempFolder">The temporary folder.</param>
        protected void ReplaceNamespace(string tempFolder)
        {
            string csprojXml = FileHelper.ReadFile(CsprojFile);
            string rootNamespace = GetExistingRootNamespace(csprojXml);
            var ext = new List<string> { ".cs", ".xaml" };
            var files = Directory.GetFiles(tempFolder, "*.*", SearchOption.AllDirectories).Where(s => ext.Any(e => s.EndsWith(e)));
            foreach (var file in files)
            {
                string text = FileHelper.ReadFile(file);
                //TODO: think about a safer way to do this... what if there is another use of RootNamespace string elsewhere... this will break the generated project.
                text = text.Replace(rootNamespace, "$safeprojectname$");
                FileHelper.WriteFile(file, text);
            }
        }
        

        /// <summary>
        /// Copies the project files to temporary folder.
        /// </summary>
        /// <param name="projectFolder">The project folder.</param>
        /// <param name="tempFolder">The temporary folder.</param>
        protected void CopyProjectFilesToTempFolder(string projectFolder, string tempFolder)
        {
            FileHelper.DirectoryCopy(projectFolder, tempFolder, true);
        }

        /// <summary>
        /// Processes the vs template.
        /// </summary>
        /// <param name="tempFolder">The temporary folder.</param>
        protected abstract void ProcessVSTemplate(string tempFolder);      

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
        /// Operates the on cs proj.
        /// </summary>
        /// <param name="tempFolder">The temporary folder.</param>
        /// <param name="csprojFile">The csproj file.</param>
        protected void OperateOnCsProj(string tempFolder, string csprojFile, bool isWindows = false)
        {
            string fileName = Path.GetFileName(CsprojFile);
            string targetPath = Path.Combine(tempFolder, fileName);

            File.Copy(CsprojFile, targetPath, true);
            string csprojText = FileHelper.ReadFile(targetPath);

            var replacements = new List<FindReplaceItem>();

            if (isWindows)
            {
                replacements.Add(new FindReplaceItem() { Pattern = @"<PackageCertificateKeyFile>(.*?)</PackageCertificateKeyFile>", Replacement = @"<PackageCertificateKeyFile>$$projectname$$_TemporaryKey.pfx</PackageCertificateKeyFile>" });
            }
            replacements.Add(new FindReplaceItem() { Pattern = "<RootNamespace>(.*?)</RootNamespace>", Replacement = "<RootNamespace>$$safeprojectname$$</RootNamespace>" });
            replacements.Add(new FindReplaceItem() { Pattern = "<AssemblyName>(.*?)</AssemblyName>", Replacement = "<AssemblyName>$$safeprojectname$$</AssemblyName>" });
            if (isWindows)
            {
                replacements.Add(new FindReplaceItem() { Pattern = @"<None Include=""(.*?)_TemporaryKey.pfx"" />", Replacement = @"<None Include=""$$projectname$$_TemporaryKey.pfx"" />" });
            }
            replacements.Add(new FindReplaceItem() { Pattern = @"<ProjectGuid>(.*?)</ProjectGuid>", Replacement = @"<ProjectGuid>$guid1$</ProjectGuid>" });


            foreach (var item in replacements)
            {
                csprojText = Regex.Replace(csprojText, item.Pattern, item.Replacement);
            }

            csprojText = RemoveItemNodeAround(@"csproj", csprojText);            

            FileHelper.WriteFile(targetPath, csprojText);
        }        

        /// <summary>
        /// Gets the existing root namespace.
        /// </summary>
        /// <param name="csprojxml">The csprojxml.</param>
        /// <returns></returns>
        protected string GetExistingRootNamespace(string csprojxml)
        {
            XDocument xdoc;
            using (StringReader sr = new StringReader(csprojxml))
            {
                xdoc = XDocument.Load(sr, LoadOptions.None);
            }

            XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
            return xdoc.Descendants(ns + "RootNamespace").FirstOrDefault().Value;

        }

        /// <summary>
        /// Removes the item node around the specified text. This is used to remove the csproj reference in the csproj file which will be replaced by a project.json NuGet reference instead.
        /// </summary>
        /// <param name="findText">The find text.</param>
        /// <param name="csprojText">The csproj text.</param>
        /// <returns></returns>
        protected string RemoveItemNodeAround(string findText, string csprojText)
        {
            if (!csprojText.Contains(findText))
            {
                return csprojText;
            }

            int findTextIndex, start, end;
            string firstHalf, lastHalf;

            findTextIndex = csprojText.IndexOf(findText);

            start = csprojText.Substring(0, findTextIndex).LastIndexOf("<ItemGroup>");
            end = csprojText.IndexOf("</ItemGroup>", findTextIndex);
            firstHalf = csprojText.Substring(0, start);
            lastHalf = csprojText.Substring(end + 12);
            return firstHalf + lastHalf;
        }        


        /// <summary>
        /// Gets the project node.
        /// </summary>
        /// <param name="csprojxml">The csproj XML.</param>
        /// <param name="projectFileName">Name of the project file.</param>
        /// <returns></returns>
        protected string GetProjectNode(string csprojxml, string projectFileName)
        {
            string projectNodeStart = @"<Project TargetFileName=""$projectName"" File=""$projectName"" ReplaceParameters=""true"">";
            projectNodeStart = projectNodeStart.Replace("$projectName", projectFileName);            
            List<string> projectItems = GetProjectItems(csprojxml);

            //-- sorting for directories
            projectItems = SortProjectItems(projectItems);
            GetItemFolder(projectItems);
            string foldersString = SerializeFolder(topFolder);            

            using (StringWriter writer = new StringWriter())
            {
                writer.WriteLine(projectNodeStart);
                writer.WriteLine(foldersString);
                writer.WriteLine("</Project>");

                return writer.ToString();
            }

        }        

        /// <summary>
        /// Serializes the folder.
        /// </summary>
        /// <param name="topFolder">The top folder.</param>
        /// <returns></returns>
        protected string SerializeFolder(ItemFolder topFolder)
        {
            string folderString = string.Empty;
            string projItemNodeTemplate = @"<ProjectItem ReplaceParameters = ""true"" TargetFileName=""$filename"">$filename</ProjectItem>";
            string folderItemNodeTemplate = @"<Folder Name=""$folderName"" TargetFolderName=""$folderName"" >";

            if (topFolder.FolderName != null)
            {
                folderString = folderItemNodeTemplate.Replace("$folderName", topFolder.FolderName);
            }

            foreach (var item in topFolder.Items)
            {
                if (IsKeyProjectItemNode(item))
                {
                    //folderString = folderString 
                    //    + @"<ProjectItem ReplaceParameters=""false"" TargetFileName=""$projectname$_TemporaryKey.pfx"" BlendDoNotCreate=""true"">Application_TemporaryKey.pfx</ProjectItem>" 
                    //    + Environment.NewLine;
                }
                else
                {
                    //-- now writing item.
                    if (!string.IsNullOrEmpty(item) && !item.Contains("csproj") && !item.Contains(".."))
                    {
                        folderString = folderString + projItemNodeTemplate.Replace("$filename", item);
                    }
                }
            }

            foreach (var folderItem in topFolder.Folders)
            {
                folderString = folderString + SerializeFolder(folderItem);
            }

            if (topFolder.FolderName != null)
            {
                folderString = folderString + "</Folder>\n";
            }

            return folderString;

        }       

        /// <summary>
        /// Gets the item folder.
        /// </summary>
        /// <param name="projectItems">The project items.</param>
        protected void GetItemFolder(List<string> projectItems)
        {
            topFolder = new ItemFolder();
            string[] stringSeparator = new string[] { @"\" };

            foreach (var item in projectItems)
            {
                var parts = item.Split(stringSeparator, StringSplitOptions.RemoveEmptyEntries);
                AddPartsToTopFolder(parts);
            }

        }

        /// <summary>
        /// Adds the parts to top folder.
        /// </summary>
        /// <param name="parts">The parts.</param>
        protected void AddPartsToTopFolder(string[] parts)
        {
            AddPartsToFolder(topFolder, parts, 0);
        }

        /// <summary>
        /// Adds the parts to folder.
        /// </summary>
        /// <param name="currentFolder">The current folder.</param>
        /// <param name="parts">The parts.</param>
        /// <param name="partIndex">Index of the part.</param>
        protected void AddPartsToFolder(ItemFolder currentFolder, string[] parts, int partIndex)
        {
            //-- empty folder
            if (partIndex >= parts.Length)
                return;

            string part = parts[partIndex];

            if (!IsFolder(part))
            {
                currentFolder.Items.Add(part);
                return;
            }

            var folder = currentFolder.Folders.FirstOrDefault(e => e.FolderName == part);

            if (folder == null)
            {
                folder = new ItemFolder() { FolderName = part };
                currentFolder.Folders.Add(folder);
            }
            AddPartsToFolder(folder, parts, ++partIndex);
        }

        /// <summary>
        /// Determines whether the specified part is folder.
        /// </summary>
        /// <param name="part">The part.</param>
        /// <returns></returns>
        protected bool IsFolder(string part)
        {
            return !part.Contains(".");
        }

        /// <summary>
        /// Sorts the project items.
        /// </summary>
        /// <param name="projectItems">The project items.</param>
        /// <returns></returns>
        protected List<string> SortProjectItems(List<string> projectItems)
        {
            projectItems.Sort();

            var l2 = new List<string>();
            foreach (var item in projectItems)
            {
                if (!item.Contains(@"\"))
                    l2.Insert(0, item);
                else
                    l2.Add(item);

            }

            projectItems = l2;
            return projectItems;
        }
       

        /// <summary>
        /// Determines whether [is key project item node] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        protected bool IsKeyProjectItemNode(string item)
        {
            return item.Contains(".pfx");
        }

        /// <summary>
        /// Gets the project items.
        /// </summary>
        /// <param name="csprojxml">The csprojxml.</param>
        /// <returns></returns>
        protected List<string> GetProjectItems(string csprojxml)
        {
            List<string> files = new List<string>(); ;
            XDocument xdoc;
            using (StringReader sr = new StringReader(csprojxml))
            {
                xdoc = XDocument.Load(sr, LoadOptions.None);
            }

            XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
            var items = xdoc.Descendants(ns + "ItemGroup");
            string itemString = string.Empty;
            foreach (var itemG in items)
            {
                foreach (var item in itemG.Elements())
                {
                    itemString = item.Attribute("Include").Value;
                    if (!string.IsNullOrEmpty(itemString) 
                        && !itemString.Contains("=") 
                        && !itemString.Contains(",")
                        && item.Name.LocalName != "Reference")
                    {                        
                        files.Add(HttpUtility.UrlDecode(itemString)); //need the decode here, because @ symbols are stored URL-encoded in csproj files. And those get used in iOS filenames!
                    }
                }
            }

            return files;

        }
    }
}
