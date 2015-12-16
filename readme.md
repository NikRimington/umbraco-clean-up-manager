# RB: Umbraco CleanUp Manager

The Umbraco CleanUp Manager allows the cleansing of orphan Data and Document Types within an Umbraco application.
It displays all orphan records in a paged table, which allows to navigate to, filter, and delete individual records, or, all at the same time.
Note - Always backup your umbraco database before deleting data or document types - there is no "Undo" after an item has been deleted. 

## Installation

The RB Umbraco CleanUp Manager package can be installed via the package's page on [our.umbraco.org](https://our.umbraco.org/member/127929) or via NuGet (https://www.nuget.org/packages/RB.Umbraco.CleanUpManager/1.0.0). 
If installing via NuGet, use the following package manager command:

    Install-Package RB.Umbraco.CleanUpManager

## Publishing

Remember to include all necessary files within the package.xml file. Run the following script, entering the new version number when prompted to created a published version of the package:

    Build\Release.bat

The release script will amend all assembly versions for the package, build the solution and create the package file. The script will also commit and tag the repository accordingly to reflect the new version.

#Licensing Agreement 

The MIT License (MIT)
Copyright (c) 2015 - Reckitt Benckiser(RB)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
