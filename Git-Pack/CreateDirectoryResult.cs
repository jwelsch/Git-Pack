using System;

namespace Git_Pack
{
    public class CreateDirectoryResult : FileSystemOperationResult
    {
        public string DirectoryPath
        {
            get;
        }

        public CreateDirectoryResult(string directoryPath, Exception error = null)
            : base(error)
        {
            this.DirectoryPath = directoryPath;
        }

        public override string ToString()
        {
            if (this.Error == null)
            {
                return $"Created directory: {this.DirectoryPath}";
            }
            else
            {
                return $"Failed to create directory: {this.DirectoryPath}{Environment.NewLine}  {this.Error.Message}";
            }
        }
    }
}
