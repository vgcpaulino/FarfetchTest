using System.IO;

namespace SeleniumDotNetTest.Helpers
{
    public class FileHelper
    {

        public FileHelper()
        {

        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        public void CreateDirectory(string directoryPath)
        {
            Directory.CreateDirectory(directoryPath);
        }
    }
}
