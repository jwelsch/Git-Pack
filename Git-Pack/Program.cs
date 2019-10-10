using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Xml;
using System;
using System.Diagnostics;
using System.IO;

namespace Git_Pack
{
    class Program
    {
        //
        // Command line syntax
        //
        // Git-Pack -r "\directory\with\repo" (-d "\output\directory" | -f "\output\zip\file") -b "branch-with-changes" -c "branch-to-compare-against" [-overwrite] [-u "\back\up\directory"]
        //

        private static readonly string DefaultGitExePath = $"Git{Path.DirectorySeparatorChar}Bin{Path.DirectorySeparatorChar}git.exe";

        static void Main(string[] args)
        {
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddXmlFile("App.config")
                    .Build();

                var gitExePath = GetFullGitExePath(config);

                var parser = new CommandLineParser();
                var cla = parser.Parse(args);

                var gitCommand = new GitChangedFilesCommand();
                var changedFileList = gitCommand.Execute(gitExePath, cla.RepositoryPath, cla.BranchWithChanges, cla.ComparableBranch);

                Console.WriteLine($"Found {changedFileList.Count} changed {"file".Pluralize("files", changedFileList.Count)}.");
                Console.WriteLine();

                ICommand command = null;

                if (!string.IsNullOrWhiteSpace(cla.OutputDirectoryPath))
                {
                    command = new FileCopyCommand(cla.RepositoryPath, changedFileList, cla.OutputDirectoryPath, cla.BackupDirectory, cla.Overwrite);
                }
                else if (!string.IsNullOrWhiteSpace(cla.OutputZipPath))
                {
                    command = new FileZipCommand(cla.RepositoryPath, changedFileList, cla.OutputZipPath, cla.Overwrite);
                }

                var result = command.Execute(s => Console.WriteLine(s));

                Console.WriteLine();
                Console.WriteLine(result.ToString());
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

        private static string GetFullGitExePath(IConfigurationRoot config)
        {
            var gitExePath = config["gitPath"];

            if (string.IsNullOrWhiteSpace(gitExePath))
            {
                gitExePath = DefaultGitExePath;
            }

            return FileResolver.Find(gitExePath);
        }
    }
}
