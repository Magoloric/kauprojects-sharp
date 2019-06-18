using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


/// <summary>
/// Handles all the stock operations
/// </summary>
namespace Shopstock
{
    public class StockController
    {
        public StockController() //Constructor
        {
            WaresInStock = new Stock(); //Main stock
            WaresInBasket = new ObservableCollection<Product>(); //Basket
            SearchResults = new Stock(); //For search results
            SoldItems = new List<KeyValuePair<DateTime, int>>(); //Sold items log
        }

        Stock waresInStock;
        ObservableCollection<Product> waresInBasket;
        List<KeyValuePair<DateTime, int>> soldItems;
        Stock searchResults;

        public Stock WaresInStock { get => waresInStock; set => waresInStock = value; }
        public ObservableCollection<Product> WaresInBasket { get => waresInBasket; set => waresInBasket = value; }
        public Stock SearchResults { get => searchResults; set => searchResults = value; }
        public List<KeyValuePair<DateTime, int>> SoldItems { get => soldItems; set => soldItems = value; }

        //Add new item of type Music
        public int AddMusic(int itemNr,
                     float price,
                     string productName,
                     int amount,
                     string title,
                     string genre,
                     int yearCreated,
                     string publisher,
                     string artist,
                     string duration,
                     int nrOfTracks)
        {
            //Reads the parameters and adds music with them
            Music musicItem = new Music(itemNr, price, productName, amount, 0, 0, title, genre, yearCreated, publisher,
                               artist, duration, nrOfTracks);
            bool alreadyAdded = DuplicateCheck(musicItem); //Checks if item with this number already exists
            if (!alreadyAdded)
            {
                WaresInStock.Container.Add(musicItem);
                return 0; //Add item and confirm
            }
            else
            {
                return 1; //Return error
            }
        }

        //Add new item of type Movie
        public int AddMovie(int itemNr,
                     float price,
                     string productName,
                     int amount,
                     string title,
                     string genre,
                     int yearCreated,
                     string publisher,
                     string studio,
                     int nrOfEpisodes)
        {
            Movie movieItem = new Movie(itemNr, price, productName, amount, 0, 0, title, genre, yearCreated, publisher,
                                studio, nrOfEpisodes);
            bool alreadyAdded = DuplicateCheck(movieItem);
            if (!alreadyAdded)
            {

                WaresInStock.Container.Add(movieItem);
                return 0;
            }
            else
            {
                return 1;
            }
        }

        //Add new item of type Book
        public int AddBook(int itemNr,
                     float price,
                     string productName,
                     int amount,
                     string title,
                     string genre,
                     int yearCreated,
                     string publisher,
                    string author,
                    string iSBN,
                    int nrOFPages)
        {
            Book bookItem = new Book(itemNr, price, productName, amount, 0, 0, title, genre, yearCreated, publisher,
                            author, iSBN, nrOFPages);
            bool alreadyAdded = DuplicateCheck(bookItem);
            if (!alreadyAdded)
            {

                WaresInStock.Container.Add(bookItem);
                return 0;
            }
            else
            {
                return 1;
            }
        }

        //Add new item of type Game
        public int AddGame(int itemNr,
                     float price,
                     string productName,
                     int amount,
                     string title,
                     string genre,
                     int yearCreated,
                     string publisher,
                    string studio,
                    string platform)
        {
            Game gameItem = new Game(itemNr, price, productName, amount, 0, 0, title, genre, yearCreated, publisher,
                            studio, platform);
            bool alreadyAdded = DuplicateCheck(gameItem);
            if (!alreadyAdded)
            {
                WaresInStock.Container.Add(gameItem);
                return 0;
            }
            else
            {
                return 1;
            }
        }

