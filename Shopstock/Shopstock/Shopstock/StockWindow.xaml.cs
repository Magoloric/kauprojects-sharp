using Microsoft.Win32;
using System;
using System.Windows;

namespace Shopstock
{
    /// <summary>
    /// Interaction for Stock Window
    /// </summary>
    public partial class StockWindow : Window
    {
        //Search filters, managed by checkboxes

        int[] filters = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        MainWindow parentWindow;  //Parent window (MainWindow)
        StockModelView smv; //Stock View Model, comes from MainWindow
        FileController fc;  //Handles file in- and output, comes from MainWindow
        AddItemWindow aid; //Add Window
        StatisticsWindow stw; //Statistics Window

        public StockWindow(MainWindow parentWindow, StockModelView smv, FileController fc)
        {
            this.smv = smv;
            this.fc = fc;
            this.parentWindow = parentWindow;
            InitializeComponent();
            //Binds list to stock collections
            StockList.ItemsSource = null;
            StockList.ItemsSource = smv.Sc.WaresInStock.Container;
        }
        //Closes the window
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //Checkboxes manage search filters
        private void NrCheckBox_Checked(object sender, RoutedEventArgs e) => filters[0] = 1;
        private void NrCheckBox_Unhecked(object sender, RoutedEventArgs e) => filters[0] = 0;
        private void TitleCheckBox_Checked(object sender, RoutedEventArgs e) => filters[1] = 1;
        private void TitleCheckBox_Unchecked(object sender, RoutedEventArgs e) => filters[1] = 0;
        private void GenreCheckBox_Checked(object sender, RoutedEventArgs e) => filters[2] = 1;
        private void GenreCheckBox_Unchecked(object sender, RoutedEventArgs e) => filters[2] = 0;
        private void YearCheckBox_Checked(object sender, RoutedEventArgs e) => filters[3] = 1;
        private void YearCheckBox_Unchecked(object sender, RoutedEventArgs e) => filters[3] = 0;
        private void PublisherCheckBox_Checked(object sender, RoutedEventArgs e) => filters[4] = 1;
        private void PublisherCheckBox_Unchecked(object sender, RoutedEventArgs e) => filters[4] = 0;
        private void CreatorCheckBox_Checked(object sender, RoutedEventArgs e) => filters[5] = 1;
        private void CreatorCheckBox_Unchecked(object sender, RoutedEventArgs e) => filters[5] = 0;
        private void InStockCheckBox_Checked(object sender, RoutedEventArgs e) => filters[6] = 1;
        private void InStockCheckBox_Unchecked(object sender, RoutedEventArgs e) => filters[6] = 0;
        private void GameCheckBox_Checked(object sender, RoutedEventArgs e) => filters[7] = 1;
        private void GameCheckBox_Unchecked(object sender, RoutedEventArgs e) => filters[7] = 0;
        private void BookCheckBox_Checked(object sender, RoutedEventArgs e) => filters[8] = 1;
        private void BookCheckBox_Unchecked(object sender, RoutedEventArgs e) => filters[8] = 0;
        private void MusicCheckBox_Checked(object sender, RoutedEventArgs e) => filters[9] = 1;
        private void MusicCheckBox_Unchecked(object sender, RoutedEventArgs e) => filters[9] = 0;
        private void MovieCheckBox_Checked(object sender, RoutedEventArgs e) => filters[10] = 1;
        private void MovieCheckBox_Unchecked(object sender, RoutedEventArgs e) => filters[10] = 0;

        //Opens adding window.
        private void AddWareButton_Click(object sender, RoutedEventArgs e)
        {
            int addedOrNot = smv.Sc.WaresInStock.Container.Count;
            aid = new AddItemWindow(smv);
            aid.ShowDialog();
            if (addedOrNot < smv.Sc.WaresInStock.Container.Count) //Checks if number of items has increased.
            {
                fc.FileIsSaved = false; //If item was added, there are unsaved changes.
            }
            
        }

