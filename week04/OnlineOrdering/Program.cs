using System;
using System.Collections.Generic;
using System.Text;

// Address class to manage customer addresses
public class Address
{
    private string _streetAddress;
    private string _city;
    private string _stateProvince;
    private string _country;

    // Constructor
    public Address(string streetAddress, string city, string stateProvince, string country)
    {
        _streetAddress = streetAddress;
        _city = city;
        _stateProvince = stateProvince;
        _country = country;
    }

    // Properties
    public string StreetAddress
    {
        get { return _streetAddress; }
        set { _streetAddress = value; }
    }

    public string City
    {
        get { return _city; }
        set { _city = value; }
    }

    public string StateProvince
    {
        get { return _stateProvince; }
        set { _stateProvince = value; }
    }

    public string Country
    {
        get { return _country; }
        set { _country = value; }
    }

    // Method to check if address is in USA
    public bool IsInUSA()
    {
        return _country.ToUpper() == "USA" || _country.ToUpper() == "UNITED STATES";
    }

    // Method to return formatted address string
    public string GetFormattedAddress()
    {
        return $"{_streetAddress}\n{_city}, {_stateProvince}\n{_country}";
    }
}

// Customer class to manage customer information
public class Customer
{
    private string _name;
    private Address _address;

    // Constructor
    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    // Properties
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public Address Address
    {
        get { return _address; }
        set { _address = value; }
    }

    // Method to check if customer lives in USA
    public bool LivesInUSA()
    {
        return _address.IsInUSA();
    }
}

// Product class to manage individual products
public class Product
{
    private string _name;
    private string _productId;
    private decimal _price;
    private int _quantity;

    // Constructor
    public Product(string name, string productId, decimal price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    // Properties
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string ProductId
    {
        get { return _productId; }
        set { _productId = value; }
    }

    public decimal Price
    {
        get { return _price; }
        set { _price = value; }
    }

    public int Quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }

    // Method to calculate total cost for this product
    public decimal GetTotalCost()
    {
        return _price * _quantity;
    }
}

// Order class to manage orders with products and customers
public class Order
{
    private List<Product> _products;
    private Customer _customer;

    // Constructor
    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    // Properties
    public Customer Customer
    {
        get { return _customer; }
        set { _customer = value; }
    }

    public List<Product> Products
    {
        get { return _products; }
    }

    // Method to add a product to the order
    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    // Method to calculate total cost of the order
    public decimal CalculateTotalCost()
    {
        decimal productTotal = 0;
        
        foreach (Product product in _products)
        {
            productTotal += product.GetTotalCost();
        }

        // Add shipping cost based on customer location
        decimal shippingCost = _customer.LivesInUSA() ? 5.00m : 35.00m;
        
        return productTotal + shippingCost;
    }

    // Method to generate packing label
    public string GetPackingLabel()
    {
        StringBuilder label = new StringBuilder();
        label.AppendLine("PACKING LABEL");
        label.AppendLine("=============");
        
        foreach (Product product in _products)
        {
            label.AppendLine($"{product.Name} (ID: {product.ProductId})");
        }
        
        return label.ToString();
    }

    // Method to generate shipping label
    public string GetShippingLabel()
    {
        StringBuilder label = new StringBuilder();
        label.AppendLine("SHIPPING LABEL");
        label.AppendLine("==============");
        label.AppendLine($"{_customer.Name}");
        label.AppendLine(_customer.Address.GetFormattedAddress());
        
        return label.ToString();
    }
}

// Main program class
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("=== Product Ordering System ===\n");

        // Create first customer (USA)
        Address address1 = new Address("123 Main Street", "Seattle", "WA", "USA");
        Customer customer1 = new Customer("John Smith", address1);

        // Create first order
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Wireless Headphones", "WH-001", 89.99m, 1));
        order1.AddProduct(new Product("USB Cable", "USB-123", 12.50m, 2));
        order1.AddProduct(new Product("Phone Case", "PC-456", 24.99m, 1));

        // Create second customer (International)
        Address address2 = new Address("456 Oak Avenue", "Toronto", "ON", "Canada");
        Customer customer2 = new Customer("Sarah Johnson", address2);

        // Create second order
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Bluetooth Speaker", "BS-789", 149.99m, 1));
        order2.AddProduct(new Product("Charging Pad", "CP-321", 39.99m, 1));

        // Display Order 1 Information
        Console.WriteLine("ORDER #1");
        Console.WriteLine("========");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order1.CalculateTotalCost():F2}");
        Console.WriteLine();

        // Display Order 2 Information
        Console.WriteLine("ORDER #2");
        Console.WriteLine("========");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order2.CalculateTotalCost():F2}");
        Console.WriteLine();

        // Display detailed breakdown for demonstration
        Console.WriteLine("=== ORDER DETAILS ===");
        Console.WriteLine();
        
        Console.WriteLine("Order 1 Details:");
        Console.WriteLine($"Customer: {order1.Customer.Name}");
        Console.WriteLine($"Lives in USA: {order1.Customer.LivesInUSA()}");
        decimal order1ProductTotal = 0;
        foreach (Product product in order1.Products)
        {
            decimal productCost = product.GetTotalCost();
            order1ProductTotal += productCost;
            Console.WriteLine($"  {product.Name}: ${product.Price:F2} x {product.Quantity} = ${productCost:F2}");
        }
        decimal order1Shipping = order1.Customer.LivesInUSA() ? 5.00m : 35.00m;
        Console.WriteLine($"Product Subtotal: ${order1ProductTotal:F2}");
        Console.WriteLine($"Shipping: ${order1Shipping:F2}");
        Console.WriteLine($"Total: ${order1.CalculateTotalCost():F2}");
        Console.WriteLine();

        Console.WriteLine("Order 2 Details:");
        Console.WriteLine($"Customer: {order2.Customer.Name}");
        Console.WriteLine($"Lives in USA: {order2.Customer.LivesInUSA()}");
        decimal order2ProductTotal = 0;
        foreach (Product product in order2.Products)
        {
            decimal productCost = product.GetTotalCost();
            order2ProductTotal += productCost;
            Console.WriteLine($"  {product.Name}: ${product.Price:F2} x {product.Quantity} = ${productCost:F2}");
        }
        decimal order2Shipping = order2.Customer.LivesInUSA() ? 5.00m : 35.00m;
        Console.WriteLine($"Product Subtotal: ${order2ProductTotal:F2}");
        Console.WriteLine($"Shipping: ${order2Shipping:F2}");
        Console.WriteLine($"Total: ${order2.CalculateTotalCost():F2}");

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}