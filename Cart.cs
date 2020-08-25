using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker
{
    /// <summary>
    /// Cart class that contains all the products
    /// </summary>
    public class Cart : ListManager<Product>
    {
        private double discount = 0;
        public Cart() { }

        public double TotalPrice => Math.Round(this.GetList().Sum(item => item.TotalPrice)-Discount, 2);
        public double TotalTax => Math.Round(this.GetList().Sum(item => item.TotalTax), 2);

        public double Discount
        {
            get { return this.discount; }
            set { this.discount = value; }
        }


    }
}
