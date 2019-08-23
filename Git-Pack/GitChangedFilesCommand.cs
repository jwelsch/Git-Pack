using System.Collections.Generic;
using System.Diagnostics;

namespace Git_Pack
{
    public class GitChangedFilesCommand
    {
        public IList<string> Execute(string gitExePath, string repositoryPath, string branchWithChanges, string comparableBranch)
        {
            var list = new List<string>();

            // git command to get changed files:
            // git diff --name-only <branch-to-compare-against>...<branch-with-changes>
            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName = gitExePath,
                UseShellExecute = false,
                Arguments = $"diff --name-only {comparableBranch}...{branchWithChanges}",
                WorkingDirectory = repositoryPath,
                RedirectStandardOutput = true
            };

            using (var process = new Process() { StartInfo = startInfo })
            {
                var success = process.Start();
                process.WaitForExit();

                while (!process.StandardOutput.EndOfStream)
                {
                    var line = process.StandardOutput.ReadLine();
                    list.Add(line.Replace("/", "\\"));
                }
            }

            return list;
        }
    }
}
