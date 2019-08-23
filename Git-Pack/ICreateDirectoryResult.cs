namespace Git_Pack
{
    public interface ICreateDirectoryResult : ICommandResult
    {
        string DirectoryPath
        {
            get;
        }
    }
}
