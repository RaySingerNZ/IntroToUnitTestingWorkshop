# Intro to Unit Testing Workshop in Visual Studio

## About
This repo contains the files used for the Intro to Unit Testing Workshop presented by John Wang and Ray Singer at CPP.

## Goal of Workshop
To show what unit testing is, why it is so helpful and how you can use it in your own projects. 
Get you started with unit testing, and show how you can use unit testing to your advantage in CPP's VP1 project.

## Goal of Files
The files are designed to show how testing can be implemented against the VP1 project's
datalayer, to check for a sound SQL connection and that the data returned is correct.

## How to Use This Project
If you have a GitHub account you can fork this repository, else you can download the ZIP file.

1. **Once you have a copy of this project, first restore the database.**
  1. Open SQL Management Studio, expand the *Database Engine*, select the server inside, right click and connect to it.
  2. Right click on the *Databases* folder in the Object Explorer and select the *Restore Database* option.
  3. In the dialog box select the *Device* under *Source*, click the ellipsis, ensure *Backup Media Type* is selected as *File* and click on the *Add* button.
  4. Navigate to the *BadassGangDB.bak* file location and click OK to restore the database.
2. **Now you can load up the solution in Visual Studio.**
  1. With the solution open, reveal the *Server Explorer* panel in VS (Visual Studio). Right click on Data Connections, hit *Refresh*. Right click again and select *Add Connection*.
  2. Under *Server Name* enter *localhost*, this should populate the *Connect to a database* combobox.
  3. Select *BadassGangDB* and hit OK.
3. **With the database connected you can now run the tests**
  1. Navigate to Test > Run > All Tests.
  2. Alternatively if you wish to debug you tests by positioning breakpoints in them, 
  select Test > Debug > All Tests.
