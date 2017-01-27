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

        internal const string XAMARINFORMSPROJECTJSON = @"""Xamarin.Forms"": ""2.3.*.*""";

        internal const string PROJECTJSON = "project.json";        

        internal const string TEMPFOLDER = "Temp";

        internal const string PROJECTNODE = "$projectNode";

        internal const string TEMPLATENAME = "$templateName";

        internal const string TEMPLATEDESCRIPTION = "$templateDescription";

        internal const string PREVIEWIMAGEFILE = "$previewImageFile";

        internal const string UWPVSTEMPLATENAME = "MyUwpTemplate.vstemplate";
        
        internal const string UWPVSTEMPLATETEXT = @"<VSTemplate Version=""3.0.0"" xmlns=""http://schemas.microsoft.com/developer/vstemplate/2005"" Type=""Project"">
  <TemplateData>
    <Name>$templateName$.UWP</Name>
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
    }
}
