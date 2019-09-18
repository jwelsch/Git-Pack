using System;

namespace Git_Pack
{
    public class FileCopyEntryResult : CommandResult, IFileCopyEntryResult
    {
        public string SourcePath
        {
            get;
            set;
        }

        public string TargetPath
        {
            get;
            set;
        }

        public string BackupPath
        {
            get;
            set;
        }

        public override string ToString()
        {
            if (this.Error == null)
            {
                return $"Copied file to: {this.TargetPath}";
            }
            else
            {
                return $"Failed to copy file: {this.TargetPath}{Environment.NewLine}  {this.Error.Message}";
            }
        }
    }
}
