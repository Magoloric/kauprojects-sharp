namespace KalkylLotto
{
    internal class LottoController
    {
        //creates new lottery
        public Lotto lottery = new Lotto();
        //Prepares the lottery
        public int PrepareLottery(string nrOfDraws, string[] userLottoNums)
        {
            int[] userLottoNumsInt = new int[7]; //For numbers input by user

            if (int.TryParse(nrOfDraws, out int nrOfDrawsInt)) //Tries to parse the input for ints.
            {
                lottery.NrOfDraws = nrOfDrawsInt;
            }
            else //If input is not int, return error code
            {
                return 1;
            }
            for (int i = 0; i < 7; i++) //For parsing lottery row
            {
                if (int.TryParse(userLottoNums[i], out userLottoNumsInt[i])) //input validation
                {
                    if (userLottoNumsInt[i] < 1 || userLottoNumsInt[i] > 35) //Range validation
                    {
                        return 3;
                    }
                    for (int j = 0; j < i; j++) //Duplicate checker
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
            lottery.LottoRow = userLottoNumsInt; //row is ready
            return 0;
        }
        public void InitiateLottery() //Starts the lottery
        {
            lottery.GenerateRows(lottery.NrOfDraws, lottery.LottoRow); //Generates and compares
        }
        public string FormError(int errCode) //Creates error messages depending on error code
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