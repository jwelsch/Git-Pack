using System;
using System.Diagnostics;

namespace Git_Pack
{
    class Program
    {
        //
        // Command line syntax
        //
        // Git-Pack -r "\directory\with\repo" (-o "\output\directory" | -z "\output\zip\file") -b "branch-with-changes" -c "branch-to-compare-against" [-overwrite]
        //

        private static readonly string GitBinFolderName = @"Git\Bin";
        private static readonly string GitExeName = "git.exe";

        static void Main(string[] args)
        {
            try
            {
                var parser = new CommandLineParser();
                var cla = parser.Parse(args);

                var gitExePath = FileResolver.FindInProgramFiles(GitBinFolderName, GitExeName);

                var command = new GitChangedFilesCommand();
                var changedFileList = command.Execute(gitExePath, cla.RepositoryPath, cla.BranchWithChanges, cla.ComparableBranch);

                Console.WriteLine($"Found {changedFileList.Count} changed files.{Environment.NewLine}");

                var copier = new FileCopier();
                var result = copier.Copy(cla.RepositoryPath, changedFileList, cla.OutputDirectoryPath, cla.Overwrite, s => Console.WriteLine(s));

                Console.WriteLine();

                if (result.FileCopySuccesses.Count > 0)
                {
                    Console.WriteLine($"Copied {result.FileCopySuccesses.Count} files successfully.");
                }

                if (result.FileCopyErrors.Count > 0)
                {
                    Console.WriteLine($"Failed to copy {result.FileCopyErrors.Count} files.");
                }

                if (result.CreateDirectorySuccesses.Count > 0)
                {
                    Console.WriteLine($"Created {result.CreateDirectorySuccesses.Count} directories successfully.");
                }

                if (result.CreateDirectoryErrors.Count > 0)
                {
                    Console.WriteLine($"Failed to create {result.CreateDirectoryErrors.Count} directories.");
                }

                if (result.GenericErrors.Count > 0)
                {
                    Console.WriteLine($"Encountered {result.GenericErrors.Count} other errors.");
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
