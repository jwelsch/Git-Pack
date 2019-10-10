using System;
using System.IO;
using System.Text;

namespace Git_Pack
{
    public static class FileResolver
    {
        public static string Find(params string[] paths)
        {
            var pathBuilder = new StringBuilder();

            foreach (var p in paths)
            {
                var partial = Path.Combine(pathBuilder.ToString(), p);
                pathBuilder.Clear();
                pathBuilder.Append(partial);
            }

            var path = pathBuilder.ToString();
            var exists = File.Exists(path);

            if (!exists)
            {
                if (!Path.IsPathRooted(path))
                {
                    var absolutePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), path);

                    exists = File.Exists(absolutePath);

                    if (exists)
                    {
                        path = absolutePath;
                    }
                    else
                    {
                        absolutePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), path);

                        exists = File.Exists(absolutePath);

                        if (exists)
                        {
                            path = absolutePath;
                        }
                    }
                }

                if (!exists)
                {
                    throw new FileNotFoundException($"Could not find git executable.", path);
                }
            }

            return path;
        }

        public static string FindInProgramFiles(params string[] paths)
        {
            var programFilesPaths = new string [paths.Length + 1];

            programFilesPaths[0] = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            Array.Copy(paths, 0, programFilesPaths, 1, paths.Length);

            return Find(programFilesPaths);
        }
    }
}
