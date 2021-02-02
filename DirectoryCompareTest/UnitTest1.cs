using System;
using System.Text;
using System.Collections.Generic;
using NUnit;
using NUnit.Framework;
using System.IO;

namespace DirectoryCompareTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestFixture]
    public class NunitTest1
    {
        string rootdir = "D:\\dirtest";
        string dir1, dir2;

        [SetUp]
        public void SetUp()
        {

            dir1 = Path.Combine(rootdir, "dir1");
            dir2 = Path.Combine(rootdir, "dir2");
        }
        [Test]
        public void Compare_Two__Directories_with_System_File()
        {
            //Arrange      
            string sub_dir_1 = Path.Combine(dir1, "dir2");
            string sub_dir_2 = Path.Combine(dir2, "dir2");
            string sub_dir_3 = Path.Combine(dir2, "dir3");
            string sub_dir_4 = Path.Combine(dir1, "dir4");
            string sub_dir_5 = Path.Combine(dir1, "dir5");

            string file1 = Path.Combine(sub_dir_2, "file1");
            string file2 = Path.Combine(sub_dir_5, "file2.txt");

            Directory.CreateDirectory(sub_dir_1);
            Directory.CreateDirectory(sub_dir_2);
            Directory.CreateDirectory(sub_dir_3);
            Directory.CreateDirectory(sub_dir_4);
            Directory.CreateDirectory(sub_dir_5);

            var file1_stream = File.Create(file1);
            var file2_stream = File.Create(file2);

            //Act

            var results = DirectoryCompare.DirectoryComparer.CompareDirectories(dir1, dir2);

            //Assert
            Assert.AreNotEqual(results, null);
            Assert.Contains("Delete directory: \\dir3", results);
            Assert.Contains("Delete file: \\dir2\\file1", results);

            file1_stream.Close();
            file2_stream.Close();
        }
        [Test]
        public void Compare_Two__Directories_with_gitignore()
        {
            //Arrange          
            string sub_dir_1 = Path.Combine(dir1, "dir2");
            string sub_dir_2 = Path.Combine(dir2, "dir2");
            string sub_dir_3 = Path.Combine(dir2, "dir3");
            string sub_dir_4 = Path.Combine(dir1, "dir4");
            string sub_dir_5 = Path.Combine(dir1, "dir5");

            string file1 = Path.Combine(sub_dir_2, ".gitignore");
            string file2 = Path.Combine(sub_dir_5, "file2.txt");

            Directory.CreateDirectory(sub_dir_1);
            Directory.CreateDirectory(sub_dir_2);
            Directory.CreateDirectory(sub_dir_3);
            Directory.CreateDirectory(sub_dir_4);
            Directory.CreateDirectory(sub_dir_5);

            var file1_stream = File.Create(file1);
            var file2_stream = File.Create(file2);

            //Act
            var results = DirectoryCompare.DirectoryComparer.CompareDirectories(dir1, dir2);

            //Assert
            Assert.AreNotEqual(results, null);
            Assert.Contains("Delete directory: \\dir3", results);
            Assert.Contains("Delete file: \\dir2\\.gitignore", results);

            file1_stream.Close();
            file2_stream.Close();
        }
        [Test]
        public void Compare_Two_Identical_Directories()
        {

            //Arrange
            string sub_dir_1 = Path.Combine(dir1, "dir2");
            string sub_dir_2 = Path.Combine(dir2, "dir2");
            string file1_path = Path.Combine(sub_dir_1, "file1.txt");
            string file2_path = Path.Combine(sub_dir_2, "file1.txt");

            Directory.CreateDirectory(sub_dir_1);
            Directory.CreateDirectory(sub_dir_2);

            var file1 = File.Create(file1_path);
            var file2 = File.Create(file2_path);

            //Act
            var results = DirectoryCompare.DirectoryComparer.CompareDirectories(dir1, dir2);

            //Assert        
            Assert.AreEqual(results.Count, 0);

            file1.Close();
            file2.Close();
        }
        [Test]
        public void Compare_Two_Directories_With_Deletion_and_Creation()
        {
            //Arrange
            string sub_dir_1 = Path.Combine(dir1, "dir2");
            string sub_dir_2 = Path.Combine(dir2, "dir2");
            string sub_dir_3 = Path.Combine(dir2, "dir3");
            string sub_dir_4 = Path.Combine(dir1, "dir4");
            string sub_dir_5 = Path.Combine(dir1, "dir5");

            string file1_path = Path.Combine(sub_dir_2, "file1.txt");
            string file2_path = Path.Combine(sub_dir_5, "file2.txt");

            Directory.CreateDirectory(sub_dir_1);
            Directory.CreateDirectory(sub_dir_2);
            Directory.CreateDirectory(sub_dir_3);
            Directory.CreateDirectory(sub_dir_4);
            Directory.CreateDirectory(sub_dir_5);

            var file1_stream = File.Create(file1_path);
            var file2_stream = File.Create(file2_path);

            //Act

            var results = DirectoryCompare.DirectoryComparer.CompareDirectories(dir1, dir2);

            //Assert
            Assert.AreNotEqual(results, null);
            Assert.Contains("Delete directory: \\dir3", results);
            Assert.Contains("Delete file: \\dir2\\file1.txt", results);

            file1_stream.Close();
            file2_stream.Close();
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete(rootdir, true);
        }
    }
}
