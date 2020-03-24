@echo off

set _=%CD%\Report\
set reportFile=%_%\UpgradeReport.html
set scriptFile=%_%\UpgradeScript.sql

REM ################################
REM Limpeza de arquivos
REM ################################

if exist %reportFile% ( 
 del /f %reportFile%
) 

if exist %scriptFile% ( 
 del /f %scriptFile%
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
    --output-report-path=%_%
    

REM ################################
REM Relatorio
REM ################################

echo. 
echo 2/2) Abrindo relatorio no navegador.
echo.

echo Relatorio : %reportFile%
echo   Scripts : %scriptFile%
echo.

start %reportFile%