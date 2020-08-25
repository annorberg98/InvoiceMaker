using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMaker.Invoices
{
    /// <summary>
    /// Invoice class
    /// </summary>
    public class Invoice
    { 
        public Invoice() { }

        public Invoice(string invNumber, SenderCompany sender, Company reciever, DateTime startDate, DateTime dueDate) 
        {
            this.InvoiceNumber = invNumber;
            this.SenderCompany = sender;
            this.RecieverCompany = reciever;
            this.StartDate = startDate;
            this.DueDate = dueDate;
        }

        public string InvoiceNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string UnknownDate { get; set; }

        public Company RecieverCompany { get; set; }
        public SenderCompany SenderCompany { get; set; }

        public int Items { get; set; }

        public Cart Cart { get; set; }

    }
}
