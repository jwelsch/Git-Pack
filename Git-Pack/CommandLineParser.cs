﻿using System;
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
                if (args[i].ToLower() == "-r")
                {
                    cla.RepositoryPath = GetDirectoryPath(GetNextArgument(args, i));
                }
                else if (args[i].ToLower() == "-o")
                {
                    cla.OutputDirectoryPath = GetDirectoryPath(GetNextArgument(args, i));
                }
                else if (args[i].ToLower() == "-z")
                {
                    cla.OutputZipPath = GetFilePath(GetNextArgument(args, i));
                }
                else if (args[i].ToLower() == "-b")
                {
                    cla.BranchWithChanges = GetBranchName(GetNextArgument(args, i));
                }
                else if (args[i].ToLower() == "-c")
                {
                    cla.ComparableBranch = GetBranchName(GetNextArgument(args, i));
                }
                else if (args[i].ToLower() == "-overwrite")
                {
                    cla.Overwrite = true;
                    i--;
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

        private string GetDirectoryPath(string arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(nameof(arg), "Path name cannot be null.");
            }

            if (arg.Length == 0)
            {
                throw new ArgumentException(nameof(arg), "Path cannot be empty.");
            }

            return Directory.Exists(arg) ? arg : throw new DirectoryNotFoundException($"Path not found: {arg}");
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