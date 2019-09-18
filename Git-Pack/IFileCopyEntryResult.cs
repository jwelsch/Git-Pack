namespace Git_Pack
{
    public interface IFileCopyEntryResult : ICommandResult
    {
        string SourcePath
        {
            get;
        }

        string TargetPath
        {
            get;
        }

        string BackupPath
        {
            get;
        }
    }
}
