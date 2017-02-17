namespace Xalami.TemplateGenerator
{
    public class CsprojItem
    {
        /// <summary>
        /// The project-relative path to this item.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// If not null, the the name (not path!) of the project item that this item depends on.
        /// </summary>
        public string DependsOn { get; set; }

        public CsprojItem(string path)
        {
            Path = path.Replace('\\', '/');
        }

        public CsprojItem(string path, string dependsOn)
        {
            Path = path.Replace('\\', '/');
            DependsOn = dependsOn;
        }
    }
}
