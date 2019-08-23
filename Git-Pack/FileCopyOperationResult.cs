using System.Collections.Generic;

namespace Git_Pack
{
    public class FileCopyOperationResult
    {
        public IList<IFileSystemOperationResult> FileCopySuccesses
        {
            get;
        }

        public IList<IFileSystemOperationResult> FileCopyErrors
        {
            get;
        }

        public IList<IFileSystemOperationResult> CreateDirectorySuccesses
        {
            get;
        }

        public IList<IFileSystemOperationResult> CreateDirectoryErrors
        {
            get;
        }

        public IList<IFileSystemOperationResult> GenericErrors
        {
            get;
        }

        public FileCopyOperationResult()
        {
            this.FileCopySuccesses = new List<IFileSystemOperationResult>();
            this.FileCopyErrors = new List<IFileSystemOperationResult>();
            this.CreateDirectorySuccesses = new List<IFileSystemOperationResult>();
            this.CreateDirectoryErrors = new List<IFileSystemOperationResult>();
            this.GenericErrors = new List<IFileSystemOperationResult>();
        }
    }
}
