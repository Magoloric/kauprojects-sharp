using System;
using System.Collections.Generic;  
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KalkylLotto
{
    /// <summary>
    /// Interaction logic for CalculatorWindow.xaml
    /// </summary>
    public partial class CalculatorWindow : Window
    {
        readonly CalculatorController calcCon = new CalculatorController();
        private string inputBuffer1 = null;
        private string inputBuffer2 = null;
        private int lastButtonClicked = 0;
        public CalculatorWindow()
        {
            InitializeComponent();
            this.DataContext = calcCon;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
                lastButtonClicked = 1;
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
                int code = calcCon.onAdd(inputBuffer1, inputBuffer2);
                if (code == 0)
                {
                    inputBuffer1 = calcCon.Result.ToString();
                    inputBuffer2 = null;
                }
                else
                {
                    string message = calcCon.formError(code);
                    string caption = "Felkod " + code;
                    MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                    inputField.Clear();
                    if (code == 1)
                    {
                        inputBuffer1 = inputBuffer2;
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
                int code = calcCon.onSubtract(inputBuffer1, inputBuffer2);
                if (code == 0)
                {
                    inputBuffer1 = calcCon.Result.ToString();
                    inputBuffer2 = null;
                }
                else
                {
                    string message = calcCon.formError(code);
                    string caption = "Felkod " + code;
                    MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                    inputField.Clear();
                    if (code == 1)
                    {
                        inputBuffer1 = null;
                    }
                    if (code == 2)
                    {
                        inputBuffer1 = inputBuffer2;
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
                int code = calcCon.onMultiply(inputBuffer1, inputBuffer2);
                if (code == 0)
                {
                    inputBuffer1 = calcCon.Result.ToString();
                    inputBuffer2 = null;
                }
                else
                {
                    string message = calcCon.formError(code);
                    string caption = "Felkod " + code;
                    MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                    inputField.Clear();
                    if (code == 1)
                    {
                        inputBuffer1 = null;
                    }
                    if (code == 2)
                    {
                        inputBuffer1 = inputBuffer2;
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
                int code = calcCon.onDivide(inputBuffer1, inputBuffer2);
                if (code == 0)
                {
                    inputBuffer1 = calcCon.Result.ToString();
                    inputBuffer2 = null;
                }
                else
                {
                    string message = calcCon.formError(code);
                    inputBuffer2 = null;
                    string caption = "Felkod " + code;
                    MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
                    inputField.Clear();
                    if (code == 1)
                    {
                        inputBuffer1 = null;
                    }
                    if (code == 2)
                    {
                        inputBuffer1 = inputBuffer2;
                        inputBuffer2 = null;
                    }
                }
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (inputField.Text != "" && inputBuffer1 != null && lastButtonClicked != 0)
            {

                calcCon.onEquals(lastButtonClicked, inputBuffer1, inputField.Text);
                inputBuffer1 = calcCon.Result.ToString();
            }
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            calcCon.clearOutput();
            inputBuffer1 = null;
            inputBuffer2 = null;
            inputField.Clear();
            resultField.Clear();
            lastButtonClicked = 0;
        }
    }

}
