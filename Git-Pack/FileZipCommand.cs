using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Git_Pack
{
    public class FileZipCommand : ICommand
    {
        public string BaseSourcePath
        {
            get;
        }

        public IEnumerable<string> FilePaths
        {
            get;
        }

        public string TargetZipPath
        {
            get;
        }

        public bool Overwrite
        {
            get;
        }

        public FileZipCommand(string baseSourcePath, IEnumerable<string> filePaths, string targetZipPath, bool overwrite = false)
        {
            this.BaseSourcePath = baseSourcePath;
            this.FilePaths = filePaths;
            this.TargetZipPath = targetZipPath;
            this.Overwrite = overwrite;
        }

        public ICommandResult Execute(Action<string> reporter = null)
        {
            var fileZipResult = new FileZipResult
            {
                ZipFilePath = this.TargetZipPath
            };

            try
            {
                using (var zipFile = new FileStream(this.TargetZipPath, this.Overwrite ? FileMode.Create : FileMode.CreateNew, FileAccess.Write, FileShare.Read))
                {
                    reporter?.Invoke($"Created ZIP file: {this.TargetZipPath}");

                    using (var zipArchive = new ZipArchive(zipFile, ZipArchiveMode.Create, true))
                    {
                        var count = this.FilePaths.Count();
                        var i = 0;

                        foreach (var path in this.FilePaths)
                        {
                            var sourcePath = Path.Combine(this.BaseSourcePath, path);

                            var entryResult = new FileZipEntryResult
                            {
                                SourcePath = sourcePath,
                                ZipEntry = path
                            };

                            try
                            {
                                zipArchive.CreateEntryFromFile(sourcePath, path, CompressionLevel.Fastest);
                            }
                            catch (Exception ex)
                            {
                                entryResult.Error = ex;
                            }

                            fileZipResult.ZipEntryResults.Add(entryResult);
                            reporter?.Invoke($"[{++i}/{count}] " + entryResult.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                fileZipResult.Error = ex;
            }

            return fileZipResult;
        }
    }
}