        //Add the item to basket
        public bool AddToBasket(Product product)
        {
            //Product shouldn't be null
            if (product != null)
            {
                //Item should be in stock.
                if (WaresInStock.Container[FindByItemNr(product.ItemNr)].Amount > 0)
                {
                    //if item is not in the basket yet, add the item's copy depending on it's type.
                    if (FindByItemNrBasket(product.ItemNr) == -1)
                    {
                        string type = product.GetType().ToString();
                        if (type == "Shopstock.Book")
                        {
                            Book temp = new Book(product.ItemNr, product.Price, product.ProductName, 0, 0, 0, null, null, 0, null,
                           null, null, 0);
                            WaresInBasket.Add(temp);

                        }
                        else if (type == "Shopstock.Music")
                        {
                            Music temp = new Music(product.ItemNr, product.Price, product.ProductName, 0, 0, 0, null, null, 0, null,
null, null, 0);
                            WaresInBasket.Add(temp);
                        }
                        else if (type == "Shopstock.Movie")
                        {
                            Movie temp = new Movie(product.ItemNr, product.Price, product.ProductName, 0, 0, 0, null, null, 0, null,
null, 0);
                            WaresInBasket.Add(temp);

                        }
                        else if (type == "Shopstock.Game")
                        {
                            Game temp = new Game(product.ItemNr, product.Price, product.ProductName, 0, 0, 0, null, null, 0, null,
null, null);
                            WaresInBasket.Add(temp);
                        }

                    }
                    //If there is item in the basket already, increase the amount of it in basket and decrease in stock
                    WaresInStock.Container[FindByItemNr(product.ItemNr)].Amount--;
                    WaresInBasket[FindByItemNrBasket(product.ItemNr)].Amount++;
                    return true;
                }
                else return false;
            }
            return true;
        }
        //Remove item from basket
        public void removeFromBasket(Product product)
        {
            if (product != null) //should exist
            {
                //If amount of this item in basket is more than 1, decrease amount
                if (WaresInBasket[FindByItemNrBasket(product.ItemNr)].Amount > 1)
                {
                    WaresInBasket[FindByItemNrBasket(product.ItemNr)].Amount--;
                }
                else //else remove it
                {
                    WaresInBasket.Remove(WaresInBasket[FindByItemNrBasket(product.ItemNr)]);
                }
                //And increment amount of this item in stock.
                WaresInStock.Container[FindByItemNr(product.ItemNr)].Amount++;
            }
        }
        //Remove all items in basket and restore amount in stock
        public void ClearBasket()
        {
            for (int i = 0; i < WaresInBasket.Count; i++)
            {
                WaresInStock.Container[FindByItemNr(WaresInBasket[i].ItemNr)].Amount += WaresInBasket[FindByItemNrBasket(WaresInBasket[i].ItemNr)].Amount;
            }
            WaresInBasket.Clear();
        }
        //Add items to sale log, increase amount of sold units and remove them from basket
        public void SellItem()
        {
            for (int i = 0; i < WaresInBasket.Count; i++)
            {
                for (int j = 0; j < WaresInBasket[i].Amount; j++)
                {
                    SoldItems.Add(new KeyValuePair<DateTime, int>(DateTime.Now, WaresInBasket[i].ItemNr));
                }
                WaresInStock.Container[FindByItemNr(WaresInBasket[i].ItemNr)].AmountSold += WaresInBasket[i].Amount;
            }
            WaresInBasket.Clear();

        }
        //Returns total price including discounts
        public float GetTotalPrice(float discount)
        {
            if (WaresInBasket.Count == 0) //Nothing to count
            {
                return 0;
            }
            else
            {
                float totalPrice = 0;
                for (int i = 0; i < WaresInBasket.Count; i++)
                {
                    totalPrice += WaresInBasket[i].Price * WaresInBasket[i].Amount; //Price for item * nr of units in basket
                }
                if (discount < totalPrice)
                    return totalPrice - discount; //Subtract discount, if it's not larger than total price.
                else
                    return 0; //If discount is larger.
            }
        }

        //Increases the amount of this item in stock but decreases amount of sold units and increases amount of returned units
        public void ReturnItem(Product product)
        {
            if (product != null)
            {
                if (WaresInStock.Container[FindByItemNr(product.ItemNr)].AmountSold > 0)
                {
                    WaresInStock.Container[FindByItemNr(product.ItemNr)].ReturnedAmount++;
                    WaresInStock.Container[FindByItemNr(product.ItemNr)].Amount++;
                    WaresInStock.Container[FindByItemNr(product.ItemNr)].AmountSold--;
                }
            }

        }
        //Checks if this product already exists
        public bool DuplicateCheck(Product product)
        {
            if (product != null)
                return FindByItemNr(product.ItemNr) != -1;
            else return false;
        }

