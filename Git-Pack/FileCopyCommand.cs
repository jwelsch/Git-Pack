using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Git_Pack
{
    public class FileCopyCommand : ICommand
    {
        public string BaseSourcePath
        {
            get;
        }

        public IEnumerable<string> FilePaths
        {
            get;
        }

        public string TargetPath
        {
            get;
        }

        public bool Overwrite
        {
            get;
        }

        public FileCopyCommand(string baseSourcePath, IEnumerable<string> filePaths, string targetPath, bool overwrite = false)
        {
            this.BaseSourcePath = baseSourcePath;
            this.FilePaths = filePaths;
            this.TargetPath = targetPath;
            this.Overwrite = overwrite;
        }

        public ICommandResult Execute(Action<string> reporter = null)
        {
            var operationResult = new FileCopyResult();

            try
            {
                var targetFilePath = string.Empty;
                var sourceFilePath = string.Empty;
                var count = this.FilePaths.Count();
                var i = 0;

                foreach (var path in this.FilePaths)
                {
                    var directoryPath = Path.GetDirectoryName(path);
                    var fileName = Path.GetFileName(path);

                    var targetDirectoryPath = Path.Combine(this.TargetPath, directoryPath);

                    if (!Directory.Exists(targetDirectoryPath))
                    {
                        var createDirectoryResult = new CreateDirectoryResult
                        {
                            DirectoryPath = directoryPath
                        };

                        try
                        {
                            Directory.CreateDirectory(targetDirectoryPath);
                        }
                        catch (Exception ex)
                        {
                            createDirectoryResult.Error = ex;
                        }

                        operationResult.CreateDirectoryResults.Add(createDirectoryResult);
                        reporter?.Invoke(createDirectoryResult.ToString());
                    }

                    targetFilePath = Path.Combine(targetDirectoryPath, fileName);
                    sourceFilePath = Path.Combine(this.BaseSourcePath, path);

                    var fileCopyEntryResult = new FileCopyEntryResult
                    {
                        SourcePath = sourceFilePath,
                        TargetPath = targetFilePath
                    };

                    try
                    {
                        File.Copy(sourceFilePath, targetFilePath, this.Overwrite);
                    }
                    catch (Exception ex)
                    {
                        fileCopyEntryResult.Error = ex;
                    }

                    operationResult.FileCopyResults.Add(fileCopyEntryResult);
                    reporter?.Invoke($"[{++i}/{count}] " + fileCopyEntryResult.ToString());
                }
            }
            catch (Exception ex)
            {
                operationResult.Error = ex;
            }

            return operationResult;
        }
    }
}
