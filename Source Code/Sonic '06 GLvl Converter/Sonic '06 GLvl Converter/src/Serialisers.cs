using System;
using System.IO;
using System.Linq;

namespace Sonic_06_GLvl_Converter.Serialisers
{
    class Paths
    {
        /// <summary>
        /// Returns the first directory of a path.
        /// </summary>
        public static string GetRootFolder(string path) {
            while (true) {
                string temp = Path.GetDirectoryName(path);
                if (string.IsNullOrEmpty(temp))
                    break;
                path = temp;
            }
            return path;
        }

        /// <summary>
        /// Returns the full path without an extension.
        /// </summary>
        public static string GetPathWithoutExtension(string path) {
            return Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
        }

        /// <summary>
        /// Returns the folder containing the file.
        /// </summary>
        public static string GetContainingFolder(string path) {
            return Path.GetFileName(Path.GetDirectoryName(path));
        }

        /// <summary>
        /// Returns if the directory is empty.
        /// </summary>
        public static bool IsDirectoryEmpty(string path) {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        /// <summary>
        /// Checks if the path is valid and exists.
        /// </summary>
        public static bool CheckPathLegitimacy(string path) {
            if (Directory.Exists(path) && path != string.Empty) return true;
            else return false;
        }

        /// <summary>
        /// Checks if the path is valid and exists.
        /// </summary>
        public static bool CheckFileLegitimacy(string path) {
            if (File.Exists(path) && path != string.Empty) return true;
            else return false;
        }

        /// <summary>
        /// Returns a new path with the specified filename.
        /// </summary>
        public static string ReplaceFilename(string path, string newFile) {
            return Path.Combine(Path.GetDirectoryName(path), Path.GetFileName(newFile));
        }

        /// <summary>
        /// Compares two strings to check if one is a subdirectory of the other.
        /// </summary>
        public static bool IsSubdirectory(string candidate, string other) {
            bool isChild = false;

            try {
                DirectoryInfo candidateInfo = new DirectoryInfo(candidate);
                DirectoryInfo otherInfo = new DirectoryInfo(other);

                while (candidateInfo.Parent != null) {
                    if (candidateInfo.Parent.FullName == otherInfo.FullName) {
                        isChild = true;
                        break;
                    } else candidateInfo = candidateInfo.Parent;
                }
            } catch (Exception ex) {
                Console.WriteLine($"[{DateTime.Now:hh:mm:ss tt}] [Error] Failed to check directories...\n{ex}");
            }

            return isChild;
        }
    }
}
