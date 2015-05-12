using System;
using System.IO;
using System.Threading;
using log4net;

namespace beah.Library.Web.FileSystem
{
    /// <summary>
    /// Class contains common used file functions
    /// </summary>
    public class FileHelper
    {
        private static readonly ILog _logger = Logging.Logger.GetLogger(typeof(FileHelper));
        private static ILog Log { get { return _logger; } }

        private const int FILE_DELETE_THREAD_SLEEP_TIME = 500;
        /// <summary>
        /// Method creates a file with the specified content
        /// </summary>
        /// <param name="fileName">File to create</param>
        /// <param name="content">File content</param>
        public static void Create(string fileName, string content)
        {
            using (FileStream fs = File.Create(fileName))
            {
                var sw = new StreamWriter(fs);
                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// Method deletes a file. It will retry 5 times in case of a sharing violation.
        /// If unsuccessful the methods logs a warning and returns false, else it returns true
        /// </summary>
        /// <param name="fileName">File to delete</param>
        public static void Delete(string fileName)
        {
            var counter = 1;
            var loop = true;
            while (loop)
            {
                try
                {
                    File.Delete(fileName);
                    loop = false;
                }
                catch (Exception ex)
                {
                    if (counter >= 6) throw new Exception(string.Format("FileDelete", fileName, ex));
                    Thread.Sleep(FILE_DELETE_THREAD_SLEEP_TIME);
                    counter++;
                }
            }
        }

        /// <summary>
        /// Method opens a file and appends to the current content
        /// </summary>
        /// <param name="fileName">File to open</param>
        /// <param name="content">Content to append</param>
        public static void Append(string fileName, string content)
        {
            var counter = 1;
            var loop = true;
            while (loop)
            {
                try
                {
                    using (StreamWriter sw = File.AppendText(fileName))
                    {
                        sw.Write(content);
                        sw.Flush();
                        sw.Close();
                        loop = false;
                    }
                }
                catch (Exception ex)
                {
                    if (counter >= 6) throw new Exception(string.Format("FileDelete", fileName, ex));
                    Thread.Sleep(FILE_DELETE_THREAD_SLEEP_TIME);
                    counter++;
                }
            }
        }

        /// <summary>
        /// Method reads the contents of a file into a Memory stream object
        /// </summary>
        /// <param name="fileName">File to read</param>
        /// <returns>Contents of file in a memory stream</returns>
        public static MemoryStream ReadFileContentIntoStream(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(String.Format(".FileNotFound"));
            }
            else
            {
                using (FileStream fs = File.OpenRead(fileName))
                {
                    var bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, (int)fs.Length);
                    fs.Close();
                    return new MemoryStream(bytes);
                }
            }
        }

        /// <summary>
        /// Reads content of specified file
        /// </summary>
        /// <param name="fileName">File to read</param>
        /// <returns>Content of file as string</returns>
        public static string ReadFileContent(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(String.Format("FileNotFound"));
            }
            else
            {
                using (FileStream fs = File.OpenRead(fileName))
                {
                    var sr = new StreamReader(fs);
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
