MT.Net Core
===========
The Core library compiles to a DLL which is referenced in an ASP.Net MVC project.

One item of configuration must be set:

    <configSections>
        <section name="mtNet" type="MTNet.Core.Configuration" />
    </configSections>
    <mtNet dataPath="~/../Sample Data" />

"dataPath" is the relative or absolute path to a directory of MT-provided XML files (the Sample Data in the project will suffice to start).

ContentFilter should be registered as an HttpModule in the website project:

    <modules>
         <add name="MTNetContentFilter" type="MTNet.Core.ContentFilter" />
    </modules>

The ContentFilter binds to the BeginRequest event in the page request lifecycle. On every request, it checks with the ContentRepository to determine if there's any content which matches the inbound path (on the first request, ContentRepository will populate with all the data provided).

If a matching ContentItem object is found, it is added to the Items collection of the HttpContext object and the URL is rewritten to the controller and action name as provided in the configuration (defaults to "Content" and "Display").  That controller (which is intended to exist in the web project), can retrieve the ContentItem from the HttpContect.Items colection ("ContentItem" key), and display it as desired.

Note that a bare bones sample content controller is included in the Core library. It is intended that you will use a controller built for your specific project.