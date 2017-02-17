namespace Xalami.TemplateGenerator
{
    internal class Constants
    {
        internal const string XAMARINFORMSVERSION = "\"2.3.3.180\"";        
        internal const string MVVMLIGHTVERSION = "\"5.3.0\"";

        internal const string TEMPFOLDER = "Temp";

        internal const string PREVIEWIMAGEKEY = "$previewImage";
        internal const string ICONKEY = "$icon";        

        #region VSTemplate constants        

        internal const string PROJECTNODE = "$projectNode";                

        internal const string UWPPLATFORMSUFFIX = "UWP";
        internal const string UWPVSTEMPLATENAME = "UWP.vstemplate";        
        internal const string UWPVSTEMPLATETEXT = @"<VSTemplate Version=""3.0.0"" xmlns=""http://schemas.microsoft.com/developer/vstemplate/2005"" Type=""Project"">
  <TemplateData>
    <Name>Xalami.UWP</Name>    
    <Icon>__TemplateIcon.ico</Icon>
    <ProjectType>CSharp</ProjectType>
    <ProjectSubType>Xalami</ProjectSubType>
    <SortOrder>0</SortOrder>        
    <CreateNewFolder>true</CreateNewFolder>
    <DefaultName>FormsApp.UWP</DefaultName>
    <ProvideDefaultName>true</ProvideDefaultName>
    <LocationField>Enabled</LocationField>    
    <EnableLocationBrowseButton>true</EnableLocationBrowseButton>        
  </TemplateData>
  <TemplateContent PreferedSolutionConfiguration=""Debug|x86"">
$projectNode
</TemplateContent>
<WizardExtension>
    <Assembly>Microsoft.VisualStudio.WinRT.TemplateWizards, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</Assembly>
    <FullClassName>Microsoft.VisualStudio.WinRT.TemplateWizards.CreateProjectCertificate.Wizard</FullClassName>
  </WizardExtension>
  <WizardExtension>
    <Assembly>Xamarin.VisualStudio.TemplateWizards, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756</Assembly>
    <FullClassName>Xamarin.VisualStudio.TemplateWizards.ExtractRootProjectNameWizard</FullClassName>
  </WizardExtension>
  <WizardExtension>
    <Assembly>NuGet.VisualStudio.Interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</Assembly>
    <FullClassName>NuGet.VisualStudio.TemplateWizard</FullClassName>
  </WizardExtension>
  <WizardExtension>
    <Assembly>Microsoft.VisualStudio.Universal.TemplateWizards, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</Assembly>
    <FullClassName>Microsoft.VisualStudio.Universal.TemplateWizards.PlatformVersion.Wizard</FullClassName>
  </WizardExtension>
  <WizardExtension>
    <Assembly>Xamarin.VisualStudio.TemplateWizards, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756</Assembly>
    <FullClassName>Xamarin.VisualStudio.TemplateWizards.EnsureRegistryWizard</FullClassName>
  </WizardExtension>
  <WizardData>
    <packages repository=""registry"" keyName=""NETCoreSDK"" isPreunzipped=""true"">
      <package id = ""Microsoft.NETCore.UniversalWindowsPlatform"" version=""5.0.0"" skipAssemblyReferences=""false"" />
    </packages>    
    <EnsureRegistryWizard>
      <Key>_Config\Projects\{A5A43C5B-DE2A-4C0C-9213-0A381AF9435A}</Key>
      <Backout>false</Backout>
    </EnsureRegistryWizard>
  </WizardData>  
</VSTemplate>";

        internal const string ANDROIDPLATFORMSUFFIX = "Android";
        internal const string ANDROIDVSTEMPLATENAME = "Android.vstemplate";
        internal const string ANDROIDVSTEMPLATETEXT = @"<VSTemplate Version=""3.0.0"" xmlns=""http://schemas.microsoft.com/developer/vstemplate/2005"" Type=""Project"">
  <TemplateData>
    <Name>Xalami.Android</Name>    
    <Icon>__TemplateIcon.ico</Icon>
    <ProjectType>CSharp</ProjectType>
    <ProjectSubType>Xalami</ProjectSubType>
    <SortOrder>0</SortOrder>        
    <CreateNewFolder>true</CreateNewFolder>
    <DefaultName>FormsApp.Android</DefaultName>
    <ProvideDefaultName>true</ProvideDefaultName>
    <LocationField>Enabled</LocationField>    
    <EnableLocationBrowseButton>true</EnableLocationBrowseButton>    
  </TemplateData>
  <TemplateContent PreferedSolutionConfiguration=""Debug|AnyCPU"">
$projectNode
</TemplateContent>
  <WizardExtension>
    <Assembly>Xamarin.VisualStudio.TemplateWizards, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756</Assembly>
    <FullClassName>Xamarin.VisualStudio.TemplateWizards.ExtractRootProjectNameWizard</FullClassName>
  </WizardExtension>  
  <WizardExtension>
    <Assembly>Xamarin.VisualStudio.TemplateWizards, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756</Assembly>
    <FullClassName>Xamarin.VisualStudio.TemplateWizards.AndroidTargetFrameworkVersionWizard</FullClassName>
  </WizardExtension>    
</VSTemplate>";

        internal const string IOSPLATFORMSUFFIX = "iOS";
        internal const string IOSVSTEMPLATENAME = "iOS.vstemplate";
        internal const string IOSVSTEMPLATETEXT = @"<VSTemplate Version=""3.0.0"" xmlns=""http://schemas.microsoft.com/developer/vstemplate/2005"" Type=""Project"">
  <TemplateData>
    <Name>Xalami.iOS</Name>    
    <Icon>__TemplateIcon.ico</Icon>
    <ProjectType>CSharp</ProjectType>
    <ProjectSubType>Xalami</ProjectSubType>
    <SortOrder>0</SortOrder>        
    <CreateNewFolder>true</CreateNewFolder>
    <DefaultName>FormsApp.iOS</DefaultName>
    <ProvideDefaultName>true</ProvideDefaultName>
    <LocationField>Enabled</LocationField>    
    <EnableLocationBrowseButton>true</EnableLocationBrowseButton>    
  </TemplateData>
  <TemplateContent PreferedSolutionConfiguration=""Debug|iPhone"">
$projectNode
</TemplateContent>
  <WizardExtension>
    <Assembly>Xamarin.VisualStudio.TemplateWizards, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756</Assembly>
    <FullClassName>Xamarin.VisualStudio.TemplateWizards.ExtractRootProjectNameWizard</FullClassName>
  </WizardExtension>
  <WizardExtension>
    <Assembly>Xamarin.VisualStudio.TemplateWizards, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756</Assembly>
    <FullClassName>Xamarin.VisualStudio.TemplateWizards.SanitizedAssemblyNameWizard</FullClassName>
  </WizardExtension>   
</VSTemplate>";

        internal const string WP8PLATFORMSUFFIX = "WinPhone";
        internal const string WP8TEMPLATENAME = "WinPhone.vstemplate";
        internal const string WP8TEMPLATETEXT = @"<VSTemplate Version=""3.0.0"" xmlns=""http://schemas.microsoft.com/developer/vstemplate/2005"" Type=""Project"">
  <TemplateData>
    <Name>Xalami.WinPhone</Name>    
    <Icon>__TemplateIcon.ico</Icon>
    <ProjectType>CSharp</ProjectType>
    <ProjectSubType>Xalami</ProjectSubType>
    <SortOrder>0</SortOrder>        
    <CreateNewFolder>true</CreateNewFolder>
    <DefaultName>FormsApp.WinPhone</DefaultName>
    <ProvideDefaultName>true</ProvideDefaultName>
    <LocationField>Enabled</LocationField>    
    <EnableLocationBrowseButton>true</EnableLocationBrowseButton>    
  </TemplateData>
  <TemplateContent PreferedSolutionConfiguration=""Debug|iPhone"">
$projectNode
</TemplateContent>  
  <WizardExtension>
    <Assembly>Xamarin.VisualStudio.TemplateWizards, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756</Assembly>
    <FullClassName>Xamarin.VisualStudio.TemplateWizards.ExtractRootProjectNameWizard</FullClassName>
  </WizardExtension> 
  <WizardExtension>
    <Assembly>Microsoft.VisualStudio.WinRT.TemplateWizards, Version=14.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</Assembly>
    <FullClassName>Microsoft.VisualStudio.WinRT.TemplateWizards.CreateProjectCertificate.Wizard</FullClassName>
  </WizardExtension>
  <WizardExtension>
    <Assembly>Xamarin.VisualStudio.TemplateWizards, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756</Assembly>
    <FullClassName>Xamarin.VisualStudio.TemplateWizards.EnsureRegistryWizard</FullClassName>
  </WizardExtension>
  <WizardData>       
    <EnsureRegistryWizard>
      <Key>_Config\Projects\{76F1466A-8B6D-4E39-A767-685A06062A39}</Key>
      <Backout>false</Backout>
    </EnsureRegistryWizard>
  </WizardData>  
</VSTemplate>";

        internal const string PCLPLATFORMSUFFIX = ""; //This variable intentionally left blank.
        internal const string PCLVSTEMPLATENAME = "PCL.vstemplate";
        internal const string PCLVSTEMPLATETEXT = @"<VSTemplate Version=""3.0.0"" xmlns=""http://schemas.microsoft.com/developer/vstemplate/2005"" Type=""Project"">
  <TemplateData>
    <Name>Xalami</Name>
    <Icon>__TemplateIcon.ico</Icon>
    <ProjectType>CSharp</ProjectType>        
    <CreateNewFolder>true</CreateNewFolder>
    <DefaultName>FormsApp</DefaultName>
    <ProvideDefaultName>true</ProvideDefaultName>
    <LocationField>Enabled</LocationField>    
    <EnableLocationBrowseButton>true</EnableLocationBrowseButton>    
  </TemplateData>
  <TemplateContent PreferedSolutionConfiguration=""Debug|AnyCPU"">
$projectNode
</TemplateContent>   
  <WizardExtension>
    <Assembly>Xamarin.VisualStudio.TemplateWizards, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756</Assembly>
    <FullClassName>Xamarin.VisualStudio.TemplateWizards.FormsWizard</FullClassName>
  </WizardExtension>    
</VSTemplate>";

        internal const string GROUPTEMPLATENAME = "Xalami.vstemplate";


        // The CopyParameters="true" allows the usage of $ext_<parameter name>$ template 
        // replacement parameters, which allows child templates
        // access to variables in the parent template.
        internal const string GROUPTEMPLATETEXT = @"<VSTemplate Version=""3.0.0"" Type=""ProjectGroup"" xmlns=""http://schemas.microsoft.com/developer/vstemplate/2005"">  
    <TemplateData>  
        <Name>Xalami</Name>  
        <ProvideDefaultName>true</ProvideDefaultName>
        <DefaultName>Xalami</DefaultName>        
        <CreateNewFolder>true</CreateNewFolder>
        <ProjectType>CSharp</ProjectType>
        <SortOrder>0</SortOrder>        
        <Description>A template to kickstart your Xamarin Forms project. Produced with &lt;3 by Futurice.</Description>  
        <Icon>" + ICONKEY + @"</Icon>
        <PreviewImage>" + PREVIEWIMAGEKEY + @"</PreviewImage>        
    </TemplateData>  
    <TemplateContent>  
        <ProjectCollection>  
            <ProjectTemplateLink ProjectName = ""$safeprojectname$"" CopyParameters=""true"">
                PCL\" + PCLVSTEMPLATENAME + @"
            </ProjectTemplateLink>
            <ProjectTemplateLink ProjectName = ""$safeprojectname$.UWP"" CopyParameters=""true"">
                UWP\" + UWPVSTEMPLATENAME + @"
            </ProjectTemplateLink>
            <ProjectTemplateLink ProjectName = ""$safeprojectname$.iOS"" CopyParameters=""true"">
                iOS\" + IOSVSTEMPLATENAME + @"
            </ProjectTemplateLink>
            <ProjectTemplateLink ProjectName = ""$safeprojectname$.Android"" CopyParameters=""true"">
                Android\" + ANDROIDVSTEMPLATENAME + @"
            </ProjectTemplateLink>
            <ProjectTemplateLink ProjectName = ""$safeprojectname$.WinPhone"" CopyParameters=""true"">
                WinPhone\" + WP8TEMPLATENAME + @"
            </ProjectTemplateLink>
        </ProjectCollection>  
    </TemplateContent>  
</VSTemplate>";
        #endregion

        #region Xamarin Studio Template
        internal const string FILENODE = "$fileNode";
        internal const string PCLFILEKEY = "$pclFilesNode";
        internal const string ANDROIDFILESKEY = "$androidFilesNode";
        internal const string IOSFILESKEY = "$iosFilesKey";

        internal const string XPTXMLTEXT = @"<?xml version=""1.0""?>
<Template>
    <TemplateConfiguration>
        <_Name>Xalami</_Name>
        <Category>crossplat/app/forms</Category>
        <LanguageName>C#</LanguageName>
        <Icon>res:Xalami.XamarinStudioAddin.Icons." + ICONKEY + @"</Icon>
        <Icon32>Icons/" + ICONKEY + @"</Icon32>
        <_Description>A template to kickstart your Xamarin Forms project. Produced with &lt;3 by Futurice.</_Description>
        <DefaultFilename>Xalami</DefaultFilename>
        <FileExtension>.csproj</FileExtension>
    </TemplateConfiguration>
    
    <Actions>
    </Actions>
    
    <Combine name=""${ProjectName}"" directory=""."">		
        <Project name=""${ProjectName}"" directory=""./${ProjectName}"" type=""C#PortableLibrary"">
            <Options Target=""Library"" TargetFrameworkVersion="".NETPortable,Version=v4.5,Profile=Profile259""/>
            <References>
                <Reference type = ""Package"" refto=""System""/>
            </References>            
            $pclFilesNode            
            <Packages>
                <Package Id=""Xamarin.Forms"" version="+ XAMARINFORMSVERSION +@"/>
                <Package Id=""MvvmLightLibs"" version=" + MVVMLIGHTVERSION + @"/>
            </Packages>
        </Project>

        <Project name = ""${ProjectName}.Android"" directory=""./${ProjectName}.Android"" type=""MonoDroid"">
            <Options AndroidApplication = ""true"" AndroidResgenFile=""Resources/Resource.Designer.cs"" GenerateSerializationAssemblies=""Off""
                     ProductVersion=""8.0.30703"" SchemaVersion=""2.0"" AppDesignerFolder=""Properties"" FileAlignment=""512""
                     JavaMaximumHeapSize=""1G""/>
            <References>
                <Reference type=""Package"" refto=""mscorlib"" />
                <Reference type=""Package"" refto=""System"" />
                <Reference type=""Package"" refto=""System.Core"" />
                <Reference type=""Package"" refto=""System.Xml"" />
                <Reference type=""Package"" refto=""System.Xml.Linq"" />
                <Reference type=""Package"" refto=""Mono.Android"" />
                <Reference type=""Package"" refto=""Mono.Android.Export"" />
                <Reference type=""Project"" refto=""${ProjectName}"" />
            </References>            
            $androidFilesNode            
            <Packages>
                <Package Id=""Xamarin.Forms"" version=" + XAMARINFORMSVERSION + @"/>
                <Package Id=""MvvmLightLibs"" version=" + MVVMLIGHTVERSION + @"/>
            </Packages>
        </Project>
        
        <Project name=""${ProjectName}.iOS"" directory=""./${ProjectName}.iOS"" type=""XamarinIOS"">
            <Options TargetFrameworkVersion=""Xamarin.iOS,Version=v1.0"" />
            <References>
                <Reference type=""Package"" refto=""System"" />
                <Reference type=""Package"" refto=""System.Core"" />
                <Reference type=""Package"" refto=""System.Xml"" />
                <Reference type=""Package"" refto=""Xamarin.iOS"" />
                <Reference type=""Project"" refto=""${ProjectName}"" />
            </References>            
            $iosFilesNode            
            <Packages>
                <Package Id=""Xamarin.Forms"" version=" + XAMARINFORMSVERSION + @"/>
                <Package Id=""MvvmLightLibs"" version=" + MVVMLIGHTVERSION + @"/>
            </Packages>
        </Project>
        
    </Combine>
</Template>";


        #endregion        
    }
}
