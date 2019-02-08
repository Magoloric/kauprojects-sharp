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
    /// Interaction logic for LottoWindow.xaml
    /// </summary>
    public partial class LottoWindow : Window
    {
        readonly LottoController Model = new LottoController();
        public LottoWindow()
        {
            InitializeComponent();
            this.DataContext = Model.lottery;
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
                                                        UserLottoNum7.Text});
            if (code == 0)
            {
                Model.InitiateLottery();
            }
            else
            {
                string message = Model.formError(code);
                string caption = "Felkod " + code;
                MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
