using System;
using System.IO;
namespace _2048WindowsFormsApp
{
    public class FileProvider
    {
        public static void Replace(string fileName, string value)
        {
            StreamWriter writer = new StreamWriter(fileName, false);
            writer.WriteLine(value);
            writer.Close();
        }
        public static string GetValue(string fileName)
        {
            var reader = new StreamReader(fileName);
            var value = reader.ReadToEnd();
            reader.Close();
            return value;
        }

        public static bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}