using System;

namespace Git_Pack
{
    public class FileCopyResult : FileSystemOperationResult
    {
        public string SourcePath
        {
            get;
        }

        public string TargetPath
        {
            get;
        }

        public FileCopyResult(string sourcePath, string targetPath, Exception error = null)
            : base(error)
        {
            this.SourcePath = sourcePath;
            this.TargetPath = targetPath;
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
