using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalkylLotto
{
    class Calculator
    {
        public float Input1 { set; get; }
        public float Input2 { set; get; }

        public Calculator()
        {
            Input1 = 0;
            Input2 = 0;
        }
        public float addNumbers(float input1, float input2)
        {
            return input1 + input2;
        }
        public float subtractNumbers(float input1, float input2)
        {
            return input1 - input2;
        }
        public float multiplyNumbers(float input1, float input2)
        {
            return input1 * input2;
        }
        public float divideNumbers(float input1, float input2)
        {
                return input1 / input2;
        }

        public void clearData() {
            Input1 = 0;
            Input2 = 0;
        }
    }
}   