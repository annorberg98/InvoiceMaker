using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker
{
    /// <summary>
    /// Company class
    /// </summary>
    public class Company
    {
        public Company(string name, Address address)
        {
            this.Name = name;
            this.Address = address;
        }


        public Company(string name, Address address, string contact) : this(name, address)
        {
            this.Person = contact;
        }

        public string Name { get; set; }
        public string Person { get; set; }
        public Address Address { get; set; }
    }
}
