using System.IO;

namespace Shopstock
{
    /// <summary>
    /// Class that hanldes file reading and writing to them
    /// </summary>
    public class FileController
    {
        private string fileName;  //Path to the file
        private bool fileIsSaved; //Trigger for unsaved changes
        private bool fileIsSavedAtHardDrive; //Triggers savefiledialog if file does not exist.

        public string FileName { get => fileName; set => fileName = value; }
        public bool FileIsSaved { get => fileIsSaved; set => fileIsSaved = value; }
        public bool FileIsSavedAtHardDrive { get => fileIsSavedAtHardDrive; set => fileIsSavedAtHardDrive = value; }



        public string[] OpenFile()
        {
            //If it's valid .txt file, function returns the file's contents
            if (Path.GetExtension(FileName) == ".txt")
                try {
                    return File.ReadAllLines(FileName);
                }
                catch
                {
                    return null;
                }
            //If file is not a valid text file
            else
            {
                return null;
            }
        }

        //Opens sale log
        public string[] OpenSellLog()
        {
            string[] data;
            if (FileName != null)
            {
                string sellLogName = fileName.Insert(fileName.Length - 4, "_sellLog"); //Filename was generated automatically during saving process
                if (File.Exists(sellLogName))
                {
                    data = File.ReadAllLines(sellLogName);
                    return data;
                }
                else return null;
            }
            else return null;
        }
        public void SaveSellLog(string[] data)
        {
            //Write the selling entries to a file
            if (fileName != null)
            {
                string sellLogName = fileName.Insert(fileName.Length - 4, "_sellLog"); //filename generates automatically
                File.WriteAllLines(sellLogName, data);
            }
        }
        public void SaveFile(string[] data)
        {
            //Writes the lines from string array.
            File.WriteAllLines(FileName, data);
        }
        //Opens last file that was using in the program
        public string[] openLastFile()
        {
            if (File.Exists("LastFile.txt")) //Checks if file containing the path to last file exists
            {
                string lastFilePath = File.ReadAllText("LastFile.txt"); //Reads the contents of the file
                if (File.Exists(lastFilePath)) //If it contains the valid path to existing file
                {
                    FileName = lastFilePath; //Opens it
                    return OpenFile(); //Return data that is grabbed from the file
                }
                else //If path doesn't exist
                {
                    File.Delete("LastFile.txt"); //Delete this file and return null
                    return null;
                }
            }
            else return null;
        }

        //Saves path to last file into internal TXT-file
        public void saveLastFilePath()
        {
            if (FileName != null)
                File.WriteAllText("LastFile.txt", FileName);
        }
    }
}
