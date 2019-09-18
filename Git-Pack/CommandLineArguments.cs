namespace Git_Pack
{
    public class CommandLineArguments : ICommandLineArguments
    {
        public string RepositoryPath
        {
            get;
            set;
        }

        public string OutputDirectoryPath
        {
            get;
            set;
        }

        public string OutputZipPath
        {
            get;
            set;
        }

        public string BranchWithChanges
        {
            get;
            set;
        }

        public string ComparableBranch
        {
            get;
            set;
        }

        public bool Overwrite
        {
            get;
            set;
        }

        public string BackupDirectory
        {
            get;
            set;
        }

        public CommandLineArguments()
        {

        }

        public CommandLineArguments(string repositoryDirectory, string outputPath, bool isOutputDirectory, string branchWithChanges, string comparableBranch, bool overwrite, string backupDirectory)
        {
            this.RepositoryPath = repositoryDirectory;
            this.OutputDirectoryPath = isOutputDirectory ? outputPath : string.Empty;
            this.OutputZipPath = !isOutputDirectory ? outputPath : string.Empty;
            this.BranchWithChanges = branchWithChanges;
            this.ComparableBranch = comparableBranch;
            this.Overwrite = overwrite;
            this.BackupDirectory = backupDirectory;
        }
    }
}
