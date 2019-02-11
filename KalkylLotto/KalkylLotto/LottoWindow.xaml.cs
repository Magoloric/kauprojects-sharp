using System.Windows;

namespace KalkylLotto
{
    /// <summary>
    /// Interaction logic for LottoWindow.xaml
    /// </summary>
    public partial class LottoWindow : Window
    {
        readonly LottoController Model = new LottoController(); //Creates lotto controller
        public LottoWindow()
        {
            InitializeComponent();
            DataContext = Model.lottery; //Sets data context for dynamic result display
        }

        private void StartLottoButton_Click(object sender, RoutedEventArgs e)
        {
               int code = Model.PrepareLottery(NrOfDrawsField.Text,
                                               new string[]{UserLottoNum1.Text,
                                                        UserLottoNum2.Text,
                                                        UserLottoNum3.Text,
                                                        UserLottoNum4.Text,
                                                        UserLottoNum5.Text,
                                                        UserLottoNum6.Text,
                                                        UserLottoNum7.Text}); //Gets the input and parses it
            if (code == 0) //if all input is valid
            {
                Model.InitiateLottery(); //Starts lottery
            }
            else
            {
                string message = Model.FormError(code); //Creates the error message
                string caption = "Felkod " + code; //Caption
                //Shows the error
                MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
