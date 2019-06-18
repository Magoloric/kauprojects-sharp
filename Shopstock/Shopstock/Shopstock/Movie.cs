namespace Shopstock
{
    /// <summary>
    /// Movie class, based off Product. For function descriptions see the base class
    /// </summary>
    class Movie : Product
    {
        private string studio;
        private int nrOfEpisodes;


        public Movie(int itemNr,
                     float price,
                     string productName,
                     int amount,
                     int returnedAmount,
                     int amountSold,
                     string title,
                     string genre,
                     int yearCreated,
                     string publisher,
                     string studio,
                     int nrOfEpisodes) : base(itemNr,
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
            Studio = studio;
            NrOfEpisodes = nrOfEpisodes;
        }

        string Studio { get => studio; set => studio = value; }
        int NrOfEpisodes { get => nrOfEpisodes; set => nrOfEpisodes = value; }

        public override string ToString()
        {
            return "Movie;" + base.ToString() + ";" + Studio + ";" + nrOfEpisodes + ";";
        }
        public new string ToCuteString()
        {
            return "Typ: Film" + "\n"
                + base.ToCuteString() + "\n"
                + "Studio: " + Studio + "\n"
                + "Antal episoder: " + NrOfEpisodes;
        }
    }
}
