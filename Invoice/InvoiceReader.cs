using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace InvoiceMaker.Invoices
{
    public class InvoiceReader
    {
        private Invoice inv;
        private const int LinesPerProduct = 4;

        /// <summary>
        /// Reads all lines of specified file into an array
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string[] ReadInvoiceFile(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        /// <summary>
        /// Creates an Invoice from the specified file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public Invoice CreateInvoiceFromFile(string filePath)
        {
            DateTime invDate, dueDate;

            string[] data = InvoiceReader.ReadInvoiceFile(filePath);

            if (!DateTime.TryParse(data[1], out invDate))
            {
                Console.WriteLine("Kunde inte läsa startdatum");
            }

            if (!DateTime.TryParse(data[2], out dueDate))
            {
                Console.WriteLine("Kunde inte läsa slutdatum");
            }

            string recieverPerson = data[4];
            Company reciever = new Company(data[3], new Address(data[5], data[6], data[7], data[8]));
            reciever.Person = recieverPerson;

            int numItems;
            Int32.TryParse(data[9], out numItems);

            List<string> products = SplitArray(data, 10, numItems * 4);

            Cart cart = GetProductsFromArray(products, numItems);

            List<string> withoutProducts = data.ToList<string>();
            withoutProducts.RemoveRange(9, (numItems * 4)+1);

            data = withoutProducts.ToArray();

            SenderCompany senderCompany = new SenderCompany(data[9], new Address(data[10], data[11], data[12], data[13]));

            string phone = data[14];
            string homepage = data[15];

            senderCompany.Phonenumber = phone;
            senderCompany.Website = homepage;

            inv = new Invoice(data[0], senderCompany, reciever, invDate, dueDate);
            inv.Cart = cart;

            return this.inv;
        }

        /// <summary>
        /// Splits an array
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private List<string> SplitArray(string[] array, int index, int amount)
        {
            List<string> result = new List<string>();
            string[] temp = new string[amount];
            Array.Copy(array, index, temp, 0, amount);

            result.AddRange(temp);

            return result;
        }

        /// <summary>
        /// Creates a Cart object with all products
        /// </summary>
        /// <param name="input"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private Cart GetProductsFromArray(List<string> input, int amount)
        {
            var cart = new Cart();
            for (int i = 0; i < input.Count; i += LinesPerProduct)
            {
                var temp = new Product();
                temp.Description = input[i];
                temp.Quantity = Convert.ToInt32(input[i + 1]);
                temp.Price = Convert.ToDouble(input[i + 2], CultureInfo.InvariantCulture);
                temp.Tax = Convert.ToDouble(input[i + 3]);
                
                cart.Add(temp);
            }
            return cart;
        }

    }
}
