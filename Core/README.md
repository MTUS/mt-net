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

If a matching ContentItem object is found, it is added to the Items collection of the HttpContext object and the URL is rewritten to the controller and action name as provided in the configuration (defaults to "Content" and "Display").  That controller (which is intended to exist in the web project), can retrieve the ContentItem from the HttpContext.Items colection ("ContentItem" key), and display it as desired.

Note that a bare bones sample content controller is included in the Core library. It is intended that you will use a controller built for your specific project.

Classes
-------

* **ContentFilter:** An HTTP module which binds to the BeginRequest event. It request matching content from the ContentRepository, and if such content exists, it stores the ContentItem in the current HttpContext and rewrites the path to the content controller.

* **Configuration:** Parses the "mtNet" section of the web.config.

* **ContentItem:** A representation of a single page or blog entry which has been loaded from the data files.

* **ContentList:** A list of ContentItems. This class is provided to filter unpublished and private content from content collections.

* **ContentRepository:** The main repository for content. Parses the data files and turns them into ContentItems, indexes them by path, then provides querying facilities  to return specific content and content lists.

Configuration
-------------

* **dataPath:** Location to the directory of data files. Required.

* **defaultFileName:** The directory default filename that should be removed from paths (defaults to "index.html").

* **manifestFilename:** The name of the core data file (defaults to "manifest.xml").

* **contentControllerName:** The name of the controller to which the request is rewritten (defaults to "content").

* **controllerActionName:** The name of the action on on the above controller (defaults to "display").

* **disallowedPaths:** A comma-delimited list of paths for which content should be ignored. Use this protect paths to which your application needs to respond. Example, "/content".  Defaults to blank.