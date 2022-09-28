@ECHO OFF 

:: This batch file details details environment variables used for the subsequent Egdewords eCommerce end to end user tests.

TITLE Edgewords eCommerce User Tests

ECHO.

ECHO ===================================================

ECHO             EDGEWORDS ECOMMERCE TESTS

ECHO ===================================================

ECHO.

ECHO Please wait... Checking system information.

ECHO.

:: Section 1: Windows OS information

ECHO     =========================================

ECHO                 WINDOWS OS INFO

ECHO     =========================================

systeminfo | findstr /c:"OS Name"

systeminfo | findstr /c:"OS Version"

systeminfo | findstr /c:"System Type"

ECHO.

:: Section 2: Hardware information.

ECHO     =========================================

ECHO                  HARDWARE INFO

ECHO     =========================================

systeminfo | findstr /c:"Total Physical Memory"

ECHO.

wmic cpu get name

wmic diskdrive get name,model,size

ECHO.

:: Section 3: Run the tests

ECHO     =========================================

ECHO            BUILDING AND RUNNING TESTS

ECHO     =========================================

ECHO Running the tests...

SET email=edgewords@example.com
SET password=aRandomPassword
SET BROWSER=chrome

:: True will take screenshots of the running tests.
SET STEPSCREENSHOT=false

:: Get date in format to use with file name.
SET datestr=%date%
SET dateNow=%datestr:/=-%

:: Get time in format to use with file name.
SET timestr=%time%
SET timeNow=%timestr::=-%
SET timeFileFormat=%timeNow:~0,8%

:: Sets the generated report path to be unique to todays date
SET TESTREPORTSPATH=%cd%\nfocus.dylanwesthead.ecommerceproject\TestReports\TestReport_%dateNow%_%timeFileFormat%.html

:: START /W waits for the program to finish before continuing to execute
cd %cd%..
START /W dotnet test

ECHO Test run complete.

ECHO Compiling test analytics...
cd nFocus.dylanwesthead.ecommerceproject\bin\Debug\net6.0\"
START /W livingdoc test-assembly nfocus.dylanwesthead.ecommerceproject.dll -t TestExecution.json --title "Edgewords eCommerce User Tests - Dylan Westhead" --output %TESTREPORTSPATH%

ECHO Report generated. Launching test report...
START %TESTREPORTSPATH%

ECHO.

PAUSE

