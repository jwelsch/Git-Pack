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

            foreach (var path in paths)
            {
                var partial = Path.Combine(pathBuilder.ToString(), path);
                pathBuilder.Clear();
                pathBuilder.Append(partial);
            }

            if (!File.Exists(pathBuilder.ToString()))
            {
                throw new FileNotFoundException($"Could not find git executable.", pathBuilder.ToString());
            }

            return pathBuilder.ToString();
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
