@echo off

set _=%CD%

REM ################################
REM Limpeza de arquivos
REM ################################

if exist %_%\UpgradeReport.html ( 
 del /f %_%\UpgradeReport.html
) 

if exist %_%\script.sql ( 
 del /f %_%\script.sql
)

REM ################################
REM Execucao do programa gerador
REM ################################

echo.
echo 1/2) Gerando relatorio de scripts pendentes de aplicacao.
echo.

dotnet .\bin\Debug\netcoreapp3.1\DatabaseMigration.dll ^
    --source-scripts="D:\\PROJECTS\\github\\webapi-netfull-docker-win\\DatabaseMigration\\Scripts\\" ^
    --connection-string="Data Source=localhost,5433;Initial Catalog=TodoDB;Persist Security Info=True;User Id=sa;Password=Pass@word;MultipleActiveResultSets=True;App=DatabaseMigration" ^
    --output-report-path="D:\\PROJECTS\\github\\webapi-netfull-docker-win\\DatabaseMigration\\"
    

REM ################################
REM Relatorio
REM ################################

echo. 
echo 2/2) Abrindo relatorio no navegador.
echo.

echo Relatorio : %_%\UpgradeReport.html
echo   Scripts : %_%\script.sql
echo.

start %_%\UpgradeReport.html