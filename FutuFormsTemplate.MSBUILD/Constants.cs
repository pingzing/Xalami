using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutuFormTemplate.MSBUILD
{
    internal class Constants
    {
        internal const string MVVMLIGHTPROJECTJSON = @"""MvvmLightLibs"": ""5.3.0"",";

        internal const string XAMARINFORMSPROJECTJSON = @"""Xamarin.Forms"": ""2.3.3.180""";

        internal const string PROJECTJSON = "project.json";        

        internal const string TEMPFOLDER = "Temp";

        internal const string PROJECTNODE = "$projectNode";

        internal const string TEMPLATENAME = "$templateName";

        internal const string TEMPLATEDESCRIPTION = "$templateDescription";

        internal const string PREVIEWIMAGEFILE = "$previewImageFile";

        internal const string UWPVSTEMPLATENAME = "MyUwpTemplate.vstemplate";
        
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
    <TargetPlatformName>Windows</TargetPlatformName>
    <RequiredPlatformVersion>6.1.0</RequiredPlatformVersion>
    <EnableLocationBrowseButton>true</EnableLocationBrowseButton>
    <Icon>__TemplateIcon.png</Icon>
    <PreviewImage>$previewImageFile</PreviewImage>    
  </TemplateData>
  <TemplateContent PreferedSolutionConfiguration=""Debug|x86"">
$projectNode
</TemplateContent>  
</VSTemplate>";

        internal const string ANDROIDVSTEMPLATENAME = "MyAndroidTemplate.vstemplate";

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
</VSTemplate>";

        internal const string IOSVSTEMPLATENAME = "MyIosTemplate.vstemplate";

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
</VSTemplate>";

        internal const string WP8TEMPLATENAME = "MyWp8Template.vstemplate";

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
</VSTemplate>";

        internal const string PCLVSTEMPLATENAME = "MyPclTemplate.vstemplate";

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
</VSTemplate>";

        internal const string GROUPTEMPLATENAME = "FutuFormsTemplate.vstemplate";

        internal const string GROUPTEMPLATETEXT = @"<VSTemplate Version=""2.0."" Type=""ProjectGroup"" xmlns=""http://schemas.microsoft.com/developer/vstemplate/2005"">  
    <TemplateData>  
        <Name>Futu Forms Template</Name>  
        <Description>A template to kickstart your Xamarin Forms project. Produced with love by Futurice.</Description>  
        <Icon>Icon.ico</Icon>
        <ProjectType>CSharp</ProjectType>
    </TemplateData>  
    <TemplateContent>  
        <ProjectCollection>  
            <ProjectTemplateLink ProjectName = ""FutuForms"">
                PCL\" + PCLVSTEMPLATENAME + @"
            </ProjectTemplateLink>
            <ProjectTemplateLink ProjectName = ""FutuForms.UWP"">
                UWP\" + UWPVSTEMPLATENAME + @"
            </ProjectTemplateLink>
            <ProjectTemplateLink ProjectName = ""FutuForms.iOS"">
                iOS\"+ IOSVSTEMPLATENAME + @"
            </ProjectTemplateLink>
            <ProjectTemplateLink ProjectName = ""FutuForms.Android"">
                iOS\" + ANDROIDVSTEMPLATENAME + @"
            </ProjectTemplateLink>
            <ProjectTemplateLink ProjectName = ""FutuForms.WinPhone"">
                iOS\" + WP8TEMPLATENAME + @"
            </ProjectTemplateLink>
        </ProjectCollection>  
    </TemplateContent>  
</VSTemplate>";
    }
}