        //Removes selected product from stock
        public bool RemoveProduct(Product product) 
        {
            if (product != null)
                return WaresInStock.Container.Remove(product);
            else return false;
        }
        //Increase amount of selected item in stock with chosen number of units
        public void IncreaseStock(Product product, int amount)
        {
            if (product != null)
            {
                int itemPos = FindItemsPosition(product); //Looks if this product exists
                if (itemPos != -1)
                {
                    WaresInStock.Container[itemPos].Amount += amount;
                }
            }
        }
        //Exports stock contents into array of strings
        public string[] StockToString()
        {
            string[] data = new string[WaresInStock.Container.Count]; 
            for (int i = 0; i < WaresInStock.Container.Count; i++)
            {
                data[i] = WaresInStock.Container[i].ToString();
            }
            return data;
        }
        //Imports stock contents from array of strings
        public int StockFromString(string[] data)
        {
            WaresInStock.Container.Clear(); //Clear the current stock before filling new
            try
            {
                for (int i = 0; i < data.Length; i++) //For every string in array
                {
                    bool containsEmptyValues = false; 
                    string[] values = data[i].Split(';'); //Splits each string in array into substrings through ;
                    for (int j = 0; j < values.Length - 1; j++) //Empty data checker
                    {
                        if (string.IsNullOrEmpty(values[j]))
                            containsEmptyValues = true;
                    }
                    //Data is checked and exists
                    if (containsEmptyValues == false)
                    {
                        int.TryParse(values[1], out int itemNr);
                        float.TryParse(values[3], out float price);
                        int.TryParse(values[4], out int amount);
                        int.TryParse(values[5], out int returnedAmount);
                        int.TryParse(values[6], out int amountSold);
                        int.TryParse(values[9], out int yearCreated);

                        //Read the type and add respective type of item.
                        if (values[0] == "Music")
                        {
                            int.TryParse(values[13], out int nrOfTracks);
                            Music newMusic = new Music(itemNr,
                                price,
                             values[2],
                             amount,
                             returnedAmount,
                             amountSold,
                             values[7],
                             values[8],
                             yearCreated,
                             values[10],
                             values[11],
                          values[12],
                         nrOfTracks);
                            WaresInStock.Container.Add(newMusic);

                        }
                        else if (values[0] == "Movie")
                        {
                            int.TryParse(values[12], out int nrOfEpisodes);
                            Movie newMovie = new Movie(itemNr,
                                price,
                             values[2],
                             amount,
                             returnedAmount,
                             amountSold,
                             values[7],
                             values[8],
                             yearCreated,
                             values[10],
                             values[11],
                         nrOfEpisodes);
                            WaresInStock.Container.Add(newMovie);
                        }
                        else if (values[0] == "Game")
                        {
                            Game newGame = new Game(itemNr,
                                price,
                             values[2],
                             amount,
                             returnedAmount,
                             amountSold,
                             values[7],
                             values[8],
                             yearCreated,
                             values[10],
                             values[11],
                             values[12]);
                            WaresInStock.Container.Add(newGame);
                        }
                        else if (values[0] == "Book")
                        {
                            int.TryParse(values[13], out int nrOfPages);
                            Book newBook = new Book(itemNr,
                                price,
                             values[2],
                             amount,
                             returnedAmount,
                             amountSold,
                             values[7],
                             values[8],
                             yearCreated,
                             values[10],
                             values[11],
                             values[12],
                         nrOfPages);
                            WaresInStock.Container.Add(newBook);
                        }
                    }
                    else
                    {
                        return 1; //If contains empty values
                    }
                }
            }
            catch //If something went wrong
            {
                return 2;
            }
            return 0;
        }

