using System.Collections;
using System.IO;
using System.Linq;

namespace DirectoryCompare
{
    public class DirectoryComparer
    {
        public static ArrayList CompareDirectories(string dir1, string dir2)
        {
            var dir1Files = Directory
                .EnumerateFileSystemEntries(dir1, "*", SearchOption.AllDirectories)
                .Select(Path.GetFullPath).Select(name => name.Replace(dir1, ""));

            var dir2Files = Directory
                .EnumerateFileSystemEntries(dir2, "*", SearchOption.AllDirectories)
                .Select(Path.GetFullPath).Select(name => name.Replace(dir2, ""));


            var diffs_to_create = dir1Files.Except(dir2Files).ToArray();
            var diffs_to_delete = dir2Files.Except(dir1Files).ToArray();

            ArrayList results_create = new ArrayList();
            ArrayList results_delete = new ArrayList();

            results_create = PrepareDiffList(dir1, diffs_to_create, "Create");
            results_delete = PrepareDiffList(dir2, diffs_to_delete, "Delete");

            results_create.Sort();

            //EnumerateFileSystemEntries searches directories recursively
            //so reversing items to be deleted is necessary to keep the order
            results_delete.Reverse();
            results_create.AddRange(results_delete);
            
            return results_create;
        }

        private static ArrayList PrepareDiffList(string dir1, string[] diffs, string createOrDelete)
        {
            var results = new ArrayList();

            foreach (var item in diffs)
            {
                FileAttributes attr = File.GetAttributes(Path.Combine(dir1, item.TrimStart('\\')));

                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    results.Add(createOrDelete + " directory: " + item);
                }
                else
                {
                    results.Add(createOrDelete + " file: " + item);
                }
            }

            return results;
        }
    }
}