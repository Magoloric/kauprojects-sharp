using System;
using System.ComponentModel;

namespace KalkylLotto
{
    class Lotto : INotifyPropertyChanged   //class implements the interface that notifies WPF element every time property changes. 
    //Source: https://www.codeproject.com/Articles/36545/WPF-MVVM-Model-View-View-Model-Simplified 
    {
        private int fiveRights, sixRights, sevenRights; //amount of rows with 5, 6 and 7 right numbers

        //For GUI interaction
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propertyName)
        {
            //Simplified if-expression (as suggested by Visual Studio)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int[] LottoRow { get; set; } //Lottery numbers holder
        public int NrOfDraws { get; set; } //Number of rounds where new row is drawn

        //If amount changes, it gets sent to the respective field in the GUI.
        public int FiveRights
        {
            get { return fiveRights; }
            set { 
                fiveRights = value;
                Notify("FiveRights");
            }
        }
        public int SixRights
        {
            get { return sixRights; }
            set
            {
                
                sixRights = value;
                Notify("SixRights");
            }
        }
        public int SevenRights
        {
            get { return sevenRights; }
            set
            {
                sevenRights = value;
                Notify("SevenRights");
            }
        }
        //constructor
        public Lotto()
        {
            LottoRow = new int[7];
            NrOfDraws = 0;
            fiveRights = 0;
            sixRights = 0;
            sevenRights = 0;
        }

        //Lotto row generator
        public void GenerateRows(int draws, int[] lottoRow)
        {
            Random rand = new Random(); //Randomizer
            int[] generatedLottoRow = new int[7]; //For generated numbers
            bool noDuplicates; //Duplicate checker
            int generatedNumber; //single generated number
            for (int i = 0; i < draws; i++) //For each draw
            {
                for (int j = 0; j < 7; j++) //For number
                {
                    do
                    {
                        noDuplicates = true; //Let's say it's unique number
                        generatedNumber = rand.Next(1, 36); //Generate it
                        for (int k = 0; k < j; k++) //and compare with others in the current row
                        {
                            if (generatedNumber == generatedLottoRow[k]) //if same number is caught in the row
                            {
                                noDuplicates = false; //then we need to generate a new number
                            }
                        }
                    } while (noDuplicates == false); //and try until number is unique
                    generatedLottoRow[j] = generatedNumber; //Then put it into generated row
                }
                CompareLottoRows(lottoRow, generatedLottoRow); //Finally, compare this row with the user's row
            } //and keep going until all the rounds done
        }
        public void CompareLottoRows(int[] userLottoNums, int[] generatedLottoNums)
        {
            int rightNums = 0;
            for (int i = 0; i < 7; i++) //Goes through user row
            {
                for (int j = 0; j < 7; j++) //Goes through generated row
                {
                    if (userLottoNums[i] == generatedLottoNums[j]) //If numbers are equal
                        rightNums++; //Number of coincidents increased!
                }
            }
            //for showing off the results in GUI
            if (rightNums == 5)
            {
                FiveRights++;
            }
            if (rightNums == 6)
            {
                SixRights++;
            }
            if (rightNums == 7)
            {
                SevenRights++;
            }
        }
    }
}
