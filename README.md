MT.Net
======

MT.Net is a library for publishing an ASP.Net MVC website from data supplied by a Movable Type installation.  This is open-source code licensed under the MIT License.

The project is divided up into four parts:

Core
----
The core libraries that make up MT.Net.  The core project generates a DLL that gets referenced by the implementing website.  See [MT Core doc](https://github.com/MTUS/mt-net/tree/master/Core).

Sample Website
--------------
A sample website implementation which uses Core and the Sample Data. The website parses the MT-supplied data, then implements an HttpModule which intercepts requests and attempts to match them with supplied content. If no supplied content matches the inbound request, the request passes through to be handled normally.

Sample Data
-----------
A set of sample data which would be generated by a Movable Type installation.

Sample Templates
----------------
The templates used by your Movable Type installation to deliver data in a form ingestiable by MT.Net.