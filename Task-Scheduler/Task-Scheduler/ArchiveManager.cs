using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Scheduler
{
    class ArchiveManager
    {
        public const string ARCHIVE_PATH = "Archive";

        public static void createArchive()
        {
            string outputPath = Registry.GetRegKeyValue(Registry.RegistryKeys.OutputFile);

            if (!Directory.Exists(outputPath + "\\" + ARCHIVE_PATH)) Directory.CreateDirectory(outputPath + "\\" + ARCHIVE_PATH);
        }

        public static void openArchiveFile(string fileName)
        {
            string fullPath = Registry.GetRegKeyValue(Registry.RegistryKeys.OutputFile) + "\\" + ARCHIVE_PATH + "\\" + fileName;
            if (File.Exists(fullPath)) System.Diagnostics.Process.Start(fullPath);
        }

        public static void addFileToArchive(string fullPath)
        {
            
            string outputPath = Registry.GetRegKeyValue(Registry.RegistryKeys.OutputFile) + "\\" + ARCHIVE_PATH;

            if (!Directory.Exists(outputPath)) createArchive();

            try
            {
                File.Copy(fullPath, outputPath + "\\" + Path.GetFileName(fullPath), true);
            }
            catch
            {

            }
        }
    }
}
