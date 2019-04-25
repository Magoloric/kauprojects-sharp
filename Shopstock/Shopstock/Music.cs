namespace Shopstock
{
    /// <summary>
    /// Music class, based off Product. For function descriptions see the base class
    /// </summary>
    class Music : Product
    {
        private string artist;
        private string duration;
        private int nrOfTracks;

        string Artist { get => artist; set => artist = value; }
        string Duration { get => duration; set => duration = value; }
        int NrOfTracks { get => nrOfTracks; set => nrOfTracks = value; }
        public Music(int itemNr,
                     float price,
                     string productName,
                     int amount,
                     int returnedAmount,
                     int amountSold,
                     string title,
                     string genre,
                     int yearCreated,
                     string publisher,
                     string artist,
                     string duration,
                     int nrOfTracks) : base(itemNr,
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
            Artist = artist;
            Duration = duration;
            NrOfTracks = nrOfTracks;
        }

        public override string ToString()
        {
            return "Music;" + base.ToString() + ";" + Artist + ";" + Duration + ";" + NrOfTracks + ";";
        }
        public new string ToCuteString()
        {
            return "Typ: Musik" + "\n"
                + base.ToCuteString() + "\n"
                + "Artisten: " + Artist + "\n"
                + "Längd: " + Duration + "\n"
                + "Antal låtar: " + NrOfTracks;
        }
    }
}
