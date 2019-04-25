using System.IO;
using System.ComponentModel;

namespace TextEditor
{
    /// <summary>
    /// Handles the reading from and writing to text files
    /// </summary>
    class FileController
    {
        public string FileName { set; get; } = "dok1.txt"; //Default filename
        public string OpenFile()
        {
            //If it's valid .txt file, function returns the file's contents
            if (Path.GetExtension(FileName) == ".txt")
                return File.ReadAllText(FileName);
            //If file is not a valid text file
            else
            {
                return "";
            }
        }
        public void SaveFile(string data)
        {
            //Write the text from textbox into current file
            File.WriteAllText(FileName, data);
        }
    }
}
