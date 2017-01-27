using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using Windows.ApplicationModel.Resources;

namespace FutuFormsTemplate.WinPhone.Localization
{
    /* This class exists due to a WinRT resource manager limitation that only manifests on physical devices,
     * in release mode. The issue is that WinRT must use ResourceLoader rather than ResourceManager due to
     * some implementation details behind how resources are packaged on the platform. So this class sneaks
     * into our resources', generated .resx.cs files and replaces the ResourceManager in them with a
     * ResourceLoader via Reflection.
     * More details in this blog post: https://blogs.msdn.microsoft.com/philliphoff/2014/11/19/missingmanifestresourceexception-when-using-portable-class-libraries-within-winrt/
     */

    public class WindowsRuntimeResourceManager : ResourceManager
    {
        private readonly ResourceLoader resourceLoader;

        private WindowsRuntimeResourceManager(string baseName, Assembly assembly)
            : base(baseName, assembly)
        {
            this.resourceLoader = ResourceLoader.GetForViewIndependentUse(baseName);
        }

        public static void PatchResourceManagerForResource(Type generatedResourceClass)
        {
            var runtimeField = generatedResourceClass.GetRuntimeFields()
                .First(x => x.Name == "resourceMan");
            runtimeField.SetValue(null,
                new WindowsRuntimeResourceManager(generatedResourceClass.FullName, generatedResourceClass.GetTypeInfo().Assembly));
        }

        public override string GetString(string name, CultureInfo culture)
        {
            return this.resourceLoader.GetString(name);
        }
    }
}
