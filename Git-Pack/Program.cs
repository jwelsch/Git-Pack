using System;
using System.Diagnostics;

namespace Git_Pack
{
    class Program
    {
        //
        // Command line syntax
        //
        // Git-Pack -r "\directory\with\repo" (-d "\output\directory" | -f "\output\zip\file") -b "branch-with-changes" -c "branch-to-compare-against" [-overwrite] [-u "\back\up\directory"]
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
    }
}
