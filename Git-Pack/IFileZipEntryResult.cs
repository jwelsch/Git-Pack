namespace Git_Pack
{
    public interface IFileZipEntryResult : ICommandResult
    {
        string SourcePath
        {
            get;
        }

        string ZipEntry
        {
            get;
        }
    }
}
