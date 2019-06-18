using System.Collections.ObjectModel;
using System.Windows;

namespace Shopstock
{
    /// <summary>
    /// Window that displays Top-10 sold products in each category
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        StockModelView smv; //StockModelView from Stock Window.
        //Local top lists
        public ObservableCollection<Product> TopBooks;
        public ObservableCollection<Product> TopGames;
        public ObservableCollection<Product> TopMovies;
        public ObservableCollection<Product> TopMusic;

        public StatisticsWindow(StockModelView smv)
        {
            InitializeComponent();
            this.DataContext = this;
            this.smv = smv;
            //Get top 10 products per category
            TopBooks = smv.Sc.GetTop10Books();
            TopGames = smv.Sc.GetTop10Games();
            TopMovies = smv.Sc.GetTop10Movies();
            TopMusic = smv.Sc.GetTop10Music();
            //Binds to respective list
            TopBooksList.ItemsSource = TopBooks;
            TopGamesList.ItemsSource = TopGames;
            TopMoviesList.ItemsSource = TopMovies;
            TopMusicList.ItemsSource = TopMusic;

        }
    }
}
