using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InvoiceMaker.Invoices;
using Microsoft.Win32;

namespace InvoiceMaker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string openFileName = string.Empty;
        private Invoice invoice;
        private List<Changable> edited;
        public MainWindow()
        {
            InvoiceReader reader = new InvoiceReader();

            InitializeComponent();

            edited = new List<Changable>();

            MenuItemDiscount.IsEnabled = false;
            StartDatepicker.IsEnabled = false;
            DueDatepicker.IsEnabled = false;
        }

        /// <summary>
        /// Eventhandler for the BtnOpen in Menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files (*.txt) | *.txt";
            if (dialog.ShowDialog() == true)
                openFileName = dialog.FileName;

            if (!string.IsNullOrEmpty(openFileName))
            {
                InvoiceReader reader = new InvoiceReader();
                invoice = reader.CreateInvoiceFromFile(openFileName);

                MenuItemDiscount.IsEnabled = true;
                StartDatepicker.IsEnabled = true;
                DueDatepicker.IsEnabled = true;
                

            }

            UpdateGui();
        }

        /// <summary>
        /// Eventhandler for adding discount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDiscount_Click(object sender, RoutedEventArgs e)
        {
            InputWindow dialog = new InputWindow("Please enter Discount");
            if (dialog.ShowDialog() == true)
                invoice.Cart.Discount = dialog.Discount;

            UpdateGui();
        }

        /// <summary>
        /// Updates Gui with new values
        /// </summary>
        private void UpdateGui()
        {

            txtInvNum.Text = invoice.InvoiceNumber;
            StartDatepicker.SelectedDate = invoice.StartDate;
            DueDatepicker.SelectedDate = invoice.DueDate;
            lblInvSender.Content = invoice.SenderCompany.Name;

            txtAddress.Content = invoice.SenderCompany.Address.ToString();
            txtPhone.Content = invoice.SenderCompany.Phonenumber;
            txtWebpage.Content = invoice.SenderCompany.Website;

            CompanyToListBox();

            ProductsData();

            if(invoice.Cart.Discount <= 0)
            {
                MenuItemDiscountRemove.IsEnabled = false;
            } else
            {
                MenuItemDiscountRemove.IsEnabled = true;
            }
        }

        /// <summary>
        /// Sets Products in the ListView
        /// </summary>
        private void ProductsData()
        {
            lvProducts.Items.Clear();

            for (int i = 0; i < invoice.Cart.Count; i++)
            {
                lvProducts.Items.Insert(0, invoice.Cart.GetAt(i));
            }

            txtToPay.Content = invoice.Cart.TotalPrice;
            txtDiscount.Content = invoice.Cart.Discount;
        }

        /// <summary>
        /// Puts the Company data in the ListBox
        /// </summary>
        private void CompanyToListBox()
        {
            txtBoxInvAddress.Items.Clear();

            Company company = invoice.RecieverCompany;
            Address address = company.Address;
            txtBoxInvAddress.Items.Add(company.Name);
            txtBoxInvAddress.Items.Add(company.Person);
            txtBoxInvAddress.Items.Add(address.Street);
            txtBoxInvAddress.Items.Add($"{address.ZipCode} {address.City}");
            txtBoxInvAddress.Items.Add(address.Country);
        }

        /// <summary>
        /// Eventhandler for removing discount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemDiscountRemove_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to remove discount?", "Remove discount", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                invoice.Cart.Discount = 0;

            UpdateGui();
            return;
        }

        /// <summary>
        /// Eventhandler for changing startdate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartDatepicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StartDatepicker.SelectedDate != invoice.StartDate)
            {
                BtnSave_Changes.IsEnabled = true;
                BtnSave_Changes.Visibility = Visibility.Visible;

                edited.Add(Changable.StartDate);
            } else
            {
                BtnSave_Changes.IsEnabled = false;
                BtnSave_Changes.Visibility = Visibility.Hidden;
            }
            
        }

        /// <summary>
        /// Eventhandler for changing DueDate on Invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DueDatepicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DueDatepicker.SelectedDate != invoice.DueDate)
            {
                BtnSave_Changes.IsEnabled = true;
                BtnSave_Changes.Visibility = Visibility.Visible;

                edited.Add(Changable.DueDate);
            } else
            {
                BtnSave_Changes.IsEnabled = false;
                BtnSave_Changes.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Saves changed dates to the invoice object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Changes_Click(object sender, RoutedEventArgs e)
        {
            foreach(var item in edited)
            {
                switch (item)
                {
                    case Changable.StartDate:
                        Console.WriteLine($"Startdate: {invoice.StartDate} -> {StartDatepicker.SelectedDate}");
                        invoice.StartDate = (DateTime)StartDatepicker.SelectedDate;
                        break;
                    case Changable.DueDate:
                        Console.WriteLine($"DueDate: {invoice.DueDate} -> {DueDatepicker.SelectedDate}");
                        invoice.DueDate = (DateTime)DueDatepicker.SelectedDate;
                        break;
                    default:
                        break;
                }
            }
            edited.Clear();
            BtnSave_Changes.IsEnabled = false;
            BtnSave_Changes.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Eventhandler for adding an image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Select an image";
            dialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png";

            if (dialog.ShowDialog() == true)
                imgLogo.Source = new BitmapImage(new Uri(dialog.FileName));

        }
    }
}
