using System;

namespace Git_Pack
{
    public class FileSystemOperationResult : IFileSystemOperationResult
    {
        public Exception Error
        {
            get;
        }

        public FileSystemOperationResult(Exception error)
        {
            this.Error = error;
        }
    }
}
