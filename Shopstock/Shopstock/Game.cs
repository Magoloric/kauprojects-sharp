namespace Shopstock
{
    /// <summary>
    /// Game class, based off Product. For function descriptions see the base class
    /// </summary>
    class Game : Product
    {
        private string studio;
        private string platform;

        public Game(int itemNr,
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
                    string platform) : base(itemNr,
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
            Platform = platform;
        }

        string Studio { get => studio; set => studio = value; }
        string Platform { get => platform; set => platform = value; }

        public override string ToString()
        {
            return "Game;" + base.ToString() + ";" + Studio + ";" + Platform + ";";
        }

        public new string ToCuteString()
        {
            return "Typ: Spel" + "\n"
                + base.ToCuteString() + "\n"
                + "Skapare: " + Studio + "\n"
                + "´Platformen: " + Platform;
        }
    }
}
