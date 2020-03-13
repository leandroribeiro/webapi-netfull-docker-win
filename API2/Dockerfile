# escape=`
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS builder
WORKDIR /app
COPY API2/. .
RUN nuget restore -PackagesDirectory ../packages
# RUN msbuild API1.csproj /p:OutputPath=c:\out `
#                                      /t:restore
RUN msbuild API2.csproj /p:Configuration=Debug `
                                      /p:DeployOnBuild=True `
                                      /p:DeployDefaultTarget=WebPublish `
                                      /p:WebPublishMethod=FileSystem `
                                      /p:DeleteExistingFiles=True `
                                      /p:publishUrl=c:\out

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8-windowsservercore-ltsc2019
WORKDIR c:/inetpub/wwwroot
COPY --from=builder c:/out .