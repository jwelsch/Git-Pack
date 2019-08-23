using System.Collections.Generic;

namespace Git_Pack
{
    public interface IFileCopyResult : ICommandResult
    {
        IEnumerable<IFileCopyEntryResult> FileCopyResults
        {
            get;
        }

        IEnumerable<ICreateDirectoryResult> CreateDirectoryResults
        {
            get;
        }
    }
}
