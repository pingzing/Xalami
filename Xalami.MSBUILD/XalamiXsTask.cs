using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace Xalami.MSBUILD
{
    public static class XalamiXsTaskHelper
    {
        public static void ReplaceNamespace(string tempFolder, string csprojPath)
        {
            string csprojXml = FileHelper.ReadFile(csprojPath);
            string rootNamespace = GetExistingRootNamespace(csprojXml);
            var ext = new List<string> { ".cs", "xaml" };
            var files = Directory.GetFiles(tempFolder, "*.*", SearchOption.AllDirectories).Where(s => ext.Any(e => s.EndsWith(e)));
            foreach(var file in files)
            {
                string text = FileHelper.ReadFile(file);
                text = text.Replace(rootNamespace, "${ProjectName}");
                FileHelper.WriteFile(file, text);
            }
        }

        /// <summary>
        /// Gets the existing root namespace.
        /// </summary>
        /// <param name="csprojxml">The csprojxml.</param>
        /// <returns></returns>
        public static string GetExistingRootNamespace(string csprojxml)
        {
            XDocument xdoc;
            using (StringReader sr = new StringReader(csprojxml))
            {
                xdoc = XDocument.Load(sr, LoadOptions.None);
            }

            XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
            return xdoc.Descendants(ns + "RootNamespace").FirstOrDefault().Value.Split('.').FirstOrDefault();
        }

        public static void CopyProjectFilesToTempFilder(string projectFolder, string tempFolder)
        {
            FileHelper.DirectoryCopy(projectFolder, tempFolder, true);
        }
        
        public static void CopyEmbeddedFilesToOutput(string targetDir)
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
        
        public static string GenerateFilesNode(string csprojXml, string projectFileName)
        {
            //Get project items
            List<string> files = new List<string>();
            XDocument xdoc;
            using (StringReader sr = new StringReader(csprojXml))
            {
                xdoc = XDocument.Load(sr, LoadOptions.None);
            }

            XNamespace ns = "http://schemas.microsoft.com/developer/msbuild/2003";
            var items = xdoc.Descendants(ns + "ItemGroup");
            string itemString = String.Empty;
            foreach (var node in items)
            {
                //todo: might need more than just the name here. might need DependentUpon info, etc too
                foreach(var item in node.Elements())
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
            //end getting project items            

            //Sort project items so items at the top level directory appear first
            files.Sort();
            var tempList = new List<string>();
            foreach (var item in files)
            {
                if (!item.Contains(@"\"))
                {
                    tempList.Insert(0, item);
                }
                else
                {
                    tempList.Add(item);
                }
            }
            files = tempList;
            //end sorting project items

            //Get Item folder
            ItemFolder topFolder = new ItemFolder();
            string[] stringSeparator = new string[] { @"\" };
            foreach (var item in files)
            {
                var parts = item.Split(stringSeparator, StringSplitOptions.RemoveEmptyEntries);                
                AddPartsToFolderRecursive(topFolder, parts, 0);                
            }
            //end getting item folder

            //Begin serializing folder
            
            // todo: use XalamiVsTask.SerializeFolder as inspiration,
            // but don't actually copy it. This is where we do the actual work
            // of generating the strings we need to stick between the <Files> node.

            //end serializing folder

            using (StringWriter writer = new StringWriter())
            {
                writer.WriteLine("<Files>");
                writer.WriteLine(folderString);
                writer.WriteLine("</Files>");

                return writer.ToString();
            }
        }

        private static void AddPartsToFolderRecursive(ItemFolder currentFolder, string[] parts, int partIndex)
        {
            // this is an empty folder, we're done
            if (partIndex >= parts.Length)
            {
                return;
            }

            string part = parts[partIndex];

            if(part.Contains(".")) //if the part contains a dot, it represents a folder
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
            AddPartsToFolderRecursive(folder, parts, partIndex + 1);
        }
    }
}
