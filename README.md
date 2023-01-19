# IridiUploadConsole
 Iridi Upload (Console Version)
This program allows you to update existing Iridi projects on the Iridi cloud. Then update the project on the server, like a UMC C3.


#build
Entry point:
Program.cs static int Main(string[] args)

Dependencies:
Packets:
.nuget\packages\commandlinetool\1.0.0\
.nuget\packages\newtonsoft.json\13.0.2\

Platform:
dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\


#Console commands.
You can use console commands. To show all use --help command.

#Example. 


Add new transfer:

IU >add


Enter login and pass at iridi:

IU >ilogin iridi_login

IU >ipass iridi_pass


Show projects at iridi.com:

IU >iprojects


Select smt iridi project:

IU >iset 8


Set file path:

IU >filepath D:\projects\Iridi\Test.sirpz


Upload file:

IU >iupload


Enter IP and pass at server:

IU >sip 192.168.1.19

IU >spass server_pass


Show projects at server:

IU >sprojects


Select smt server project:
IU >sset 1


Download project at server:
IU >sdownload



#Arguments
You can use console arguments.


arguments:

args[0] "get_iridi_projects"

args[1] login 

args[2] pass


response:

Iridi projects

[Count]

[Folder name]

[Object name]

[Project name]

[Folder name]

[Object name]

[Project name]

...


arguments:
args[0] "get_server_projects"
args[1] IP 
args[2] pass

response:
Iridi projects
[Count]
[Object name]
[Project name]
[Object name]
[Project name]
...


arguments:
args[0] "upload"
args[1] login 
args[2] pass
args[3] folder
args[4] object 
args[5] project
args[6] file_path

response:
result_code


arguments:
args[0] "upload_and_download"
args[1] iridi_login 
args[2] iridi_pass
args[3] iridi_folder
args[4] iridi_object 
args[5] project
args[6] file_path
args[7] server_IP
args[8] server_pass 
args[9] server_object
args[10] server_project

response:
result_code


arguments:
args[0] "upload_many"
args[1] {count of uploads} 
args[2] login 
args[3] pass
args[4] folder
args[5] object 
args[6] project
args[7] file_path
args[8] login 
args[9] pass
args[10] folder
args[11] object 
args[12] project
args[13] file_path
...

response:
result_code


arguments:
args[0] "upload_and_download_many"
args[1] {count of uploads}
args[2] {if download at server: 1; else 0;}
args[3] iridi_login 
args[4] iridi_pass
args[5] iridi_folder
args[6] iridi_object 
args[7] iridi_project
args[8] file_path
args[9] {server_IP}
args[10] {server_pass}
args[11] {server_object}
args[12] {server_project}

response:
result_code

example: upload_and_download_many 2 0 "iridi_login" "iridi_pass" "iridi_folder" "iridi_object" "iridi_project" file_path 1 "iridi_login" "iridi_pass" "iridi_folder" "iridi_object" "iridi_project" file_path "10.16.1.19" "server_pass" "server_object" "server_project"


#result_codes:
//Operation completed successfully
Successfully = 1

//Unable to access resource iridi.com
IridiUnabledAccess = 10

//Authentication to iridi.com failed
IridiUnabledAuthentication = 11

//Can't find project on iridi.com
IridiCantFindProject = 12

//Data is not filled for iridi.com
IridiDataIsNotFilled = 13

//Upload file failed at iridi.com
IridiUploadFileFailed = 14

//Wrong line project at iridi.com
IridiWrongLineProject = 15

//Unknown error on iridi.com
IridiUnknownError = 19


//Cannot access on server
ServerUnabledAccess = 20

//Authentication to server failed
ServerUnabledAuthentication = 21

//Can't find project on server
ServerCantFindProject = 22

//Data is not filled for server
ServerDataIsNotFilled = 23

//Download file at server failed 
ServerDownloadFileFailed = 24

//Wrong line project at server
ServerWrongLineProject = 25

//Unknown error on server
ServerUnknownError = 29


//Invalid file path
InvalidFilePath = 30

//Wrong args
WrongConsoleArgs = 40
