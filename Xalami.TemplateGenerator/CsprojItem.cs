namespace Xalami.TemplateGenerator
{
    public class CsprojItem
    {
        /// <summary>
        /// The project-relative path to this item.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// If not null, the name (not path!) of the project item that this item depends on.
        /// </summary>
        public string DependsOn { get; set; }

        /// <summary>
        /// If not null, the name of the Custom Tool attribute to attach to this file in Visual/Xamarin Studio.
        /// </summary>
        public string CustomTool { get; set; }        

        public CsprojItem(string path, string dependsOn = null, string customTool = null)
        {
            Path = path.Replace('\\', '/');
            DependsOn = dependsOn;
            CustomTool = customTool;
        }        
    }
}
