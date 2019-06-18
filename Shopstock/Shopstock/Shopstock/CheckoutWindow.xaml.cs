using System.Drawing.Printing;
using System.Windows;

namespace Shopstock
{
    /// <summary>
    /// Interaction for Checkout Window
    /// </summary>
    public partial class CheckoutWindow : Window
    {
        //Search filters, managed by checkboxes
        int[] filters = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        MainWindow parentWindow; //Parent window (MainWindow)
        StockModelView smv; //Stock View Model, comes from MainWindow
        PrintController pc; //Handles printing
        FileController fc; //Handles file in- and output, comes from MainWindow

        public CheckoutWindow(MainWindow parentWindow, StockModelView smv, FileController fc) //constructor
        {
            this.pc = new PrintController(); //Initiates print controller
            this.smv = smv;
            this.fc = fc;
            this.parentWindow = parentWindow;
            InitializeComponent();
            //Binds lists to stock and basket collections
            StockList.ItemsSource = null;
            StockList.ItemsSource = smv.Sc.WaresInStock.Container;
            BasketList.ItemsSource = null;
            BasketList.ItemsSource = smv.Sc.WaresInBasket;
        }

        //Clears basket and closes the window
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            smv.Sc.ClearBasket();
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
        //Adds selected item from the stock list to the basket
        private void ToBasketButton_Click(object sender, RoutedEventArgs e)
        {
            Product temp = (Product)(StockList.SelectedItem); //Creates temporary copy
            if (smv.Sc.AddToBasket(temp) == false) //Show error if item is out of stock
            {
                MessageBox.Show("Produkten är slut i lagret!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Decreases amount of or removes ware from basket.
        private void FromBasketButton_Click(object sender, RoutedEventArgs e)
        {
            smv.Sc.removeFromBasket((Product)BasketList.SelectedItem);

        }

        //Clears the basket and resets total price to 0
        private void ClearBasketButton_Click(object sender, RoutedEventArgs e)
        {
            smv.Sc.ClearBasket();

            TotalPriceBlock.Text = "0";
        }

        //Increases the amount and returned amount and decreases sold amount of an item chosen in the stock list. 
        private void ReturnWareButton_Click(object sender, RoutedEventArgs e)
        {
            smv.Sc.ReturnItem((Product)BasketList.SelectedItem);
        }

        //initiates selling process
        private void SellButton_Click(object sender, RoutedEventArgs e)
        {
            float discount = 0;
            //Discount field control. If unacceptable symbols detected, error is shown.
            if (float.TryParse(DiscountBox.Text, out discount) == true || DiscountBox.Text == "")
            {
                if (DiscountBox.Text == "") //Set discount to 0 if field is empty
                    discount = 0;
                if (ReceiptCheckBox.IsChecked == true) //If Receipt checkbox is checked
                {
                    string[] data = smv.Sc.ToReceipt(discount); //Writes a receipt to string array
                    pc.Data = data; //Sends that array to Print Controller
                    PrintDocument pd = new PrintDocument();
                    pd.PrintPage += new PrintPageEventHandler
                       (pc.PrintReceipt);
                    try
                    {
                        pd.Print(); //tries to print the page
                    }
                    catch
                    {
                        //Catches if printing did not succeed
                        MessageBox.Show("Något gick fel när programmet försökte att skriva ut kvittot!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        MessageBox.Show("Köpet slutfört! Summa: " + smv.Sc.GetTotalPrice(discount) + " SEK", "Klart!", MessageBoxButton.OK, MessageBoxImage.Information);
                        smv.Sc.SellItem();
                        fc.FileIsSaved = false;
                    }
                }
                    //Shows the confirmation, clears the basket and notifies file controller that unsaved changes are present
                    MessageBox.Show("Köpet slutfört! Summa: " + smv.Sc.GetTotalPrice(discount) + " SEK", "Klart!", MessageBoxButton.OK, MessageBoxImage.Information);
                    smv.Sc.SellItem();
                    fc.FileIsSaved = false;
            }
            else
            {
                //Show error
                MessageBox.Show("Rabattfältet innehåller otillåtna tecken!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        //Every time the basket is updated, total price is updated too. 
        private void BasketList_LayoutUpdated(object sender, System.EventArgs e)
        {
            float discount;
            if (!float.TryParse(DiscountBox.Text, out discount))
            {
                discount = 0;
            }
            TotalPriceBlock.Text = smv.Sc.GetTotalPrice(discount) + " SEK";

            //Kan ej trycka på sälj innan något finns i varukorgen.
            if (BasketList.Items.Count == 0)
            {
                SellButton.IsEnabled = false;
            }
            else
            {
                SellButton.IsEnabled = true;
            }
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

    }
}
