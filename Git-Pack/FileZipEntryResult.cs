using System;

namespace Git_Pack
{
    public class FileZipEntryResult : CommandResult, IFileZipEntryResult
    {
        public string SourcePath
        {
            get;
            set;
        }

        public string ZipEntry
        {
            get;
            set;
        }

        public override string ToString()
        {
            if (this.Error == null)
            {
                return $"Added file ZIP: {this.SourcePath}";
            }
            else
            {
                return $"Failed to add file to ZIP: {this.SourcePath}{Environment.NewLine}  {this.Error.Message}";
            }
        }
    }
}
