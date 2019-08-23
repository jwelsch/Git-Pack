using System.Collections.Generic;

namespace Git_Pack
{
    public interface IFileZipResult : ICommandResult
    {
        string ZipFilePath
        {
            get;
        }

        IEnumerable<IFileZipEntryResult> ZipEntryResults
        {
            get;
        }
    }
}
