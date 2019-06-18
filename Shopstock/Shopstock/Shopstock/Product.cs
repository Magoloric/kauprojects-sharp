using System.ComponentModel;

///<summary>
///Basic product class
/// </summary>
namespace Shopstock
{
    public abstract class Product : INotifyPropertyChanged
    {
        int itemNr;
        float price;
        string productName;
        int amount;
        int returnedAmount;
        int amountSold;
        string title;
        string genre;
        int yearCreated;
        string publisher;

        protected Product(int itemNr,
            float price,
            string productName,
            int amount,
            int returnedAmount,
            int amountSold,
            string title,
            string genre,
            int yearCreated,
            string publisher)
        {
            ItemNr = itemNr;
            Price = price;
            ProductName = productName;
            Amount = amount;
            ReturnedAmount = returnedAmount;
            AmountSold = amountSold;
            Title = title;
            Genre = genre;
            YearCreated = yearCreated;
            Publisher = publisher;
        }

        public float Price { get => price; set { price = value; Notify("Price"); } }
        public string ProductName { get => productName; set { productName = value; Notify("ProductName"); } }
        public int Amount { get => amount; set { amount = value; Notify("Amount"); } }
        public int ReturnedAmount { get => returnedAmount; set { returnedAmount = value; Notify("ReturnedAmount"); } }
        public string Title { get => title; set { title = value; Notify("Title"); } }
        public string Genre { get => genre; set { genre = value; Notify("Genre"); } }
        public int YearCreated { get => yearCreated; set { yearCreated = value; Notify("YearCreated"); } }
        public string Publisher { get => publisher; set { publisher = value; Notify("Publisher"); } }
        public int AmountSold { get => amountSold; set { amountSold = value; Notify("AmountSold"); } }
        public int ItemNr { get => itemNr; set { itemNr = value; Notify("ItemNr"); } }

        //Notifies when anything changed. Reused from the previous lab
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Overrides default ToString in order to get good separation
        public override string ToString()
        {
            return ItemNr + ";" + ProductName + ";" + Price + ";" + Amount + ";" + ReturnedAmount + ";" + AmountSold + ";" + Title + ";" + Genre + ";" + YearCreated + ";" + Publisher;
        }
        //Arranges the data for beautiful display in the stock window.
        public string ToCuteString()
        {
            return "Produktnamn: " + ProductName + "\n"
                + "Artikelnummer: " + ItemNr + "\n"
                + "Pris: " + Price + "\n"
                + "Antal i lager: " + Amount + "\n"
                + "Antal sålda: " + AmountSold + "\n"
                + "Antal returnerade: " + ReturnedAmount + "\n"
                + "Titel: " + Title + "\n"
                + "Genre: " + Genre + "\n"
                + "Utgiven år: " + YearCreated + "\n"
                + "Förlag: " + Publisher;
        }
    }
}
