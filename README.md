### Introduction

Project comprise from two appication with enabled seamless data flow between them :

* Desktop App => https://github.com/BartxxxW/SignalRCommunicationsktopClient
* Web App =>  https://github.com/BartxxxW/SignalRCommunication/tree/master

Web App is also contenerized :
* bwitrepo/singalr_server:3 => https://hub.docker.com/repository/docker/bwitrepo/singalr_server/tags 

### Description

Both applications work seamlessly and synchronize data in realtime.
Web App instances sends  data only to Destkop Clients instances and vice-versa , when desktop client sends data it is displayed only on Web App instances.
Desktop Client comprise "Connection Status" label to notify user if app is actually connected.
In case of disconnection  desktop client will try  to reconnects server for severl minutes.



### Build and run both applications.

Prerequisites:
* Installed Docker  =>  https://docs.docker.com/desktop/install/windows-install/
* Installed .Net Runtime  & .Net Sdk=> https://learn.microsoft.com/pl-pl/dotnet/core/install/windows?tabs=net80
* VS or another IDE or just MSBuild
----------------

Web Part :

1. pull docker image =>  docker pull bwitrepo/singalr_server:3
2. run docker image on port 8000 => docker run -d -p 8000:8080  bwitrepo/singalr_server:3
3. Open browser and  enter following url : http://localhost:8000/

Desktop Part :

4. clone  repository : git clone https://github.com/BartxxxW/SignalRCommunicationsktopClient.git
5. with use of Visual Studio open solution and build
6. from solution location go to following directory:  ..\DesktopClient\bin\Debug\net8.0-windows
7. Double Click DesktopClient.exe
8. Run multiple instances of  DesktopCLient.exe 

### Possible errors / lack of connections:
* if data flow does not work properly  please clean cache and cookies of browser
* if there is still lack of connection after few seconds  => please check ApiUrl value in DesktopClient.dll.config - it should match Url of Web App




