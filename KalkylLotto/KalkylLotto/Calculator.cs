namespace KalkylLotto
{
    class Calculator
    {
        //Inputs for calculation
        public float Input1 { set; get; }
        public float Input2 { set; get; }

        //Constructor
        public Calculator()
        {
            Input1 = 0;
            Input2 = 0;
        }

        //Addition
        public float AddNumbers(float input1, float input2)
        {
            return Input1 + Input2;
        }
        //Subtraction
        public float SubtractNumbers(float input1, float input2)
        {
            return Input1 - Input2;
        }
        //Multiplication
        public float MultiplyNumbers(float input1, float input2)
        {
            return Input1 * Input2;
        }
        //Division
        public float DivideNumbers(float input1, float input2)
        {
                return Input1 / Input2;
        }
        //Return to initial state
        public void ClearData() {
            Input1 = 0;
            Input2 = 0;
        }
    }
}