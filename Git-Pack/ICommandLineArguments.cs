namespace Git_Pack
{
    public interface ICommandLineArguments
    {
        string RepositoryPath
        {
            get;
        }

        string OutputDirectoryPath
        {
            get;
        }

        string OutputZipPath
        {
            get;
            set;
        }

        string BranchWithChanges
        {
            get;
        }

        string ComparableBranch
        {
            get;
        }

        bool Overwrite
        {
            get;
        }

        string BackupDirectory
        {
            get;
        }
    }
}
