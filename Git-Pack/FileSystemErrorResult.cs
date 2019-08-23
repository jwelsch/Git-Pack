using System;

namespace Git_Pack
{
    public class FileSystemErrorResult : FileSystemOperationResult
    {
        public string SourcePath
        {
            get;
        }

        public string TargetPath
        {
            get;
        }

        public FileSystemErrorResult(string sourcePath, string targetPath, Exception error)
            : base(error == null ? error : new ArgumentNullException(nameof(error)))
        {
            this.SourcePath = sourcePath;
            this.TargetPath = targetPath;
        }

        public override string ToString()
        {
            return $"Error while processing file to copy: {this.SourcePath}{Environment.NewLine}  {this.Error.Message}";
        }
    }
}
