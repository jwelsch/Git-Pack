using System;

namespace Git_Pack
{
    public class CreateDirectoryResult : CommandResult, ICreateDirectoryResult
    {
        public string DirectoryPath
        {
            get;
            set;
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
