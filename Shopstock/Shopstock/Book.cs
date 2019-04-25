namespace Shopstock
{
    /// <summary>
    /// Book class, based off Product. For function descriptions see the base class
    /// </summary>
    class Book : Product
    {
        private string author;
        private string iSBN;
        private int nrOfPages;


        public Book(int itemNr,
                    float price,
                    string productName,
                    int amount,
                    int returnedAmount,
                    int amountSold,
                    string title,
                    string genre,
                    int yearCreated,
                    string publisher,
                    string author,
                    string iSBN,
                    int nrOfPages) : base(itemNr,
                        price,
                        productName,
                        amount,
                        returnedAmount,
                        amountSold,
                        title,
                        genre,
                        yearCreated,
                        publisher)
        {
            Author = author;
            ISBN = iSBN;
            NrOfPages = nrOfPages;
        }

        string Author { get => author; set => author = value; }
        string ISBN { get => iSBN; set => iSBN = value; }
        int NrOfPages { get => nrOfPages; set => nrOfPages = value; }

        public override string ToString()
        {
            return "Book;" + base.ToString() + ";" + Author + ";" + ISBN + ";" + NrOfPages;
        }
        public new string ToCuteString()
        {
            return "Typ: Bok" + "\n"
                + base.ToCuteString() + "\n"
                + "Skriven av: " + Author + "\n"
                + "ISBN: " + ISBN + "\n"
                + "Antal sidor: " + NrOfPages;
        }
    }
}
