
namespace Shopstock
{
    /// <summary>
    /// View Model for stock
    /// </summary>
    public class StockModelView
    {
        StockController sc;

        public StockModelView()
        {
            Sc = new StockController(); //Creates stock controller

        }
        public StockController Sc { get => sc; set => sc = value; }
    }
}
