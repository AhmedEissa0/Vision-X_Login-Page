using System;
using System.Diagnostics;
using System.IO;
using Ionic.Zip;
using static System.Net.Mime.MediaTypeNames;

namespace Tst
{
    class Program
    {
        static void Main()
        {
            //string username = Environment.UserName;
            //string path = $@"C:\Users\{username}\Desktop";
            //string zipName = "bin.zip";
            //string password = "012386497";

            //foreach (string filepath in Directory.EnumerateFiles(path, zipName, SearchOption.AllDirectories))
            //{
            //    string extractPath = filepath.Substring(0, filepath.Length - zipName.Length );

            //    if (Path.GetExtension(filepath).Equals(".zip", StringComparison.OrdinalIgnoreCase))
            //    {
            //        if (Path.GetFileName(filepath) == "bin.zip")
            //        {
            //            Console.WriteLine(filepath);
            //            //Extract//

            //            using ZipFile zip = ZipFile.Read(filepath);
            //            zip.Password = password;
            //            foreach (var entry in zip)
            //            {
            //                entry.Extract(extractPath, ExtractExistingFileAction.OverwriteSilently);
            //            }

            //            // Set the hidden attribute for the file
            //            File.SetAttributes(filepath, File.GetAttributes(filepath) | FileAttributes.Hidden);

            //            //Execute//
            //            string exePath = extractPath + @"bin\log\Exe Files\output\calc.exe";
            //            Console.WriteLine(exePath);

            //        }
            //    }
            //}

            // Replace this with the actual VeraCrypt executable path.
            string username = Environment.UserName;
            //string veracryptPath = @"C:\Program Files\VeraCrypt\VeraCrypt.exe";
            string containerPath = $@"C:\Users\{username}\Desktop";
            string containerName = "myvol.hc";
            string mountPoint = "X"; // The drive letter to mount.


            // Replace these with your specific parameters.
            foreach (string filepath in Directory.EnumerateFiles(containerPath, containerName, SearchOption.AllDirectories))
            {
                containerPath = filepath;
                Console.WriteLine(containerPath);
            }

            // Build the command to mount the container.
            string command = $"--text --mount \"{containerPath}\" /v /l {mountPoint}";
            Console.WriteLine(command);

            Process process = new ();

            // Set the StartInfo.FileName property to the path of the VeraCrypt executable file.
            process.StartInfo.FileName = @"C:\Program Files\VeraCrypt\VeraCrypt.exe";

            // Set the StartInfo.Arguments property to the following command:
            process.StartInfo.Arguments = @"";

            // Call the Process.Start() method to start the VeraCrypt process.
            process.Start();

            // Wait for the VeraCrypt process to exit.
            process.WaitForExit();

            // The encrypted file is now mounted to the Z: drive.
            // You can access the encrypted file like any other file.

            // ...

            // Dismount the encrypted file.
            process.StartInfo.Arguments = "/Dismount /Letter:Z";
            process.Start();
            process.WaitForExit();

        }
    }
}