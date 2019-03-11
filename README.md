# generic-host-windowsservice

Trying out the new Generic host builder in asp.net core 2.1


dotnet publish --configuration Release

sc create MyService binPath= "c:\my_services\aspnetcoreservice\bin\release<TARGET_FRAMEWORK>\publish\aspnetcoreservice.exe" sc start MyService
