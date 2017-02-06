using System.Collections.Generic;

namespace Xalami.MSBUILD
{
    /// <summary>
    /// Used to keep track of the Item folders during conversion in vstemplate
    /// </summary>
    public class ItemFolder
    {
        internal ItemFolder()
        {

            Folders = new List<ItemFolder>();
            Items = new List<string>();
        }

        internal string FolderName { get; set; }

        internal List<ItemFolder> Folders { get; set; }

        internal List<string> Items { get; set; }

    }
}
