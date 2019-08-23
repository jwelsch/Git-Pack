using System;
using System.Collections.Generic;
using System.IO;

namespace Git_Pack
{
    public class FileCopier
    {
        public FileCopyOperationResult Copy(string baseSourcePath, IEnumerable<string> filePaths, string targetPath, bool overwrite = false, Action<string> reporter = null)
        {
            var operationResult = new FileCopyOperationResult();
            IFileSystemOperationResult result = null;

            var targetFilePath = string.Empty;
            var sourceFilePath = string.Empty;

            foreach (var path in filePaths)
            {
                try
                {
                    var directoryPath = Path.GetDirectoryName(path);
                    var fileName = Path.GetFileName(path);

                    var targetDirectoryPath = Path.Combine(targetPath, directoryPath);

                    if (!Directory.Exists(targetDirectoryPath))
                    {
                        try
                        {
                            Directory.CreateDirectory(targetDirectoryPath);
                            result = new CreateDirectoryResult(targetDirectoryPath);
                            operationResult.CreateDirectorySuccesses.Add(result);
                        }
                        catch (Exception ex)
                        {
                            result = new CreateDirectoryResult(targetDirectoryPath, ex);
                            operationResult.CreateDirectoryErrors.Add(result);
                        }
                        reporter?.Invoke(result.ToString());
                    }

                    targetFilePath = Path.Combine(targetDirectoryPath, fileName);
                    sourceFilePath = Path.Combine(baseSourcePath, path);

                    try
                    {
                        File.Copy(sourceFilePath, targetFilePath, overwrite);
                        result = new FileCopyResult(sourceFilePath, targetFilePath);
                        operationResult.FileCopySuccesses.Add(result);
                    }
                    catch (Exception ex)
                    {
                        result = new FileCopyResult(sourceFilePath, targetFilePath, ex);
                        operationResult.FileCopyErrors.Add(result);
                    }
                    reporter?.Invoke(result.ToString());
                }
                catch (Exception ex)
                {
                    operationResult.GenericErrors.Add(new FileSystemErrorResult(sourceFilePath, targetFilePath, ex));
                }
            }

            return operationResult;
        }
    }
}
