using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TextEditor
{
    /// <summary>
    /// Main window interaction
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //Counters
        private int nrOfLetters;
        private int nrOfWords;
        private int nrOfLines;
        private int nrOfSpaceAndLetter;

        //Trigger for text changing
        private bool textChanged = false;

        //Trigger for file saved to hard drive/opened from hard drive
        private bool fileIsSaved = false;
        //Trigger for pressing Cancel in save dialog.
        private bool saveCancelled = false;
        //Window title
        private string currentTitle = "dok1.txt";
        //Handles writing and reading of files
        private FileController fc = new FileController();

        //Handles counters
        private TextController tc = new TextController();

        //Handler for counters and titles.
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Incapsulators
        public int NrOfLines
        {
            get { return nrOfLines; }
            set
            {
                nrOfLines = value;
                Notify("NrOfLines");
            }
        }
        public int NrOfLetters
        {
            get { return nrOfLetters; }
            set
            {
                nrOfLetters = value;
                Notify("NrOfLetters");
            }
        }
        public int NrOfWords
        {
            get { return nrOfWords; }
            set
            {
                nrOfWords = value;
                Notify("NrOfWords");
            }
        }

        public int NrOfSpaceAndLetter
        {
            get { return nrOfSpaceAndLetter; }
            set
            {
                nrOfSpaceAndLetter = value;
                Notify("NrOfSpaceAndLetter");
            }
        }
        public string CurrentTitle
        {
            get { return currentTitle; }
            set
            {
                currentTitle = value;
                Notify("CurrentTitle");
            }
        }
        //Counter functions
        private int GetNumberOfLetters()
        {
            return tc.GetNumberOfLetters(); //Gets number of letters
        }
        private int GetNumberOfLettersAndSpaces()
        {
            return tc.getNumberOfLettersAndSpaces(); //Gets number of letters and whitespaces
        }
        private int GetNumberOfWords()
        {
            return tc.getNumberOfWords();//Gets number of words
        }
        public MainWindow()         //initiates main window
        {
            InitializeComponent();
            //Sets data context for binding.
            DataContext = this;
        }
        //Detects text changes in textbox
        private void TextEditorBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Adds an asterisk which indicates that file is changed (If there's none yet)
            if (!Title.Contains("*") && textChanged == true)
                CurrentTitle = fc.FileName + "*";
            textChanged = true; //Text is changed
            tc.Text = TextEditorBox.Text; //Sends text to text controller for counting functions
            NrOfLines = TextEditorBox.LineCount;  //Updates line counter via built-in function in TextBox
                                                  //(Source: https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.textbox.linecount?view=netframework-4.7.2)

            //Updates counters via functions in the text controller
            NrOfLetters = GetNumberOfLetters(); //Letters
            NrOfSpaceAndLetter = GetNumberOfLettersAndSpaces(); //Spaces And Letters
            NrOfWords = GetNumberOfWords(); //Words
        }

        //Menu buttons:
        //New File
        private void NewFileButton_Click(object sender, RoutedEventArgs e)
        {
            //Initiates function for unsaved changes checker
            SaveHandler("new", null);
        }

        //Open file
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            //Initiates function for unsaved changes checker
            SaveHandler("open", null);
        }

        //Save
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //If file is already saved to hard drive
            if (fileIsSaved == true)
            {
                fc.SaveFile(TextEditorBox.Text); //Initiates file writing function.
                CurrentTitle = fc.FileName; //Updates the window title (removes asterisk)
                textChanged = false; //Text is saved and therefore no longer is changed.
                saveCancelled = false; //Saving process is not aborted
            }
            //If not, initiates Save As Dialogue
            else
                SaveAsButton_Click(this, null); //Imitates click on Save As...
        }
        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            //Creates dialog for saving files, restricted to text files.
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Textfilerna (*.txt)|*.txt"
            };
            //Initiates the dialog. If user presses "Save"
            if (sfd.ShowDialog() == true)
            {
                fc.FileName = sfd.FileName; //Update fileName
                fc.SaveFile(TextEditorBox.Text); //Write text from textbox to file
                fileIsSaved = true; //File is saved to hard drive
                textChanged = false; //File is saved, text is not changed
                CurrentTitle = fc.FileName; //Updates the title
                saveCancelled = false; //File is saved
            }
            else //If user presses "Cancel"
            {
                saveCancelled = true; //File is not saved
            }
        }

        //Exit
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {

            Close(); //Initiate closing event
        }

        //Closing event handler
        //Source https://docs.microsoft.com/en-us/dotnet/api/system.windows.window.closing?view=netframework-4.7.2
        private void TextEditWindow_Closing(object sender, CancelEventArgs e)
        {
            //Checks for unsaved changes (Separated from SaveHandler due to event handling)
            if (textChanged == true)
            {
                //Displays warning message
                MessageBoxResult warn = MessageBox.Show(this, "Du har osparade ändringar. Vill du spara filen nu?", "Varning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                //If user wants to save
                if (warn == MessageBoxResult.Yes)
                {
                    SaveButton_Click(this, null);  //Initiates saving
                    //If user cancels saving, closing event gets cancelled as well
                    if (saveCancelled == true)
                    {
                        e.Cancel = true;
                    }
                }
                //If user presses "Cancel" in warning dialog, event gets cancelled
                else if (warn == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
                //If user presses "No", software is closed without saving
            }
        }

        //Handles dropping of text files in the textbox
        //Source https://stackoverflow.com/questions/52621314/wpf-drag-and-drop-text-file-into-application
        private void TextEditorBox_Drop(object sender, DragEventArgs e)
        {
            //If data that is dropped in the textbox is a file
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //Fills string array with file paths
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files[0] != null) //Always reads the first one.

                    //If Shift is held while file is dropped
                    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                    {
                        string tempFN = fc.FileName; //Sets temporary variable to current filename.
                        fc.FileName = files[0]; //Changes filename to first file path in the array.
                        //Adds the text from the file starting from cursors current position
                        //Source: https://docs.microsoft.com/en-us/dotnet/api/system.string.insert?view=netframework-4.7.2
                        TextEditorBox.Text = TextEditorBox.Text.Insert(TextEditorBox.CaretIndex, fc.OpenFile());
                        fc.FileName = tempFN; //Sets filename back to current
                        textChanged = true; //Text is changed
                        CurrentTitle = fc.FileName + "*"; //Add asterisk
                    }
                    //If Ctrl is held while file is dropped
                    else if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                    {
                        string tempFN = fc.FileName;
                        fc.FileName = files[0];
                        //Appends the text from the file to current text
                        TextEditorBox.Text += fc.OpenFile();
                        fc.FileName = tempFN;
                        textChanged = true;
                        CurrentTitle = fc.FileName + "*";
                    }
                    //if file is just dropped into textbox
                    else
                    {
                        //initiate saving dialog handler
                        SaveHandler("drop", files[0]);
                    }
            }
            //Show error if it doesn't accept the dropped data
            else
            {
                MessageBox.Show(this, "Endast .txt-filer stöds av denna program", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Handles drag event
        private void TextEditorBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }
        /// <summary>
        /// Function that handles file saving and opening dialogs
        /// </summary>
        /// <param name="action">string that defines what should be done</param>
        /// <param name="extra">For filename in "drop" action</param>
        /// <returns>Nothing, needed for early return</returns>
        private int SaveHandler(string action, string extra)
        {
            MessageBoxResult mbr; //Stores the button that user click on "Do You Want To Save" dialog.
            if (textChanged == true)
            {
                //"Do You Want To Save"-dialog
                mbr = MessageBox.Show(this, "Du har osparade ändringar. Vill du spara filen nu?", "Varning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            }
            else //If text wasn't changed, dialog is not needed
            {
                mbr = MessageBoxResult.No;
            }
            //If user clicked "yes"
            if (mbr == MessageBoxResult.Yes)
            {
                SaveButton_Click(this, null);
                //If user chooses "no", this will be skipped.
            }
            //Aborts the function if user presses "cancel". 
            else if (mbr == MessageBoxResult.Cancel)
            {
                return 0;
            }
            //If new file creation initiated
            if (action == "new")
            {
                //If user chooses to save the file and doesn't cancel it during savefile dialog
                //Or if user chooses not to save
                if ((saveCancelled == false && mbr == MessageBoxResult.Yes) || mbr == MessageBoxResult.No)
                {
                    TextEditorBox.Text = ""; //Empty the textbox
                    fc.FileName = "dok1.txt"; //Sets default file name
                    fileIsSaved = false; //tells that new file is not saved to hard drive.
                    textChanged = false; //tells that text is not changed at the moment.
                    CurrentTitle = fc.FileName; //Updates the title.
                }
            }
            //If file opening initiated
            else if (action == "open")
            {
                if ((saveCancelled == false && mbr == MessageBoxResult.Yes) || mbr == MessageBoxResult.No)
                {
                    //creates dialog for opening files, restricted to text files
                    OpenFileDialog ofd = new OpenFileDialog
                    {
                        Filter = "Textfilerna (*.txt)|*.txt"
                    };
                    //Shows the dialog. If user opens a valid file
                    if (ofd.ShowDialog() == true)
                    {
                        fc.FileName = ofd.FileName; //Sets filename to path of file that user opens.
                        TextEditorBox.Text = fc.OpenFile(); //Initiates file reading
                        textChanged = false; //Signals that text is just opened and therefore not changed
                        fileIsSaved = true; //Signals that the file was opened from hard drive and therefore is already saved on it.
                        CurrentTitle = fc.FileName; //Updates the title
                    }
                    //If user cancels file opening, text and title stays the same. 
                }
            }
            //If file dropped onto textbox
            else if (action == "drop")
            {

                if ((saveCancelled == false && mbr == MessageBoxResult.Yes) || mbr == MessageBoxResult.No)
                {
                    fc.FileName = extra; //Sets filename to path of dropped file (Needed to save previous file before changing the filename)
                    TextEditorBox.Text = fc.OpenFile(); //Reads the dropped file
                    textChanged = false; //Text is not changed
                    fileIsSaved = true; //File is already on hard drive
                    CurrentTitle = fc.FileName; //Updates the title

                }
            }
            return 0;
        }

    }
}
