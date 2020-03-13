# escape=`
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS builder
WORKDIR c:/Projects/Labs/Lab.NETFullwithDocker/
COPY . .
RUN msbuild Lab.NETFullwithDocker.sln /p:OutputPath=c:\out `
                                      /t:restore
RUN msbuild Lab.NETFullwithDocker.sln /p:Configuration=Debug `
                                      /p:DeployOnBuild=True `
                                      /p:DeployDefaultTarget=WebPublish `
                                      /p:WebPublishMethod=FileSystem `
                                      /p:DeleteExistingFiles=True `
                                      /p:publishUrl=c:\out

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2019
WORKDIR c:/inetpub/wwwroot
COPY --from=builder c:/out .