namespace AcTemplate
{
    static class PathHelpers
    {
        /// <summary>
        /// 将 \ 替换成 /
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string AsStdPath(this string path)
        {
            if (path == null)
                return null;

            path = path.Replace('\\', '/').TrimEnd('/');

            return path;
        }

        public static bool IsAbsolutePath(string path)
        {
            return Path.IsPathRooted(path);
            if (path.Length > 1)
            {
                if (char.IsLetter(path[0]) && path[1] == ':')
                {
                    return true;
                }
            }

            if (path[0] == '/')
            {
                return true;
            }

            return false;
        }

        public static string GetAbsolutePath(string currentPath, string path)
        {
            string originalPath = path;
            path = path.AsStdPath();

            if (IsAbsolutePath(path))
                return originalPath;

            string currentPathDirName = Path.GetDirectoryName(currentPath).AsStdPath();
            DirectoryInfo currentPathDir = new DirectoryInfo(currentPathDirName);

            if (path.StartsWith("./"))
            {
                // D:\WorkSpace\task.json    
                // task1.json
                // dir/task1.json
                return Path.Combine(currentPathDirName, path.Replace("./", ""));
            }

            DirectoryInfo targetDir = currentPathDir;
            string[] pathItems = path.Split('/', '\\');
            string targetPath = "";
            for (int i = 0; i < pathItems.Length; i++)
            {
                string pathItem = pathItems[i];
                if (pathItem == "..")
                {
                    targetDir = targetDir.Parent;
                    continue;
                }

                if (targetDir == null)
                    throw new FileNotFoundException(originalPath);

                targetPath = Path.Combine(targetDir.FullName.AsStdPath(), string.Join("/", pathItems.Skip(i)));
                break;
            }

            if (string.IsNullOrEmpty(targetPath))
            {
                throw new FileNotFoundException(originalPath);
            }

            return targetPath.AsStdPath();
        }

        public static FileInfo[] GetFiles(string dir, string searchPattern)
        {
            DirectoryInfo directory = new DirectoryInfo(dir);
            var files = directory.GetFiles(searchPattern, new EnumerationOptions() { RecurseSubdirectories = true });

            return files;
        }

        public static string GetFileDir(string fileName)
        {
            return new FileInfo(fileName).Directory.FullName;
        }
        public static void CreateIfDirectoryNotExists(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
        public static void EnsureFileDirectoryExists(string file)
        {
            var fileInfo = new FileInfo(file);
            CreateIfDirectoryNotExists(fileInfo.Directory.FullName);
        }

        public static void CleanDirectory(string dir)
        {
            if (!Directory.Exists(dir))
            {
                return;
            }

            foreach (var filePath in Directory.GetFiles(dir))
            {
                File.Delete(filePath);
            }

            foreach (var subDir in Directory.GetDirectories(dir))
            {
                Directory.Delete(subDir, true);
            }
        }
    }
}