        public int AppendStockFromString(string[] data)
        {
            if (data == null)
            {
                return 1;
            }
            try
            {
                for (int i = 0; i < data.Length; i++) //For every string in array
                {
                    string[] values = data[i].Split(';'); //Splits each string in array into substrings through ;
                    int.TryParse(values[1], out int itemNr);
                    if (FindByItemNr(itemNr) == -1)
                    {
                        float.TryParse(values[3], out float price);
                        int.TryParse(values[4], out int amount);
                        int.TryParse(values[5], out int returnedAmount);
                        int.TryParse(values[6], out int amountSold);
                        int.TryParse(values[9], out int yearCreated);

                        //Read the type and add respective type of item.
                        if (values[0] == "Music")
                        {
                            int.TryParse(values[13], out int nrOfTracks);
                            Music newMusic = new Music(itemNr,
                                price,
                             values[2],
                             amount,
                             returnedAmount,
                             amountSold,
                             values[7],
                             values[8],
                             yearCreated,
                             values[10],
                             values[11],
                          values[12],
                         nrOfTracks);
                            WaresInStock.Container.Add(newMusic);

                        }
                        else if (values[0] == "Movie")
                        {
                            int.TryParse(values[12], out int nrOfEpisodes);
                            Movie newMovie = new Movie(itemNr,
                                price,
                             values[2],
                             amount,
                             returnedAmount,
                             amountSold,
                             values[7],
                             values[8],
                             yearCreated,
                             values[10],
                             values[11],
                         nrOfEpisodes);
                            WaresInStock.Container.Add(newMovie);
                        }
                        else if (values[0] == "Game")
                        {
                            Game newGame = new Game(itemNr,
                                price,
                             values[2],
                             amount,
                             returnedAmount,
                             amountSold,
                             values[7],
                             values[8],
                             yearCreated,
                             values[10],
                             values[11],
                             values[12]);
                            WaresInStock.Container.Add(newGame);
                        }
                        else if (values[0] == "Book")
                        {
                            int.TryParse(values[13], out int nrOfPages);
                            Book newBook = new Book(itemNr,
                                price,
                             values[2],
                             amount,
                             returnedAmount,
                             amountSold,
                             values[7],
                             values[8],
                             yearCreated,
                             values[10],
                             values[11],
                             values[12],
                         nrOfPages);
                            WaresInStock.Container.Add(newBook);
                        }
                    }
                }
            }
            catch //If something went wrong
            {
                return 2;
            }
            return 0;
        }

        //Returns the data for printing on receipt
        public string[] ToReceipt(float discount)
        {
            string[] data = new string[WaresInBasket.Count + 1]; //Adds extra string for needed information
            for (int i = 0; i < WaresInBasket.Count; i++) //adds every product name, amount of them in basket and price
            {
                data[i] = WaresInBasket[i].ProductName + "  *" + WaresInBasket[i].Amount + ": "+ WaresInBasket[i].Price + " SEK";
            } //Adds the info about discount and total price
            data[WaresInBasket.Count] = "Avdrag: -" + discount + " SEK" + '\n'
            + "Totalt: " + GetTotalPrice(discount) + " SEK" + '\n' 
            + "SPARA KVITTOT!";
            return data;
        }

        //Seeks the position of the item in stock
        public int FindItemsPosition(Product product)
        {
            if (WaresInStock.Container.Contains(product))
            {
                return WaresInStock.Container.IndexOf(product);
            }
            else
            {
                return -1;
            }
        }
        //Seeks the position of the item in stock using item number
        public int FindByItemNr(int itemNr)
        {
            for (int i = 0; i < WaresInStock.Container.Count; i++)
            {
                if (WaresInStock.Container[i].ItemNr == itemNr)
                {
                    return i;
                }
            }
            return -1;
        }
        //Seeks the position of the item in search results using item number
        public int FindByItemNrSearch(int itemNr)
        {
            for (int i = 0; i < SearchResults.Container.Count; i++)
            {
                if (SearchResults.Container[i].ItemNr == itemNr)
                {
                    return i;
                }
            }
            return -1;
        }
        //Seeks the position of the item in basket using item number
        public int FindByItemNrBasket(int itemNr)
        {
            for (int i = 0; i < WaresInBasket.Count; i++)
            {
                if (WaresInBasket[i].ItemNr == itemNr)
                {
                    return i;
                }
            }
            return -1;
        }
        //Returns data from SoldItems as array of strings.
        public string[] ExportSaleLog()
        {
            string[] data = new string[SoldItems.Count];
            
            for (int i = 0; i < SoldItems.Count; i++)
            {
                data[i] = SoldItems[i].Key.ToString() + ";" + SoldItems[i].Value.ToString();
            }

            return data;
        }
        //Reads the data from array of strings into SoldItems
        public void ImportSaleLog(string[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                string[] values = data[i].Split(';');
                DateTime.TryParse(values[0], out DateTime sellDate);
                Int32.TryParse(values[1], out int itemNr);
                SoldItems.Add(new KeyValuePair<DateTime, int>(sellDate, itemNr));
            }

        }

