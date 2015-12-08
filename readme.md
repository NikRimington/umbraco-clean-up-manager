# RB: Umbraco CleanUp Manager

The Umbraco CleanUp Manager allows the cleansing of orphan Data and Document Types within an Umbraco application.
It displays all orphan records in a paged table, which allows to navigate to, filter, and delete individual records of all at the same time.


## Installation

The Domain Manager package can be installed via the package's page on [our.umbraco.org](https://our.umbraco.org/member/127929) or via NuGet. If installing via NuGet, use the following package manager command:

    Install-Package RB.Umbraco.CleanUpManager

## Contributing



## Publishing

Remember to include all necessary files within the package.xml file. Run the following script, entering the new version number when prompted to created a published version of the package:

    Build\Release.bat

The release script will amend all assembly versions for the package, build the solution and create the package file. The script will also commit and tag the repository accordingly to reflect the new version.