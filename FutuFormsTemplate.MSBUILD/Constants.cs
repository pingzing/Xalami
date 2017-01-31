using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutuFormTemplate.MSBUILD
{
    internal class Constants
    {
        internal const string XAMARINFORMSVERSION = "\"2.3.3.180\"";
        internal const string ANDROIDSUPPORTVERSION = "\"23.3.0\"";
        internal const string ANDROIDSUPPORTDESIGNVERSION = "\"23.3.0\"";
        internal const string ANDROIDAPPCOMPATVERSION = "\"23.3.0\"";
        internal const string ANDROIDCARDVIEWVERSION = "\"23.3.0\"";
        internal const string ANDROIDMEDIAROUTERVERSION = "\"23.3.0\"";
        internal const string MVVMLIGHTVERSION = "\"5.3.0\"";

        internal const string TEMPFOLDER = "Temp";

        internal const string PROJECTNODE = "$projectNode";

        internal const string TEMPLATENAME = "$templateName";

        internal const string TEMPLATEDESCRIPTION = "$templateDescription";

        internal const string PREVIEWIMAGEFILE = "$previewImageFile";

        internal const string UWPVSTEMPLATENAME = "UWP.vstemplate";
        
        internal const string UWPVSTEMPLATETEXT = @"<VSTemplate Version=""3.0.0"" xmlns=""http://schemas.microsoft.com/developer/vstemplate/2005"" Type=""Project"">
  <TemplateData>
    <Name>$templateName.UWP</Name>
    <Description>$templateDescription</Description>
    <ProjectType>CSharp</ProjectType>
    <ProjectSubType>FutuFormsTemplate</ProjectSubType>
    <SortOrder>0</SortOrder>        
    <CreateNewFolder>true</CreateNewFolder>
    <DefaultName>FormsApp.UWP</DefaultName>
    <ProvideDefaultName>true</ProvideDefaultName>
    <LocationField>Enabled</LocationField>    
    <EnableLocationBrowseButton>true</EnableLocationBrowseButton>
    <Icon>__TemplateIcon.png</Icon>
    <PreviewImage>$previewImageFile</PreviewImage>    
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
    <packages repository=""extension"" repositoryId=""FutuForms.VsixInstaller.236a11fc-545b-40f0-96fe-9e6dc5aee3db"">
      <package id=""Xamarin.Forms"" version=" + XAMARINFORMSVERSION + @" />
      <package id=""MvvmLight"" version=" + MVVMLIGHTVERSION + @"/> 
    </packages>
    <EnsureRegistryWizard>
      <Key>_Config\Projects\{A5A43C5B-DE2A-4C0C-9213-0A381AF9435A}</Key>
      <Backout>false</Backout>
    </EnsureRegistryWizard>
  </WizardData>  
</VSTemplate>";

        internal const string ANDROIDVSTEMPLATENAME = "Droid.vstemplate";

      //    <package id = ""Xamarin.Android.Support.v4"" version=" + ANDROIDSUPPORTVERSION + @" />
      //<package id = ""Xamarin.Android.Support.Design"" version=" + ANDROIDSUPPORTDESIGNVERSION + @" />
      //<package id = ""Xamarin.Android.Support.v7.AppCompat"" version=" + ANDROIDAPPCOMPATVERSION + @" />
      //<package id = ""Xamarin.Android.Support.v7.CardView"" version=" + ANDROIDCARDVIEWVERSION + @" />
      //<package id = ""Xamarin.Android.Support.v7.MediaRouter"" version=" + ANDROIDMEDIAROUTERVERSION + @" />

        internal const string ANDROIDVSTEMPLATETEXT = @"<VSTemplate Version=""3.0.0"" xmlns=""http://schemas.microsoft.com/developer/vstemplate/2005"" Type=""Project"">
  <TemplateData>
    <Name>$templateName.Droid</Name>
    <Description>$templateDescription</Description>
    <ProjectType>CSharp</ProjectType>
    <ProjectSubType>FutuFormsTemplate</ProjectSubType>
    <SortOrder>0</SortOrder>        
    <CreateNewFolder>true</CreateNewFolder>
    <DefaultName>FormsApp.Droid</DefaultName>
    <ProvideDefaultName>true</ProvideDefaultName>
    <LocationField>Enabled</LocationField>    
    <EnableLocationBrowseButton>true</EnableLocationBrowseButton>
    <Icon>__TemplateIcon.png</Icon>
    <PreviewImage>$previewImageFile</PreviewImage>    
  </TemplateData>
  <TemplateContent PreferedSolutionConfiguration=""Debug|AnyCPU"">
$projectNode
</TemplateContent>
  <WizardExtension>
    <Assembly>Xamarin.VisualStudio.TemplateWizards, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756</Assembly>
    <FullClassName>Xamarin.VisualStudio.TemplateWizards.ExtractRootProjectNameWizard</FullClassName>
  </WizardExtension>
  <WizardExtension>
    <Assembly>NuGet.VisualStudio.Interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</Assembly>
    <FullClassName>NuGet.VisualStudio.TemplateWizard</FullClassName>
  </WizardExtension>
  <WizardExtension>
    <Assembly>Xamarin.VisualStudio.TemplateWizards, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756</Assembly>
    <FullClassName>Xamarin.VisualStudio.TemplateWizards.AndroidTargetFrameworkVersionWizard</FullClassName>
  </WizardExtension>
  <WizardData>   
    <packages repository=""extension"" repositoryId=""FutuForms.VsixInstaller.236a11fc-545b-40f0-96fe-9e6dc5aee3db"">
      <package id=""Xamarin.Forms"" version=" + XAMARINFORMSVERSION + @" />                
      <package id=""MvvmLight"" version=" + MVVMLIGHTVERSION + @"/> 
    </packages>
  </WizardData>  
</VSTemplate>";

        internal const string IOSVSTEMPLATENAME = "iOS.vstemplate";

        internal const string IOSVSTEMPLATETEXT = @"<VSTemplate Version=""3.0.0"" xmlns=""http://schemas.microsoft.com/developer/vstemplate/2005"" Type=""Project"">
  <TemplateData>
    <Name>$templateName.iOS</Name>
    <Description>$templateDescription</Description>
    <ProjectType>CSharp</ProjectType>
    <ProjectSubType>FutuFormsTemplate</ProjectSubType>
    <SortOrder>0</SortOrder>        
    <CreateNewFolder>true</CreateNewFolder>
    <DefaultName>FormsApp.iOS</DefaultName>
    <ProvideDefaultName>true</ProvideDefaultName>
    <LocationField>Enabled</LocationField>    
    <EnableLocationBrowseButton>true</EnableLocationBrowseButton>
    <Icon>__TemplateIcon.png</Icon>
    <PreviewImage>$previewImageFile</PreviewImage>    
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
 <WizardExtension>
    <Assembly>NuGet.VisualStudio.Interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</Assembly>
    <FullClassName>NuGet.VisualStudio.TemplateWizard</FullClassName>
  </WizardExtension>
  <WizardData>   
    <packages repository=""extension"" repositoryId=""FutuForms.VsixInstaller.236a11fc-545b-40f0-96fe-9e6dc5aee3db"">
      <package id=""Xamarin.Forms"" version=" + XAMARINFORMSVERSION + @" />
      <package id=""MvvmLight"" version=" + MVVMLIGHTVERSION + @"/> 
    </packages>
  </WizardData>    
</VSTemplate>";

        internal const string WP8TEMPLATENAME = "WinPhone.vstemplate";

        internal const string WP8TEMPLATETEXT = @"<VSTemplate Version=""3.0.0"" xmlns=""http://schemas.microsoft.com/developer/vstemplate/2005"" Type=""Project"">
  <TemplateData>
    <Name>$templateName.WinPhone</Name>
    <Description>$templateDescription</Description>
    <ProjectType>CSharp</ProjectType>
    <ProjectSubType>FutuFormsTemplate</ProjectSubType>
    <SortOrder>0</SortOrder>        
    <CreateNewFolder>true</CreateNewFolder>
    <DefaultName>FormsApp.WinPhone</DefaultName>
    <ProvideDefaultName>true</ProvideDefaultName>
    <LocationField>Enabled</LocationField>    
    <EnableLocationBrowseButton>true</EnableLocationBrowseButton>
    <Icon>__TemplateIcon.png</Icon>
    <PreviewImage>$previewImageFile</PreviewImage>    
  </TemplateData>
  <TemplateContent PreferedSolutionConfiguration=""Debug|iPhone"">
$projectNode
</TemplateContent>  
  <WizardExtension>
    <Assembly>Xamarin.VisualStudio.TemplateWizards, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756</Assembly>
    <FullClassName>Xamarin.VisualStudio.TemplateWizards.ExtractRootProjectNameWizard</FullClassName>
  </WizardExtension>
 <WizardExtension>
    <Assembly>NuGet.VisualStudio.Interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</Assembly>
    <FullClassName>NuGet.VisualStudio.TemplateWizard</FullClassName>
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
    <packages repository=""extension"" repositoryId=""FutuForms.VsixInstaller.236a11fc-545b-40f0-96fe-9e6dc5aee3db"">
      <package id=""Xamarin.Forms"" version=" + XAMARINFORMSVERSION + @" />
      <package id=""MvvmLight"" version=" + MVVMLIGHTVERSION + @"/> 
    </packages>
    <EnsureRegistryWizard>
      <Key>_Config\Projects\{76F1466A-8B6D-4E39-A767-685A06062A39}</Key>
      <Backout>false</Backout>
    </EnsureRegistryWizard>
  </WizardData>  
</VSTemplate>";

        internal const string PCLVSTEMPLATENAME = "PCL.vstemplate";

        internal const string PCLVSTEMPLATETEXT = @"<VSTemplate Version=""3.0.0"" xmlns=""http://schemas.microsoft.com/developer/vstemplate/2005"" Type=""Project"">
  <TemplateData>
    <Name>$templateName</Name>
    <Description>$templateDescription</Description>
    <ProjectType>CSharp</ProjectType>
    <ProjectSubType>FutuFormsTemplate</ProjectSubType>
    <SortOrder>0</SortOrder>        
    <CreateNewFolder>true</CreateNewFolder>
    <DefaultName>FormsApp</DefaultName>
    <ProvideDefaultName>true</ProvideDefaultName>
    <LocationField>Enabled</LocationField>    
    <EnableLocationBrowseButton>true</EnableLocationBrowseButton>
    <Icon>__TemplateIcon.png</Icon>
    <PreviewImage>$previewImageFile</PreviewImage>    
  </TemplateData>
  <TemplateContent PreferedSolutionConfiguration=""Debug|AnyCPU"">
$projectNode
</TemplateContent>  
 <WizardExtension>
    <Assembly>NuGet.VisualStudio.Interop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a</Assembly>
    <FullClassName>NuGet.VisualStudio.TemplateWizard</FullClassName>
  </WizardExtension>
  <WizardExtension>
    <Assembly>Xamarin.VisualStudio.TemplateWizards, Version=1.0.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756</Assembly>
    <FullClassName>Xamarin.VisualStudio.TemplateWizards.FormsWizard</FullClassName>
  </WizardExtension>
  <WizardData>   
    <packages repository=""extension"" repositoryId=""FutuForms.VsixInstaller.236a11fc-545b-40f0-96fe-9e6dc5aee3db"">
      <package id=""Xamarin.Forms"" version=" + XAMARINFORMSVERSION + @" />
      <package id=""MvvmLight"" version=" + MVVMLIGHTVERSION + @"/> 
    </packages>
  </WizardData>  
</VSTemplate>";

        internal const string GROUPTEMPLATENAME = "FutuFormsTemplate.vstemplate";

        internal const string GROUPTEMPLATETEXT = @"<VSTemplate Version=""3.0.0"" Type=""ProjectGroup"" xmlns=""http://schemas.microsoft.com/developer/vstemplate/2005"">  
    <TemplateData>  
        <Name>Futu Forms Template</Name>  
        <Description>A template to kickstart your Xamarin Forms project. Produced with love by Futurice.</Description>  
        <Icon>Icon.ico</Icon>
        <ProjectType>CSharp</ProjectType>
    </TemplateData>  
    <TemplateContent>  
        <ProjectCollection>  
            <ProjectTemplateLink ProjectName = ""$safeprojectname$"">
                PCL\" + PCLVSTEMPLATENAME + @"
            </ProjectTemplateLink>
            <ProjectTemplateLink ProjectName = ""$safeprojectname$.UWP"">
                UWP\" + UWPVSTEMPLATENAME + @"
            </ProjectTemplateLink>
            <ProjectTemplateLink ProjectName = ""$safeprojectname$.iOS"">
                iOS\" + IOSVSTEMPLATENAME + @"
            </ProjectTemplateLink>
            <ProjectTemplateLink ProjectName = ""$safeprojectname$.Android"">
                Android\" + ANDROIDVSTEMPLATENAME + @"
            </ProjectTemplateLink>
            <ProjectTemplateLink ProjectName = ""$safeprojectname$.WinPhone"">
                WinPhone\" + WP8TEMPLATENAME + @"
            </ProjectTemplateLink>
        </ProjectCollection>  
    </TemplateContent>  
</VSTemplate>";
    }
}
