using Observer.Items;
using System;

namespace Observer
{
    public class Investor : IInvestor
    {
        private string name;
        private Stock stock;

        public Investor(string name)
        {
            this.Name = name;
        }

        public void Update(Stock stock)
        {
            Console.WriteLine($"Notified {Name} of {stock.Symbol}'s " + $"change to {stock.Price} lv.");
        }

        // Gets or sets the stock
        public Stock Stock
        {
            get { return this.stock; }
            set { this.stock = value; }
        }

        public string Name {
            get { return this.name; }
            set { this.name = value; }
        }
    }
}
