# ChocoCup
ChocoCup is a command-line tool whose objective is to generate a PowerShell script which installs all of the packages currently
installed on your system. Essentialy, it's a tool for backing up your current [Chocolatey](https://github.com/chocolatey/choco) package library. This tool is designed to work 
with Chocolatey version **0.9.9.6**, however it should work with other versions too.

# Usage

` chococup [<options>] `

# Options

	-p
	Print - print the genarated script to the console.
	
	-i
	Ignore Version - ignore the version with which the tool is designed to work with and generate the script even
	if the Chocolatey and the target version of ChocoCup don't match.
	
	-v
	Include Version - include the version of the packages installed in your machine in the backup script. For example,
	if you have v1.2.3 of a certain package installed on your machine, the backup script will download exactly that version
	(even if there is a newever version available). By default, the newest version of every package will be installed.
	
	-c VALUE
	Chocolatey Path - the default Chocolatey path is C:\ProgramData\chocolatey\bin\choco.exe. This option allows to specify
	a different path for the executable. For example, if Chocolatey is installed in C:\chocolatey\ on your system, you would run
	"choco -c C:\chocolatey\bin\choco.exe".
	
	-f VALUE
	Outup File Path - specify the output file path for the genarated script. This will prevent the tool from asking for the output
	file path at the end. Include full path to the file, as well as its extension. Example usage: "choco -f C:\Users\John\Desktop\script.ps1".

For example, if your Chocolatey executable is located at `C:\chocolatey\bin\choco.exe`, you want the output script to be saved at
`C:\Users\John\script.ps1` and you also want the script printed to the console window, you would run (the arguments can be in any order):

	choco -c C:\chocolatey\bin\choco.exe -f C:\Users\John\script.ps1 -p
	
# FAQ

**1. Does The Tool Require Admin Privileges When Run?**

No.