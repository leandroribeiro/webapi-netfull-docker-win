using System;
using System.IO;
using DbUp;
using DbUp.Helpers;
using DbUp.ScriptProviders;

namespace DatabaseMigration
{
    public class ApproachOne : ApproachBase
    {
        public int Run(string[] args)
        {
            if (args.Length != 2)
            {
                return ReturnError(
                    "Invalid args. You have to specify connection string and scripts path");
            }

            var connectionString = args[0];
            var scriptsPath = args[1];

            Console.WriteLine("Start executing predeployment scripts...");
            string preDeploymentScriptsPath = Path.Combine(scriptsPath, "PreDeployment");
            var preDeploymentScriptsExecutor =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsFromFileSystem(preDeploymentScriptsPath, new FileSystemScriptOptions
                    {
                        IncludeSubDirectories = true
                    })
                    .LogToConsole()
                    .JournalTo(new NullJournal())
                    .Build();

            var preDeploymentUpgradeResult = preDeploymentScriptsExecutor.PerformUpgrade();

            if (!preDeploymentUpgradeResult.Successful)
            {
                return ReturnError(preDeploymentUpgradeResult.Error.ToString());
            }

            ShowSuccess();

            Console.WriteLine("Start executing migration scripts...");
            var migrationScriptsPath = Path.Combine(scriptsPath, "Migrations");
            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsFromFileSystem(migrationScriptsPath, new FileSystemScriptOptions
                    {
                        IncludeSubDirectories = true
                    })
                    .LogToConsole()
                    .JournalToSqlTable("app", "MigrationsJournal")
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                return ReturnError(result.Error.ToString());
            }

            ShowSuccess();

            Console.WriteLine("Start executing postdeployment scripts...");
            string postdeploymentScriptsPath = Path.Combine(scriptsPath, "PostDeployment");
            var postDeploymentScriptsExecutor =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsFromFileSystem(postdeploymentScriptsPath, new FileSystemScriptOptions
                    {
                        IncludeSubDirectories = true
                    })
                    .LogToConsole()
                    .JournalTo(new NullJournal())
                    .Build();

            var postdeploymentUpgradeResult = postDeploymentScriptsExecutor.PerformUpgrade();

            if (!postdeploymentUpgradeResult.Successful)
            {
                return ReturnError(result.Error.ToString());
            }

            ShowSuccess();

            return 0;
        }
    }
}