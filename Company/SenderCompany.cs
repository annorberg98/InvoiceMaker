using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker
{
    /// <summary>
    /// SenderCompany inherits Company class and extends with Phonenumber and Website
    /// </summary>
    public class SenderCompany : Company
    {
        public SenderCompany(string name, Address address) : base(name, address) { }

        public SenderCompany(string name, Address address, string phone, string website) : base(name, address)
        {
            this.Phonenumber = phone;
            this.Website = website;
        }

        public string Phonenumber { get; set; }
        public string Website { get; set; }
    }
}
