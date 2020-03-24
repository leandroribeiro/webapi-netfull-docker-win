using System;
using System.Collections.Generic;
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
    class Program
    {
        
        static int Main(string[] args)
        {
            var result = CommandLine.Parser.Default
                .ParseArguments<Options>(args)
                .MapResult(
                    (opts) => RunOptionsAndReturnExitCode(opts)
                    , //in case parser sucess
                    errs => HandleParseError(errs)); //in  case parser fail
            
            Console.WriteLine("Return code= {0}", result);
            
            return 0;
        }
        
        //3)	//In sucess: the main logic to handle the options
        static int RunOptionsAndReturnExitCode(Options args)
        {
            return new ApproachTwo().Run(args);
        }

        //in case of errors or --help or --version
        static int HandleParseError(IEnumerable<Error> errs)
        {
            var result = -2;
            
            if (errs.Any(x => x is HelpRequestedError || x is VersionRequestedError))
                result = -1;
            
            return result;
        }

    }
}