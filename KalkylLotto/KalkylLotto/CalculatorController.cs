using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalkylLotto
{
    class CalculatorController : INotifyPropertyChanged
    {
        protected Calculator calc = new Calculator();
        private float result;
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Notify(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public float Result
        {
            get { return result; }
            set
            {
                if (value != result)
                { result = value; }
                Notify("Result");
            }
        }
        public int prepareCalc(string input1, string input2)
        {

            float inputFloat1, inputFloat2;
            if (float.TryParse(input1, out inputFloat1))
                {
                calc.Input1 = inputFloat1;
                if (float.TryParse(input2, out inputFloat2))
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
        }
        public int onAdd(string input1, string input2)
        {
            
            int code = prepareCalc(input1, input2);
            if (code == 0)
            {
                Result = calc.addNumbers(calc.Input1, calc.Input2);
                calc.Input1 = Result;
            }
            return code;

        }
        public int onSubtract(string input1, string input2)
        {
            int code = prepareCalc(input1, input2);
            if (code == 0)
            {
                Result = calc.subtractNumbers(calc.Input1, calc.Input2);
                calc.Input1 = Result;
            }
            return code;
        }
        public int onMultiply(string input1, string input2)
        {
            int code = prepareCalc(input1, input2);
            if (code == 0)
            {
                Result = calc.multiplyNumbers(calc.Input1, calc.Input2);
                calc.Input1 = Result;

            }
            return code;
        }
        public int onDivide(string input1, string input2)
        {
            int code = prepareCalc(input1, input2);
            if (code == 0)
            {
                if (calc.Input2 != 0)
                {
                    Result = calc.divideNumbers(calc.Input1, calc.Input2);
                    calc.Input1 = Result;
                   
                }
                else
                {
                    code = 3;
                }
            }
            return code;
        }
        public int onEquals (int lastButtonClicked, string input1, string input2)
        {
            int code = 5;
            switch(lastButtonClicked)
            {
                case 0:
                    break;
                case 1:
                    code = onAdd(input1, input2);
                    break;
                case 2:
                    code = onSubtract(input1, input2);
                    break;
                case 3:
                    code = onMultiply(input1, input2);
                    break;
                case 4:
                    code = onDivide(input1, input2);
                    break;  
            }
            return code;
        }
        public string formError(int errCode)
        {
            if (errCode == 1)
            {
                return "Inmattningsfält innehåller otillåtna symboler! Om du har skrivit in ett bråktal, se till att använda komma och inte punkt!";
            }
            if (errCode == 2)
            {
                return "Inmattningsfält innehåller otillåtna symboler! Om du har skrivit in ett bråktal, se till att använda komma och inte punkt!";
            }
            if (errCode == 3)
            {
                return "Du får ej dela med noll!";
            }
            else return "";
        }
        public void clearOutput()
        {
            calc.clearData();
            Result = 0;
        }
    }
}
