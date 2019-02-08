using System;
using System.Windows.Input;

namespace KalkylLotto
{
    internal class LottoController
    {
        public Lotto lottery = new Lotto();
        public int PrepareLottery(string nrOfDraws, string[] userLottoNums)
        {
            int nrOfDrawsInt;
            int[] userLottoNumsInt = new int[7];

            if (Int32.TryParse(nrOfDraws, out nrOfDrawsInt))
            {
                lottery.NrOfDraws = nrOfDrawsInt;
            }
            else
            {
                return 1;
            }
            for (int i = 0; i < 7; i++)
            {
                if (Int32.TryParse(userLottoNums[i], out userLottoNumsInt[i]))
                {
                    if (userLottoNumsInt[i] < 1 || userLottoNumsInt[i] > 35)
                    {
                        return 3;
                    }
                    for (int j = 0; j < i; j++)
                    {
                        if (userLottoNumsInt[i] == userLottoNumsInt[j])
                            return 4;
                    }
                }
                else
                {
                    return 2;
                }
            };
            lottery.LottoRow = userLottoNumsInt;
            return 0;
        }
        public void InitiateLottery()
        {
            lottery.generateRows(lottery.NrOfDraws, lottery.LottoRow);
        }
        public string formError(int errCode)
        {
            string errorMsg = null;
            if (errCode == 1)
            {
                errorMsg = "Antal dragningar skall vara ett heltal!";
            }
            if (errCode == 2)
            {
                errorMsg = "Lottonummer skall alla vara heltal!";
            }
            if (errCode == 3)
            {
                errorMsg = "Lottonummer skall vara ett heltal mellan 1 och 35";  
            }
            if (errCode == 4)
            {
                errorMsg = "Lottorad får ej innehålla dubletter!";
            }
            return errorMsg;
        }
    }
}