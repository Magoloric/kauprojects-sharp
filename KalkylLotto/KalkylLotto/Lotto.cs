using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalkylLotto
{
    class Lotto : INotifyPropertyChanged
    {
        private int fiveRights, sixRights, sevenRights;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public int[] LottoRow { get; set; }
        public int NrOfDraws { get; set; }
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
        public Lotto()
        {
            LottoRow = new int[7];
            NrOfDraws = 0;
            fiveRights = 0;
            sixRights = 0;
            sevenRights = 0;
        }
        public void generateRows(int draws, int[] lottoRow)
        {
            Random rand = new Random();
            int[] generatedLottoRow = new int[7];
            bool noDuplicates;
            int generatedNumber;
            for (int i = 0; i < draws; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    do
                    {
                        noDuplicates = true;
                        generatedNumber = rand.Next(1, 36);
                        for (int k = 0; k < j; k++)
                        {
                            if (generatedNumber == generatedLottoRow[k])
                            {
                                noDuplicates = false;
                            }
                        }
                    } while (noDuplicates == false);
                    generatedLottoRow[j] = generatedNumber;
                }
                CompareLottoRows(lottoRow, generatedLottoRow);
            }
        }
        public void CompareLottoRows(int[] userLottoNums, int[] generatedLottoNums)
        {
            int rightNums = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (userLottoNums[i] == generatedLottoNums[j])
                        rightNums++;
                }
            }
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
