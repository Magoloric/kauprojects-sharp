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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KalkylLotto
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); //Closes the window
        }


        private void CalculatorButton_Click(object sender, RoutedEventArgs e)
        {
            CalculatorWindow cw = new CalculatorWindow();
            cw.Show();
        }

        private void LottoButton_Click(object sender, RoutedEventArgs e)
        {
            LottoWindow lw = new LottoWindow();
            lw.Show();
        }
    }
}
