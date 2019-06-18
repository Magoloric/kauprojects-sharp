using System.Windows;

namespace MediaIntegrator
{
    /// <summary>
    /// Interaction for MediaIntegrator
    /// </summary>
    public partial class MainWindow : Window
    {
        ConvertController cc;
        public MainWindow()
        {
            InitializeComponent();
            cc = new ConvertController();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            //Close the program
            Close();
        }

        private void ToXMLButton_Click(object sender, RoutedEventArgs e)
        {
            //Convert XML to CSV and return a message if it was successfull or not
            int result = cc.CSVtoXML();
            if (result == 1)
                {
                MessageBox.Show("Gick inte att konvertera filen till XML: Ingen fil att öppna!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            if (result == 2)
            {
                MessageBox.Show("Gick inte att konvertera filen till XML: CSV-filen är skadad eller ej formatterad som den ska!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            if (result == 0)
            {
                MessageBox.Show("Databasen har exporterats till XML!", "Klart!", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void ToCSVButton_Click(object sender, RoutedEventArgs e)
        {
            //Convert XML to CSV and return a message if it was successfull or not
            int result = cc.XMLtoCSV();
            if (result == 2)
            {
                MessageBox.Show("Gick inte att konvertera filen till CSV: Ingen fil att öppna!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            if (result == 1)
            {
                MessageBox.Show("Gick inte att konvertera filen till CSV: XML-filen är skadad eller ej formatterad som den ska!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            if (result == 0)
            {
                MessageBox.Show("Databasen har exporterats till CSV!", "Klart!", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }
    }
}
