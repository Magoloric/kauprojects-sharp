using System.ComponentModel;

namespace KalkylLotto
{
    class CalculatorController : INotifyPropertyChanged
    //class implements the interface that notifies WPF element every time property changes. 
    //Source: https://www.codeproject.com/Articles/36545/WPF-MVVM-Model-View-View-Model-Simplified
    {
        protected Calculator calc = new Calculator(); //Creates calculator
        private float result; //stores the result
        //For GUI interaction
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propertyName)
        {
            //Simplified if-expression (as suggested by Visual Studio)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public float Result
        {
            get { return result; }
            set
            {
                //If result changes, it gets sent to the "Result Field" in the GUI.
                if (value != result)
                { result = value; }
                Notify("Result");
            }
        }
        public int PrepareCalc(string input1, string input2)
        {
            //Parses the input for floats. 
            //Source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-a-string-to-a-number
            //Check first input
            if (float.TryParse(input1, out float inputFloat1))
            {
                calc.Input1 = inputFloat1;
                //Check second input
                if (float.TryParse(input2, out float inputFloat2))
                {
                    calc.Input2 = inputFloat2;
                    return 0;
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 1;
            }
            //Depending on the returned number the error message is formed (or not, if it's zero)
        }
        //Following four functions work similar way. 
        public int OnAdd(string input1, string input2)
        {
            //Parse inputs first
            int code = PrepareCalc(input1, input2);
            if (code == 0) //If parse is successfull
            {
                Result = calc.AddNumbers(calc.Input1, calc.Input2); //Do the math
                calc.Input1 = Result; //Move result to the first input for further actions
            }
            return code; //Return the code for error checker.

        }
        public int OnSubtract(string input1, string input2)
        {
            int code = PrepareCalc(input1, input2);
            if (code == 0)
            {
                Result = calc.SubtractNumbers(calc.Input1, calc.Input2);
                calc.Input1 = Result;
            }
            return code;
        }
        public int OnMultiply(string input1, string input2)
        {
            int code = PrepareCalc(input1, input2);
            if (code == 0)
            {
                Result = calc.MultiplyNumbers(calc.Input1, calc.Input2);
                calc.Input1 = Result;

            }
            return code;
        }
        public int OnDivide(string input1, string input2)
        {
            int code = PrepareCalc(input1, input2);
            if (code == 0)
            {
                if (calc.Input2 != 0) //Prevents division with zero.
                {
                    Result = calc.DivideNumbers(calc.Input1, calc.Input2);
                    calc.Input1 = Result;

                }
                else
                {
                    code = 3;
                }
            }
            return code;
        }
        //When Equals button pressed
        public int OnEquals(int lastButtonClicked, string input1, string input2)
        {
            int code = 5;
            switch (lastButtonClicked) //Does the action depending on what button was clicked prior to this one.
            {
                case 0:
                    break;
                case 1:
                    code = OnAdd(input1, input2);
                    break;
                case 2:
                    code = OnSubtract(input1, input2);
                    break;
                case 3:
                    code = OnMultiply(input1, input2);
                    break;
                case 4:
                    code = OnDivide(input1, input2);
                    break;
            }
            return code;
        }
        public string FormError(int errCode) // Creates error message
        {
            if (errCode == 1)
            //Invalid input 1
            {
                return "Inmattningsfält innehåller otillåtna symboler! Om du har skrivit in ett bråktal, se till att använda komma och inte punkt!";
            }
            if (errCode == 2)
            {
                //invalid input 2
                return "Inmattningsfält innehåller otillåtna symboler! Om du har skrivit in ett bråktal, se till att använda komma och inte punkt!";
            }
            if (errCode == 3)
            {
                //Division with zero
                return "Du får ej dela med noll!";
            }
            else return "";
        }
        //Brings calculator back to initial state.
        public void ClearOutput()
        {
            calc.ClearData();
            Result = 0;
        }
    }
}
