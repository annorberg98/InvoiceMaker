using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker
{
    /// <summary>
    /// Product class
    /// </summary>
    public class Product
    {
        private double price, tax;
        private double discunt = 0;
        public Product(string description, double price, double tax)
        {
            this.Description = description;
            this.Price = price;
            this.Tax = tax;
        }

        public Product(string description, string price, string tax)
        {
            this.Description = description;
            Double.TryParse(price, out this.price);
            Double.TryParse(tax, out this.tax);

        }

        public Product() { }

        public string Description { get; set; }

        public double Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        public double Tax
        {
            get { return this.tax; }
            set { this.tax = value; }
        }

        public int Quantity { get; set; }

        public double Discount
        {
            get { return this.discunt; }
            set { this.discunt = value; }
        }

        public double TotalPrice
        {
            get { return (Price * Quantity)-Discount; }
        }

        public double TotalTax
        {
            get { return Math.Round((Price * Quantity - Discount) * getTaxDecimal(), 2); }
        }

        private double getTaxDecimal()
        {
            return Tax / 100;
        }
    }
}
