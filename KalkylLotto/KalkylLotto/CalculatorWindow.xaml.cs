using System.Windows;

namespace KalkylLotto
{
    /// <summary>
    /// Interaction logic for CalculatorWindow.xaml
    /// </summary>
    public partial class CalculatorWindow : Window
    {
        //Creates calculator controller
        readonly CalculatorController calcCon = new CalculatorController();
        //input buffers
        private string inputBuffer1 = null;
        private string inputBuffer2 = null;
        //For equals-button functionality
        private int lastButtonClicked = 0;

        //Initializes the window
        public CalculatorWindow()
        {
            InitializeComponent();
            DataContext = calcCon; //Sets data context, needed for dynamic result display. (See INotifyPropertyChanged)
        }

        //Following button clicks works the similar way
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //For equalsButton check.
            lastButtonClicked = 1;
            //If something's written in the input field and input buffer 1 is empty
            if (inputBuffer1 == null && inputField.Text != "")
            {
                //brings user input from the input field into buffer and clears the field
                inputBuffer1 = inputField.Text;
                inputField.Clear();
            }
            // Same but for second input buffer
            if (inputBuffer1 != null && inputBuffer2 == null && inputField.Text != "")
            {
                inputBuffer2 = inputField.Text;
            }
            // When both buffers are filled
            if (inputBuffer1 != null && inputBuffer2 != null)
            {
                int code = calcCon.OnAdd(inputBuffer1, inputBuffer2); //Launches the "wrapper"
                if (code == 0) //If everything's fine
                {
                    inputBuffer1 = calcCon.Result.ToString(); //Result goes to first input buffer for future actions.
                    inputBuffer2 = null; //Cleaned for future user input.
                }
                else //If error happened
                {
                    string message = calcCon.FormError(code); //gets the message from error message former
                    string caption = "Felkod " + code; //Caption for the window
                    //Show the error (see https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.messagebox?view=netframework-4.7.2)
                    MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                    inputField.Clear();
                    //Fixes the buffer depending on the error.
                    if (code == 1)
                    {
                        inputBuffer1 = null; 
                        inputBuffer2 = null; 
                    }
                    if (code == 2)
                    {
                        inputBuffer2 = null;
                    }
                }
            }
        }

        private void SubtractButton_Click(object sender, RoutedEventArgs e)
        {
                lastButtonClicked = 2;
            if (inputBuffer1 == null && inputField.Text != "")
            {
                inputBuffer1 = inputField.Text;
                inputField.Clear();
            }
            if (inputBuffer1 != null && inputBuffer2 == null && inputField.Text != "")
            {
                inputBuffer2 = inputField.Text;
            }
            if (inputBuffer1 != null && inputBuffer2 != null)
            {
                int code = calcCon.OnSubtract(inputBuffer1, inputBuffer2);
                if (code == 0)
                {
                    inputBuffer1 = calcCon.Result.ToString();
                    inputBuffer2 = null;
                }
                else
                {
                    string message = calcCon.FormError(code);
                    string caption = "Felkod " + code;
                    MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                    inputField.Clear();
                    if (code == 1)
                    {
                        inputBuffer1 = null;
                        inputBuffer2 = null;
                    }
                    if (code == 2)
                    {
                        inputBuffer2 = null;
                    }
                }
            }
        }

        private void MultiplyButton_Click(object sender, RoutedEventArgs e)
        {
                lastButtonClicked = 3;
            if (inputBuffer1 == null && inputField.Text != "")
            {
                inputBuffer1 = inputField.Text;
                inputField.Clear();
            }
            if (inputBuffer1 != null && inputBuffer2 == null && inputField.Text != "")
            {
                inputBuffer2 = inputField.Text;
            }
            if (inputBuffer1 != null && inputBuffer2 != null)
            {
                int code = calcCon.OnMultiply(inputBuffer1, inputBuffer2);
                if (code == 0)
                {
                    inputBuffer1 = calcCon.Result.ToString();
                    inputBuffer2 = null;
                }
                else
                {
                    string message = calcCon.FormError(code);
                    string caption = "Felkod " + code;
                    MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                    inputField.Clear();
                    if (code == 1)
                    {
                        inputBuffer1 = null;
                        inputBuffer2 = null;
                    }
                    if (code == 2)
                    {
                        inputBuffer2 = null;
                    }
                }
            }
        }

        private void DivideButton_Click(object sender, RoutedEventArgs e)
        {
            lastButtonClicked = 4;
            if (inputBuffer1 == null && inputField.Text != "")
            {
                inputBuffer1 = inputField.Text;
                inputField.Clear();
            }
            if (inputBuffer1 != null && inputBuffer2 == null && inputField.Text != "")
            {
                inputBuffer2 = inputField.Text;
            }
            if (inputBuffer1 != null && inputBuffer2 != null)
            {
                int code = calcCon.OnDivide(inputBuffer1, inputBuffer2);
                if (code == 0)
                {
                    inputBuffer1 = calcCon.Result.ToString();
                    inputBuffer2 = null;
                }
                else
                {
                    string message = calcCon.FormError(code);
                    inputBuffer2 = null;
                    string caption = "Felkod " + code;
                    MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                    inputField.Clear();
                    if (code == 1)
                    {
                        inputBuffer1 = null;
                        inputBuffer2 = null;
                    }
                    if (code == 2)
                    {
                        inputBuffer2 = null;
                    }
                }
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            //Works only if first input buffer contains something and input field is not empty.
            //Also, one of the action (+ - * /) buttons should've been clicked prior to this button. 
            if (inputField.Text != "" && inputBuffer1 != null && lastButtonClicked != 0)
            {

                calcCon.OnEquals(lastButtonClicked, inputBuffer1, inputField.Text);
                inputBuffer1 = calcCon.Result.ToString();
            }
        }
        //Returns calculator to initial state
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            calcCon.ClearOutput();
            inputBuffer1 = null;
            inputBuffer2 = null;
            inputField.Clear();
            resultField.Clear();
            lastButtonClicked = 0;
        }
    }

}
