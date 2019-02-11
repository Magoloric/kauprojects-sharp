using System.Windows;

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
            Close(); //Closes the window
        }

        //Creates and shows calculator window
        private void CalculatorButton_Click(object sender, RoutedEventArgs e)
        {
            CalculatorWindow cw = new CalculatorWindow();
            cw.Show();
        }

        //Creates and shows calculator windo
        private void LottoButton_Click(object sender, RoutedEventArgs e)
        {
            LottoWindow lw = new LottoWindow();
            lw.Show();
        }
    }
}
