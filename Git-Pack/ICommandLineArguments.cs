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
    }
}
