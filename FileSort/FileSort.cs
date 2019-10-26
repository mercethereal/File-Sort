using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;



namespace FileSort
{
    class Program
    {
        const string directoryPath = @"c:\sharp\FileSort\tempfiles\";

        static int CreateFiles()
        {
            int numfiles = 0;
            using (StreamReader sr = new StreamReader(directoryPath + "bigfile.txt"))
            {
                while ((sr.Peek()) != -1)
                {
                    StringCollection f = new StringCollection();
                    for (int i = 1; i <= 5000; i++)
                    {
                        if (sr.Peek() != -1)
                            f.Add(sr.ReadLine());
                    }
                    ArrayList.Adapter(f).Sort();
                    using (StreamWriter xl = new StreamWriter(directoryPath + numfiles.ToString() + @".txt", true))
                    {
                        foreach (string e in f)
                        {
                            xl.WriteLine(e);
                        }
                    }
                    numfiles++;
                }
            }
            return numfiles;
        }

        static void retrieveAnswer(int numFiles)
        {
            string nextLine;
            string answer = "";
            string smallestLine = "";
            Dictionary<string, int> lineFileLookup = new Dictionary<string, int>();
            List<StreamReader> fileArray = new List<StreamReader>();
            List<string> MergedLinesArray = new List<string>();

            for (int x = 0; x < numFiles; x++)
            {
                StreamReader sr = new StreamReader(directoryPath + x.ToString() + ".txt");
                MergedLinesArray.Add(sr.ReadLine());
                lineFileLookup.Add(MergedLinesArray[x].Substring(0, 14), x);
                fileArray.Add(sr);
            }

            MergedLinesArray.Sort();
            while (MergedLinesArray.Count > 0)
            {
                smallestLine = MergedLinesArray[0].ToString();
                answer = answer + smallestLine.Substring(28, 1);
                MergedLinesArray.RemoveAt(0);
                int file = lineFileLookup[smallestLine.Substring(0, 14)];
                lineFileLookup.Remove(smallestLine.Substring(0, 14));
                nextLine = fileArray[file].ReadLine();
                if (nextLine != null)
                {
                    var index = MergedLinesArray.BinarySearch(nextLine.Substring(0, 14));
                    MergedLinesArray.Insert(~index, nextLine);
                    lineFileLookup.Add(nextLine.Substring(0, 14), file);
                }
            }
            using (StreamWriter final = new StreamWriter(directoryPath + "answer2.txt"))
            {
                final.Write(answer);
            }
        }

        static void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine($"Decompressed: {fileToDecompress.Name}");
                    }
                }
            }
        }

        static void Compress(FileInfo fileToCompress)
        {
            using (FileStream originalFileStream = fileToCompress.OpenRead())
            {
                if ((File.GetAttributes(fileToCompress.FullName) &
                   FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                {
                    using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ".gz"))
                    {
                        using (GZipStream compressionStream = new GZipStream(compressedFileStream,
                           CompressionMode.Compress))
                        {
                            originalFileStream.CopyTo(compressionStream);
                        }
                    }
                    FileInfo info = new FileInfo(directoryPath + Path.DirectorySeparatorChar + fileToCompress.Name + ".gz");
                    Console.WriteLine($"Compressed {fileToCompress.Name} from {fileToCompress.Length.ToString()} to {info.Length.ToString()} bytes.");
                }
            }
        }

        static void Main(string[] args)
        {
            var fileToDecompress = new FileInfo(directoryPath + "Bigfile.txt.gz");
            var fileToCompress = new FileInfo(directoryPath + "answer2.txt");
            //decompress the files in .net           
            Decompress(fileToDecompress);

            int numFiles = CreateFiles();

            retrieveAnswer(numFiles);

            Compress(fileToCompress);

        }
    }
}

