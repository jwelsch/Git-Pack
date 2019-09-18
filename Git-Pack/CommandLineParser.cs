using System;
using System.IO;

namespace Git_Pack
{
    public class CommandLineParser
    {
        public ICommandLineArguments Parse(string[] args)
        {
            var cla = new CommandLineArguments();

            for (var i = 0; i < args.Length; i+=2)
            {
                var arg = args[i].ToLower();

                if (arg == "-r" || arg == "--repository")
                {
                    cla.RepositoryPath = GetDirectoryPath(GetNextArgument(args, i), true);
                }
                else if (arg == "-d" || arg == "--output-directory")
                {
                    cla.OutputDirectoryPath = GetDirectoryPath(GetNextArgument(args, i), false);
                }
                else if (arg == "-f" || arg == "--zip-file")
                {
                    cla.OutputZipPath = GetFilePath(GetNextArgument(args, i));
                }
                else if (arg == "-b" || arg == "--branch")
                {
                    cla.BranchWithChanges = GetBranchName(GetNextArgument(args, i));
                }
                else if (arg == "-c" || arg == "--comparable-branch")
                {
                    cla.ComparableBranch = GetBranchName(GetNextArgument(args, i));
                }
                else if (arg == "-o" || arg == "--overwrite")
                {
                    cla.Overwrite = true;
                    i--;
                }
                else if (arg == "-u" || arg == "--backup")
                {
                    cla.BackupDirectory = GetDirectoryPath(GetNextArgument(args, i), false);
                }
                else
                {
                    throw new ArgumentException($"Unknown command line argument found: {args[i]}", nameof(args));
                }
            }

            if (string.IsNullOrWhiteSpace(cla.RepositoryPath))
            {
                throw new ArgumentException($"Missing repository path argument.", nameof(args));
            }

            if (string.IsNullOrWhiteSpace(cla.OutputDirectoryPath) && string.IsNullOrWhiteSpace(cla.OutputZipPath))
            {
                throw new ArgumentException($"Missing output directory or output zip path argument.", nameof(args));
            }

            if (string.IsNullOrWhiteSpace(cla.BranchWithChanges))
            {
                throw new ArgumentException($"Missing name of branch with changes argument.", nameof(args));
            }

            if (string.IsNullOrWhiteSpace(cla.ComparableBranch))
            {
                throw new ArgumentException($"Missing name of branch to compare against argument.", nameof(args));
            }

            return cla;
        }

        private string GetNextArgument(string[] args, int index)
        {
            var newIndex = index + 1;

            if (newIndex >= args.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(args), $"Not enough command line arguments found.");
            }

            return args[newIndex];
        }

        private string GetDirectoryPath(string arg, bool mustExist)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(nameof(arg), "Path name cannot be null.");
            }

            if (arg.Length == 0)
            {
                throw new ArgumentException(nameof(arg), "Path cannot be empty.");
            }

            return mustExist && !Directory.Exists(arg) ? throw new DirectoryNotFoundException($"Path not found: {arg}") : arg;
        }

        private string GetFilePath(string arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(nameof(arg), "Path name cannot be null.");
            }

            if (arg.Length == 0)
            {
                throw new ArgumentException(nameof(arg), "Path cannot be empty.");
            }

            return arg;
        }

        private string GetBranchName(string arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(nameof(arg), "Branch name cannot be null.");
            }

            if (arg.Length == 0)
            {
                throw new ArgumentException(nameof(arg), "Branch name cannot be empty.");
            }

            return arg;
        }
    }
}
