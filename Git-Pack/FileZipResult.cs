using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git_Pack
{
    public class FileZipResult : CommandResult, IFileZipResult
    {
        public string ZipFilePath
        {
            get;
            set;
        }

        IEnumerable<IFileZipEntryResult> IFileZipResult.ZipEntryResults
        {
            get { return this.ZipEntryResults; }
        }

        public IList<IFileZipEntryResult> ZipEntryResults
        {
            get;
        }

        public FileZipResult()
        {
            this.ZipEntryResults = new List<IFileZipEntryResult>();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            if (this.Error == null)
            {
                var zipEntrySuccessCount = this.ZipEntryResults.Count(i => i.Error == null);
                var zipEntryFailCount = this.ZipEntryResults.Count - zipEntrySuccessCount;

                if (zipEntrySuccessCount > 0)
                {
                    builder.AppendLine($"Added {zipEntrySuccessCount} {"file".Pluralize("files", zipEntrySuccessCount)} to the ZIP file successfully.");
                }

                if (zipEntryFailCount > 0)
                {
                    builder.AppendLine($"Failed to add {zipEntryFailCount} {"file".Pluralize("files", zipEntryFailCount)} to the ZIP file.");
                }
            }
            else
            {
                builder.AppendLine($"Failed to create ZIP file.");
                builder.AppendLine($"  {this.Error.Message}");
            }

            return builder.ToString();
        }
    }
}