        //Searches the stock for items containing search query
        public void searchItems(string query, int[] filters)
        {
            int[] doNotFilter = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            ObservableCollection<Product> unfiltered = new ObservableCollection<Product>();
            SearchResults.Container.Clear(); //Clears search results
            for (int i = 0; i < WaresInStock.Container.Count; i++) //Looks for every item
            {
                string[] temp = WaresInStock.Container[i].ToString().Split(';'); //Used to find creator, since every subclass has it as value #12

                //If any of the filterable values contain this query
                if (WaresInStock.Container[i].ItemNr.ToString().Contains(query) ||
                    WaresInStock.Container[i].ProductName.Contains(query) ||
                    WaresInStock.Container[i].Title.Contains(query) ||
                    WaresInStock.Container[i].Genre.Contains(query) ||
                    WaresInStock.Container[i].YearCreated.ToString().Contains(query) ||
                    WaresInStock.Container[i].Publisher.Contains(query) ||
                    temp[11].Contains(query) ||
                    query == "")
                {
                    if (!unfiltered.Contains(WaresInStock.Container[i])) //If product is not already in search results
                    {
                        unfiltered.Add(WaresInStock.Container[i]); //it gets added
                    }
                }
            }
            //If no checkbox was checked, no filtering needs to be done. Else go to next function
            if (doNotFilter == filters || unfiltered.Count == 0)
            {
                SearchResults.Container = unfiltered;
            }
            else
            {
                FilterSearch(filters, query, unfiltered);
            }
        }
        //Filters primary search results
        private void FilterSearch(int[] filters, string query, ObservableCollection<Product> unfiltered)
        {
            ObservableCollection<Product> amountFiltered = new ObservableCollection<Product>();

            if (filters[6] == 1) //If filter for amount units in stock is activated
            {
                for (int i = 0; i < unfiltered.Count; i++)
                {

                    if (unfiltered[i].Amount > 0)
                    {
                        if (!amountFiltered.Contains(unfiltered[i]))
                        {
                            amountFiltered.Add(unfiltered[i]);
                        }//All items that are out of stock are removed
                    }
                }
            }
            else //If not activated, then continue to next step
            {
                amountFiltered = unfiltered;
            }
            //Next, if any of item type filters are checked, filter them. If not - continue to next step
            ObservableCollection<Product> typeFiltered = new ObservableCollection<Product>();
            if (filters[10] == 0 &&
                filters[9] == 0 &&
                filters[8] == 0 &&
                filters[7] == 0)
            {
                typeFiltered = amountFiltered;
            }
            else
            {
                //Checks every item for being a certain type.
                for (int i = 0; i < amountFiltered.Count; i++)
                {

                    if (filters[10] == 1)
                    {
                        if (amountFiltered[i].GetType().ToString() == "Shopstock.Movie")
                        {
                            if (!typeFiltered.Contains(amountFiltered[i]))
                            {
                                typeFiltered.Add(unfiltered[i]);
                                ;
                            }
                        }
                    }
                    if (filters[9] == 1)
                    {
                        if (amountFiltered[i].GetType().ToString() == "Shopstock.Music")
                        {
                            if (!typeFiltered.Contains(amountFiltered[i]))
                            {
                                typeFiltered.Add(amountFiltered[i]);
                            }
                        }
                    }
                    if (filters[8] == 1)
                    {
                        if (amountFiltered[i].GetType().ToString() == "Shopstock.Book")
                        {
                            if (!typeFiltered.Contains(amountFiltered[i]))
                            {
                                typeFiltered.Add(amountFiltered[i]);
                            }
                        }
                    }
                    if (filters[7] == 1)
                    {
                        if (amountFiltered[i].GetType().ToString() == "Shopstock.Game")
                        {
                            if (!typeFiltered.Contains(amountFiltered[i]))
                            {
                                typeFiltered.Add(amountFiltered[i]);
                            }
                        }
                    }
                }
            }
            //Finally, if any of "value" filters are checked, add only results that contain query in the selected values. Else add directly to search results
            if (filters[0] == 0 &&
                filters[1] == 0 &&
                filters[2] == 0 &&
                filters[3] == 0 &&
                filters[4] == 0 &&
                filters[5] == 0)
            {
                SearchResults.Container = typeFiltered;
            }

            else
            {
                for (int i = 0; i < typeFiltered.Count; i++)
                {
                    if (filters[0] == 1)
                    {
                        if (typeFiltered[i].ItemNr.ToString().Contains(query) || query == "")
                        {
                            if (!SearchResults.Container.Contains(typeFiltered[i]))
                            {
                                SearchResults.Container.Add(typeFiltered[i]);
                            }
                        }
                    }
                    if (filters[1] == 1)
                    {
                        if (typeFiltered[i].Title.Contains(query) || query == "")
                        {
                            if (!SearchResults.Container.Contains(typeFiltered[i]))
                            {
                                SearchResults.Container.Add(typeFiltered[i]);
                            }
                        }
                    }
                    if (filters[2] == 1)
                    {
                        if (typeFiltered[i].Genre.Contains(query) || query == "")
                        {
                            if (!SearchResults.Container.Contains(typeFiltered[i]))
                            {
                                SearchResults.Container.Add(typeFiltered[i]);
                            }
                        }
                    }
                    if (filters[3] == 1)
                    {
                        if (typeFiltered[i].YearCreated.ToString().Contains(query) || query == "")
                        {
                            if (!SearchResults.Container.Contains(typeFiltered[i]))
                            {
                                SearchResults.Container.Add(typeFiltered[i]);
                            }
                        }
                    }
                    if (filters[4] == 1)
                    {
                        if (typeFiltered[i].Publisher.Contains(query) || query == "")
                        {
                            if (!SearchResults.Container.Contains(typeFiltered[i]))
                            {
                                SearchResults.Container.Add(typeFiltered[i]);
                            }
                        }
                    }
                    if (filters[5] == 1)
                    {
                        string[] temp = typeFiltered[i].ToString().Split(';');
                        if (temp[11].Contains(query) || query == "")
                        {
                            if (!SearchResults.Container.Contains(typeFiltered[i]))
                            {
                                SearchResults.Container.Add(typeFiltered[i]);
                            }
                        }
                    }
                }
            }
        }
        //Writes amount sold items this month and year in a graceful style for displaying in stock window
        public string SaleDataToString(int itemNr)
        {
            int soldThisMonth = 0, soldThisYear = 0;
            for (int i = 0; i < SoldItems.Count; i++)
            {
                if (SoldItems[i].Key.Month == DateTime.Now.Month && SoldItems[i].Value == itemNr)
                {
                    soldThisMonth++;
                }
                if (SoldItems[i].Key.Year == DateTime.Now.Year && SoldItems[i].Value == itemNr)
                {
                    soldThisYear++;
                }
            }
            return "Sålt den här månaden: " + soldThisMonth + '\n'
                + "Sålt den här året: " + soldThisYear;
        }

