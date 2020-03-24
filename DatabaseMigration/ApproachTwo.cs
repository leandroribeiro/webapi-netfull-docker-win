using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CommandLine;
using DbUp;
using DbUp.Engine;
using DbUp.Helpers;
using DbUp.ScriptProviders;
using DbUp.Support;

namespace DatabaseMigration
{
    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
        public bool Verbose { get; set; }

        [Option('s', "source-scripts", Required = true, HelpText = "Source Scripts")]

        public string SourceScripts { get; set; }

        [Option('c', "connection-string", Required = true, HelpText = "Connection String")]
        public string ConnectionString { get; set; }

        [Option('r', "output-report-path", Required = false, HelpText = "Preview Report Changes")]
        public string PreviewReportPath { get; set; }

        [Option('o', "output-script-path", Required = false, HelpText = "Script with Changes")]
        public string OutputScriptPath { get; set; }
    }

    public class ApproachTwo : ApproachBase
    {
        public int Run(Options args)
        {
            int resultCode = -1;
            
            string connectionString = args.ConnectionString,
                report = args.PreviewReportPath;
                // migrationScriptsPath = Path.Combine(args.SourceScripts, "Migrations"),
                // postMigrationsScriptsPath = Path.Combine(args.SourceScripts, "PostDeployment"),
                // preMigrationsScriptsPath = Path.Combine(args.SourceScripts, "PreDeployment");

            var upgrader = DeployChanges.To
                .SqlDatabase(connectionString, null)
                .WithScriptsFromFileSystem(args.SourceScripts, new FileSystemScriptOptions
                {
                    IncludeSubDirectories = true
                })
                .LogToConsole()
                .JournalToSqlTable("dbo", "MigrationsJournal")
                .Build();

            Console.WriteLine("Is upgrade required? " + upgrader.IsUpgradeRequired());

            if (!string.IsNullOrEmpty(report))
            {
                GenerateReport(report, upgrader);

                GenerateScriptFile(report, upgrader);
                
                resultCode = ShowSuccess();
            }
            else
            {
                var result = upgrader.PerformUpgrade();

                // Display the result
                if (result.Successful)
                {
                    resultCode = ShowSuccess();
                }
                else
                {
                    resultCode = ReturnError(result.Error.ToString());
                    resultCode = ReturnError("Failed!");
                }
            }

            return resultCode;
        } // Run

        private static void GenerateScriptFile(string destinationDir, UpgradeEngine upgrader)
        {
            // Generate full script to apply 
            var scriptPath = Path.Combine(destinationDir, "UpgradeScript.sql");

            Console.WriteLine($"Generating the script {scriptPath}");

            File.WriteAllLines(scriptPath,
                upgrader.GetScriptsToExecute().Select(x => $"-- ### {x.Name}{Environment.NewLine}{x.Contents}"),
                Encoding.UTF8);
        }

        private static void GenerateReport(string destinationDir, UpgradeEngine upgrader)
        {
            var fullReportPath = Path.Combine(destinationDir, "UpgradeReport.html");

            Console.WriteLine($"Generating the report at {fullReportPath}");

            upgrader.GenerateUpgradeHtmlReport(fullReportPath);
        }
    }
}