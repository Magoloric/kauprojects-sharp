using Microsoft.Win32;
using System.ComponentModel;
using System.Windows;

namespace Shopstock
{
    /// <summary>
    /// Main window, menu with three buttons
    /// </summary>
    public partial class MainWindow : Window
    {
        CheckoutWindow cw; //Window for managing sales and returns. Meant to be used on checkouts.
        StockWindow sw; //Window for managing stock/inventory. Meant to be used for adding, deleting products and such.
        FileController fc; //Class for reading and writing files.
        StockModelView smv; //ViewModel for stock.
        public MainWindow()
        {
            InitializeComponent();
            //Initialize components
            smv = new StockModelView();
            fc = new FileController();
            Closing += this.MainWindow_Closing; //handles closing event

            ///Open last file (if it exists)///
            //Functions get data from last used files.
            string[] data = fc.openLastFile();
            string[] sellLog = fc.OpenSellLog();
            fc.FileIsSaved = true;

            //If data was successfully transferred into the string array
            if (data != null)
            {
                if (smv.Sc.StockFromString(data) == 0) //Try to read this data into the program
                {
                    //If succeeds, file is saved and is on a hard drive
                    fc.FileIsSavedAtHardDrive = true;
                    //If previous sale log exists and was successfully transferred to array, read it too.
                    if (sellLog != null)
                    {
                        smv.Sc.ImportSaleLog(sellLog);
                    }
                }
            }

        }
        //Open checkout window.
        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            cw = new CheckoutWindow(this, smv, fc);
            cw.ShowDialog();
        }
        //Open stock window.
        private void StockButton_Click(object sender, RoutedEventArgs e)
        {
            sw = new StockWindow(this, smv, fc);
            sw.ShowDialog();
        }
        //Close the program
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            //Before closing the program,  checks if file is saved. 
            if (fc.FileIsSaved == false)
            {
                //If not - ask user if he wants to save.
                MessageBoxResult notSaved = MessageBox.Show("Det finns osparade ändringar. Vill du spara dem?", "Vänta!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (notSaved == MessageBoxResult.Cancel)
                {
                    //User can push cancel and therefore cancel closing
                    e.Cancel = true;
                }
                else if (notSaved == MessageBoxResult.Yes)
                {
                    //If user wants to save and file is not yet saved on hard drive, save dialog opens.
                    if (fc.FileIsSavedAtHardDrive == false)
                    {
                        SaveFileDialog sfd = new SaveFileDialog
                        {
                            Filter = "Textfilerna (*.txt)|*.txt"
                        };
                        //Initiates the dialog. If user presses "Save"
                        if (sfd.ShowDialog() == true)
                        {
                            fc.FileName = sfd.FileName; //Update the filename to the one user has chosen.
                            fc.SaveFile(smv.Sc.StockToString()); //Saves data grabbed from the stock 
                            fc.FileIsSaved = true;
                            fc.FileIsSavedAtHardDrive = true;
                            fc.saveLastFilePath(); //Saves path to the file to an internal text file which is used at next startup
                            fc.SaveSellLog(smv.Sc.ExportSaleLog()); //Saves sale log to the same derictory. 
                        }
                        //User can cancel saving and therefore closing even here
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                    else //If file already exists, no need to open the dialog
                    {
                        fc.SaveFile(smv.Sc.StockToString()); //Initiates file writing function.
                        fc.FileIsSaved = true;
                        fc.saveLastFilePath();
                        fc.SaveSellLog(smv.Sc.ExportSaleLog());
                    }
                }
            }
            //Then close the program
        }

    }
}
