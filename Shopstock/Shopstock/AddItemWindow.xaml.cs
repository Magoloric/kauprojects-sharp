using System.Windows;
using System.Windows.Controls;

namespace Shopstock
{
    /// <summary>
    /// Window for adding new items to stock
    /// </summary>
    public partial class AddItemWindow : Window
    {
        StockModelView smv; //StockViewModel, sends from Stock Window.
        public AddItemWindow(StockModelView smv) //Constructor
        {
            this.smv = smv;
            InitializeComponent();
        }

        //Shows and hides relevant extra parameter boxes depending on the selected type. In order to be able to press OK
        //You'll need to choose a type in combo box.
        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MusicInfoGrid.Visibility = TypeComboBox.SelectedIndex == 0 ? Visibility.Visible : Visibility.Hidden;
            MovieInfoGrid.Visibility = TypeComboBox.SelectedIndex == 1 ? Visibility.Visible : Visibility.Hidden;
            GameInfoGrid.Visibility = TypeComboBox.SelectedIndex == 2 ? Visibility.Visible : Visibility.Hidden;
            BookInfoGrid.Visibility = TypeComboBox.SelectedIndex == 3 ? Visibility.Visible : Visibility.Hidden;
            OKButton.IsEnabled = true;
        }

        //Handles the click on OK button
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            //Checks if all fields are filled. Only relevant extra fields for current type are checked.
            if (string.IsNullOrWhiteSpace(ItemNameField.Text) ||
                string.IsNullOrWhiteSpace(ItemNrField.Text) ||
                string.IsNullOrWhiteSpace(PriceField.Text) ||
                string.IsNullOrWhiteSpace(TitleField.Text) ||
                string.IsNullOrWhiteSpace(GenreField.Text) ||
                string.IsNullOrWhiteSpace(YearField.Text) ||
                string.IsNullOrWhiteSpace(PublisherField.Text) ||
                (TypeComboBox.SelectedIndex == 0 && (string.IsNullOrWhiteSpace(ArtistField.Text) ||
                string.IsNullOrWhiteSpace(DurationField.Text) || string.IsNullOrWhiteSpace(NrOfTracksField.Text))) ||
                (TypeComboBox.SelectedIndex == 1 && (string.IsNullOrWhiteSpace(MovieStudioField.Text) ||
                string.IsNullOrWhiteSpace(NrOfEpisodesField.Text))) ||
                (TypeComboBox.SelectedIndex == 2 && (string.IsNullOrWhiteSpace(GameStudioField.Text) ||
                string.IsNullOrWhiteSpace(PlatformField.Text))) ||
                (TypeComboBox.SelectedIndex == 3 && (string.IsNullOrWhiteSpace(AuthorField.Text) ||
                string.IsNullOrWhiteSpace(ISBNField.Text) || string.IsNullOrWhiteSpace(NrOfPagesField.Text))))
            {
                //Display error if empty fields detected.
                MessageBox.Show("Alla fält måsta vara ifyllda. Kontrollera det och försök igen!", "Fel!", MessageBoxButton.OK);
            }
            else
            {
                //Try to read in number values
                if (int.TryParse(ItemNrField.Text, out int itemNr) == true &&
                float.TryParse(PriceField.Text, out float price) == true &&
                int.TryParse(YearField.Text, out int year) == true)
                {
                    //Check if item with this number already exists
                    if (smv.Sc.FindByItemNr(itemNr) == -1)
                    {
                        //Checks what type is chosen
                        if (TypeComboBox.SelectedIndex == 0)
                        {
                            if (int.TryParse(NrOfTracksField.Text, out int nrOfTracks) == true)
                            {
                                //Add the item of that type to stock and close the window
                                smv.Sc.AddMusic(itemNr, price, ItemNameField.Text, 0, TitleField.Text, GenreField.Text, year, PublisherField.Text, ArtistField.Text, DurationField.Text, nrOfTracks);
                                MessageBox.Show("Artikeln har lagts till!", "Klart!", MessageBoxButton.OK, MessageBoxImage.Information);
                                Close();
                            }
                            else
                            {
                                //Shown if number fields couldn't be read.
                                MessageBox.Show("Se till att du fyllde endast tal där det är nödvändigt!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else if (TypeComboBox.SelectedIndex == 1)
                        {
                            if (int.TryParse(NrOfEpisodesField.Text, out int nrOfEpisodes) == true)
                            {
                                smv.Sc.AddMovie(itemNr, price, ItemNameField.Text, 0, TitleField.Text, GenreField.Text, year, PublisherField.Text, MovieStudioField.Text, nrOfEpisodes);
                                MessageBox.Show("Artikeln har lagts till!", "Klart!", MessageBoxButton.OK, MessageBoxImage.Information);
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Se till att du fyllde endast tal där det är nödvändigt!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else if (TypeComboBox.SelectedIndex == 2)
                        {
                            smv.Sc.AddGame(itemNr, price, ItemNameField.Text, 0, TitleField.Text, GenreField.Text, year, PublisherField.Text, GameStudioField.Text, PlatformField.Text);
                            MessageBox.Show("Artikeln har lagts till!", "Klart!", MessageBoxButton.OK, MessageBoxImage.Information);
                            Close();
                        }
                        else if (TypeComboBox.SelectedIndex == 3)
                        {
                            if (int.TryParse(NrOfPagesField.Text, out int nrOfPages) == true)
                            {
                                smv.Sc.AddBook(itemNr, price, ItemNameField.Text, 0, TitleField.Text, GenreField.Text, year, PublisherField.Text, AuthorField.Text, ISBNField.Text, nrOfPages);
                                MessageBox.Show("Artikeln har lagts till!", "Klart!", MessageBoxButton.OK, MessageBoxImage.Information);
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("Se till att du fyllde endast tal där det är nödvändigt!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            //Just in case type is not chosen but OK is pressed. Not used at the moment.
                            MessageBox.Show("Oj då! Något gick fel!", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        //If item with this item number already exists
                        MessageBox.Show("Artikeln med den här artikelnummer finns redan!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    //Shown if number fields couldn't be read.
                    MessageBox.Show("Se till att du fyllde endast tal där det är nödvändigt!", "Fel!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        //Closes window without adding anything.
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