        //Following 4 functions get items of respective type into separate observable collection, order it by amount sold and return the collection
        public ObservableCollection<Product> GetTop10Books()
        {
            ObservableCollection<Product> result = new ObservableCollection<Product>();
            for (int i = 0; i < WaresInStock.Container.Count; i++)
            {
                if (WaresInStock.Container[i].GetType().ToString() == "Shopstock.Book")
                {

                    result.Add(WaresInStock.Container[i]);
                }
            }
            //LINQ for ordering, based on this one: https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.orderby?view=netframework-4.8
            result.OrderBy(Product => Product.AmountSold);
            return result;
        }
        public ObservableCollection<Product> GetTop10Games()
        {
            ObservableCollection<Product> result = new ObservableCollection<Product>();
            for (int i = 0; i < WaresInStock.Container.Count; i++)
            {
                if (WaresInStock.Container[i].GetType().ToString() == "Shopstock.Game")
                {

                    result.Add(WaresInStock.Container[i]);

                }
            }
            result.OrderBy(Product => Product.AmountSold);
            return result;
        }
        public ObservableCollection<Product> GetTop10Music()
        {
            ObservableCollection<Product> result = new ObservableCollection<Product>();
            for (int i = 0; i < WaresInStock.Container.Count; i++)
            {
                if (WaresInStock.Container[i].GetType().ToString() == "Shopstock.Music")
                {

                    result.Add(WaresInStock.Container[i]);

                }
            }
            result.OrderBy(Product => Product.AmountSold);
            return result;
        }
        public ObservableCollection<Product> GetTop10Movies()
        {
            ObservableCollection<Product> result = new ObservableCollection<Product>();
            for (int i = 0; i < WaresInStock.Container.Count; i++)
            {
                if (WaresInStock.Container[i].GetType().ToString() == "Shopstock.Movie")
                {

                    result.Add(WaresInStock.Container[i]);

                }
            }
            result.OrderBy(Product => Product.AmountSold);
            return result;
        }
    }
}

