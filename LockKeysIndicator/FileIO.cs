using System.IO;
using System.Text;

namespace LockKeysIndicator
{
    public static class FileIO
    {
        /// <summary>
        /// Write the specified text to a file.
        /// </summary>
        /// <param name="path">Path of the file to write to</param>
        /// <param name="text">Text to be written</param>
        /// <param name="append">Append the text at the end of the file's contents?</param>
        /// <returns>A boolean representing write success</returns>
        public static bool WriteToFile(string path, string text, bool append = false)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, append, Encoding.UTF8))
                {
                    sw.Write(text);
                }
                return true;
            }
            catch { return false; }
        }

        /// <summary>
        /// Reads the contents of the file at the specified path to a string.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Returns "#IO_ERROR" if file could not be read.</returns>
        public static string ReadFromFile(string path)
        {
            string toReturn = "";
            try
            {
                using (StreamReader sr = new StreamReader(path, true))
                {
                    toReturn = sr.ReadToEnd();
                }
            }
            catch { toReturn = "#IO_ERROR"; }
            return toReturn;
        }

        /// <summary>
        /// Write the specified text to a file.
        /// </summary>
        /// <param name="path">Path of the file to write to</param>
        /// <param name="text">Text to be written</param>
        /// <param name="append">Append the text at the end of the file's contents?</param>
        /// <param name="enc">File encoding to be used</param>
        /// <returns>A boolean representing write success</returns>
        public static bool WriteToFileEnc(string path, string text, bool append, Encoding enc)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, append, enc))
                {
                    sw.Write(text);
                }
                return true;
            }
            catch { return false; } // Log...
        }

        /// <summary>
        /// Reads the contents of the file at the specified path to a string.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Returns "#IO_ERROR" if file could not be read.</returns>
        public static string ReadFromFileEnc(string path, Encoding enc)
        {
            string toReturn = "";
            try
            {
                using (StreamReader sr = new StreamReader(path, enc, true))
                {
                    toReturn = sr.ReadToEnd();
                }
            }
            catch { toReturn = "#IO_ERROR"; } // Log...
            return toReturn;
        }
    }
}
