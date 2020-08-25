using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker
{
    /// <summary>
    /// Address class
    /// </summary>
    public class Address
    {
        private string street, zip, city, country;

        public Address(string street, string zipcode, string city, string country)
        {
            this.street = street;
            this.zip = zipcode;
            this.city = city;
            this.country = country;
        }

        public string Street
        {
            get { return this.street; }
        }

        public string ZipCode
        {
            get { return this.zip; }
        }

        public string City
        {
            get { return this.city; }
        }

        public string Country
        {
            get { return this.country; }
        }

        public override string ToString()
        {
            return $"{Street}\n{ZipCode} {City}\n{Country}";
        }
    }
}