        //Initiates reading from the file 
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            bool dontWantToSave = false;
            if (fc.FileIsSaved == false) //If changes are unsaved, ask user if he wants to save
            {
                MessageBoxResult notSaved = MessageBox.Show("Det finns osparade ändringar. Vill du spara dem?", "Vänta!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (notSaved == MessageBoxResult.Yes) //Press Yes
                {
                    //If file is already saved on hard drive, just save it
                    if (fc.FileIsSavedAtHardDrive == true)
                    {
                        fc.SaveFile(smv.Sc.StockToString()); //Initiates file writing function.
                        fc.FileIsSaved = true;
                    }
                    else
                    {
                        SaveFileDialog sfd = new SaveFileDialog //Open file dialog
                        {
                            Filter = "Textfilerna (*.txt)|*.txt"
                        };
                        //If not, initiates Save As Dialogue
                        //Initiates the dialog, If user presses "Save"
                        if (sfd.ShowDialog() == true)
                        {
                            fc.FileName = sfd.FileName; //Update fileName
                            fc.SaveFile(smv.Sc.StockToString()); //Initiates file writing function.
                            fc.FileIsSaved = true; //File is now saved
                            fc.FileIsSavedAtHardDrive = true; //To harddisk
                            fc.saveLastFilePath(); //Path to this saved file is saved in internal text file
                            fc.SaveSellLog(smv.Sc.ExportSaleLog()); //Selling log is saed
                        }
                    }
                }
                else if (notSaved == MessageBoxResult.No) //Press no
                {
                    dontWantToSave = true;
                }
            }
            if (fc.FileIsSaved == true || dontWantToSave == true) 
            {
                OpenFileDialog ofd = new OpenFileDialog
                {
                    Filter = "Textfilerna (*.txt)|*.txt"
                };
                //Shows the dialog. If user opens a valid file
                if (ofd.ShowDialog() == true)
                {
                    fc.FileName = ofd.FileName; //Sets filename to path of file that user opens.
                    int result = smv.Sc.StockFromString(fc.OpenFile()); //Tries to open file
                    if (result == 1) //If function return 1, file contains empty fields
                    {
                        MessageBox.Show("Ett fel inträffade när programmet försökte läsa in filen: Filen innehåller tomma eller oläsbara fält!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    if (result == 2) //Was unable to read file
                    {
                        MessageBox.Show("Ett fel inträffade när programmet försökte läsa in filen: Det gick inte att läsa in ett heltalsvärde! Inläsningen är avbryten!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    if (result == 0) //Read without errors
                    {
                        smv.Sc.ImportSaleLog(fc.OpenSellLog());
                        fc.FileIsSaved = true;
                        fc.FileIsSavedAtHardDrive = true;
                        fc.saveLastFilePath();
                        MessageBox.Show("Databasen har lästs från filen!", "Klart!", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                    }
                }
            }
        }

        //Saves stock data to file
        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            //If file is already saved on hard drive, just save it
            if (fc.FileIsSavedAtHardDrive == true)
            {
                fc.SaveFile(smv.Sc.StockToString()); //Initiates file writing function.
                fc.FileIsSaved = true;
                fc.saveLastFilePath();
            }
            //Else open Save as dialog
            else
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Textfilerna (*.txt)|*.txt"
                };
                //Initiates the dialog. If user presses "Save"
                if (sfd.ShowDialog() == true)
                {
                    fc.FileName = sfd.FileName; //Update fileName
                    fc.SaveFile(smv.Sc.StockToString()); //Initiates file writing function.
                    fc.FileIsSaved = true; //File is now saved
                    fc.FileIsSavedAtHardDrive = true; //To harddisk
                    fc.saveLastFilePath();
                }
            }
        }

        //Increases amount of selected item by amount that was written in the textfield
        private void RestockButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Int32.TryParse(AmountIncreaseBox.Text, out int increaseBy)) //If not a number
            {
                MessageBox.Show("Inmatningen gick inte att läsa! Endast heltal kan läsas!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (increaseBy > 0) //Number should be larger than 0, else can't increase
            {
                smv.Sc.IncreaseStock((Product)StockList.SelectedItem, increaseBy); //Increase amount of item
                fc.FileIsSaved = false; //Unsaved changes
            }
            else
            {
                //Show error if number is 0 or smaller
                MessageBox.Show("Angivet nummer är 0 eller mindre! Kan ej öka antalet!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Outputs info about the selected item
        private void StockList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (StockList.SelectedItem != null) //Item should be chosen
            {
                string type = StockList.SelectedItem.GetType().ToString();
                if (type == "Shopstock.Movie") //correct output depends on the item type
                {
                    Movie temp = (Movie)StockList.SelectedItem; //Create temp product of the same type
                    DescriptionBlock.Text = temp.ToCuteString(); //Get the item description
                    SaleStatBox.Text = smv.Sc.SaleDataToString(temp.ItemNr); //Get sales data
                }
                else if (type == "Shopstock.Music")
                {
                    Music temp = (Music)StockList.SelectedItem;
                    DescriptionBlock.Text = temp.ToCuteString();
                    SaleStatBox.Text = smv.Sc.SaleDataToString(temp.ItemNr);
                }
                else if (type == "Shopstock.Game")
                {
                    Game temp = (Game)StockList.SelectedItem;
                    DescriptionBlock.Text = temp.ToCuteString();
                    SaleStatBox.Text = smv.Sc.SaleDataToString(temp.ItemNr);
                }
                else if (type == "Shopstock.Book")
                {
                    Book temp = (Book)StockList.SelectedItem;
                    DescriptionBlock.Text = temp.ToCuteString();
                    SaleStatBox.Text = smv.Sc.SaleDataToString(temp.ItemNr);
                }
            }
            else //Else clear the text boxes
                {
                    DescriptionBlock.Text = "";
                    SaleStatBox.Text = "";
                }
        }

        //Removes selected item from stock. 
        private void DeleteWareButton_Click(object sender, RoutedEventArgs e)
        {
            if (StockList.SelectedItem != null)
            {
                Product temp = (Product)StockList.SelectedItem;
                if (temp.Amount > 0) //If amount of item in stock is bigger than 0
                {
                    MessageBoxResult del = MessageBox.Show("Det finns fortfarande några exemplar av varan kvar i lagret. Vill du verkligen ta bort den ur sortimentet?", "Vänta!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (del == MessageBoxResult.OK) //Ask user if he really wants to remove it.
                    {
                        smv.Sc.RemoveProduct(temp); //Remove the product
                        fc.FileIsSaved = false; //Changes unsaved
                    }
                }
                else
                {
                    smv.Sc.RemoveProduct(temp); //If amount is 0, remove it without asking.
                    fc.FileIsSaved = false;
                }
            }
        }
        //Opens statistics window
        private void StatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            stw = new StatisticsWindow(smv);
            stw.ShowDialog();
        }
        //Clears the search filters by unchecking all filters, clearing search field and binding stock list back to stock collection
        private void ClearSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchBox.Text = "";
            BookCheckBox.IsChecked = false;
            CreatorCheckBox.IsChecked = false;
            GameCheckBox.IsChecked = false;
            GenreCheckBox.IsChecked = false;
            InStockCheckBox.IsChecked = false;
            MovieCheckBox.IsChecked = false;
            MusicCheckBox.IsChecked = false;
            NrCheckBox.IsChecked = false;
            PublisherCheckBox.IsChecked = false;
            TitleCheckBox.IsChecked = false;
            YearCheckBox.IsChecked = false;
            StockList.ItemsSource = null;
            StockList.ItemsSource = smv.Sc.WaresInStock.Container;
        }
        //Initiatest the search and binds stock list to the result collection no matter the result
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            smv.Sc.searchItems(SearchBox.Text, filters);
            StockList.ItemsSource = null;
            StockList.ItemsSource = smv.Sc.SearchResults.Container;
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
                    fc.FileName = "../../../../../MediaIntegrator/MediaIntegrator/bin/Debug/frMediaShop/data.txt";
                    fc.SaveFile(smv.Sc.StockToString()); //Initiates file writing function.
            MessageBox.Show("Databasen har exporterats till filen!", "Klart!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            fc.FileName = "../../../../../MediaIntegrator/MediaIntegrator/bin/Debug/tillMediaShop/data.txt";
            int result = smv.Sc.AppendStockFromString(fc.OpenFile()); //Tries to open file
            if (result == 1) //If function returns 1, file doesn't exist
            {
                MessageBox.Show("Gick inte att importera filen: Ingen fil hittades!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (result == 2) //Was unable to read file
            {
                MessageBox.Show("Ett fel inträffade när programmet försökte läsa in filen: Det gick inte att läsa in ett heltalsvärde! Inläsningen är avbryten!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (result == 0) //Read without errors
            {
                fc.FileIsSaved = false;
                MessageBox.Show("Databasen har importerats från filen!", "Klart!", MessageBoxButton.OK, MessageBoxImage.Asterisk);

            }
        }
    }
}
