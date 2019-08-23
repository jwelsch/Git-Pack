using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git_Pack
{
    public class FileCopyResult : CommandResult, IFileCopyResult
    {
        public IList<IFileCopyEntryResult> FileCopyResults
        {
            get;
        }

        public IList<ICreateDirectoryResult> CreateDirectoryResults
        {
            get;
        }

        IEnumerable<IFileCopyEntryResult> IFileCopyResult.FileCopyResults
        {
            get { return this.FileCopyResults; }
        }

        IEnumerable<ICreateDirectoryResult> IFileCopyResult.CreateDirectoryResults
        {
            get { return this.CreateDirectoryResults; }
        }

        public FileCopyResult()
        {
            this.FileCopyResults = new List<IFileCopyEntryResult>();
            this.CreateDirectoryResults = new List<ICreateDirectoryResult>();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            if (this.Error == null)
            {
                var fileCopySuccessCount = this.FileCopyResults.Count(i => i.Error == null);
                var fileCopyFailCount = this.FileCopyResults.Count - fileCopySuccessCount;
                var createDirectorySuccessCount = this.CreateDirectoryResults.Count(i => i.Error == null);
                var createDirectoryFailCount = this.CreateDirectoryResults.Count - createDirectorySuccessCount;

                if (fileCopySuccessCount > 0)
                {
                    builder.AppendLine($"Copied {fileCopySuccessCount} {"file".Pluralize("files", fileCopySuccessCount)} successfully.");
                }

                if (fileCopyFailCount > 0)
                {
                    builder.AppendLine($"Failed to copy {fileCopyFailCount} {"file".Pluralize("files", fileCopyFailCount)}.");
                }

                if (createDirectorySuccessCount > 0)
                {
                    builder.AppendLine($"Created {createDirectorySuccessCount} {"directory".Pluralize("directories", createDirectorySuccessCount)} successfully.");
                }

                if (createDirectoryFailCount > 0)
                {
                    builder.AppendLine($"Failed to create {createDirectoryFailCount} {"directory".Pluralize("directories", createDirectoryFailCount)}.");
                }
            }
            else
            {
                builder.AppendLine($"Failed to copy files.");
                builder.AppendLine($"  {this.Error.Message}");
            }

            return builder.ToString();
        }
    }
}
