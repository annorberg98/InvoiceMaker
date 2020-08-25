# InvoiceMaker 
InvoiceMaker is a program created as an assignment for a C#-course at Malmö University. 

At the moment, it simply loads data from a text-file and displays it as an Invoice. 
When invoice-data is loaded into the program, you can add discounts to the Invoice.

Run the application. To load a file, click `File > Open` and select the file. Select the DEMO.txt-file in the project files to see an example of how a data-file should look.

To add a discount, Click `Discount > Set Discount`. Then enter a number as discount, click `Ok` and watch the price become reduced.

Support for JSON-files will be implemented in the future.

## Creating you own data file

A data-file should be structured like the following:

```
Invoice Number
Start Date
Due Date
Company to which the invoice is to be sent
Name of a person to be addressed to
Street address
Zipcode
City
Country
Number of items
Description for Item 1 (not more than 1 row)
Quantity of item 1
Unit price for item 1
Tax in % for Item 1
Description for Item 2 (not more than 1 row)
Quantity of item 2
Unit price for item 2
Tax in % for Item 2
<Same for all Items>
Name of the Company (sender of invoice) 
Street 
Zipcode 
City 
Country 
Phone number 
Home page URL 
```