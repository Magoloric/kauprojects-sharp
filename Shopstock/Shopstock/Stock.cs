using System.Collections.ObjectModel;

namespace Shopstock
{
    ///<summary>
    ///Contains main stock
    ///</summary>
    public class Stock
    {
        ObservableCollection<Product> container;

        public Stock()
        {
            Container = new ObservableCollection<Product>(); //Stock container
        }

        public ObservableCollection<Product> Container { get => this.container; set => this.container = value; }
    }
}
