using BigCommerceNET.Models.Category;
using BigCommerceNET.Models.Product;
using TestProject;

BigCommerceStoreAccess BCAccess = new BigCommerceStoreAccess();
List<BigCommerceCategory> categories = BCAccess.GetCategories();
List<BigCommerceProduct> products = await BCAccess.GetProducts();

Console.WriteLine("Categories:");
foreach (var category in categories)
{
    Console.WriteLine($"Category: {category.Category_Name}");
}

Console.WriteLine();
Console.WriteLine("Products:");

foreach (var product in products)
{    

    Console.WriteLine($"Product: {product.Name}");
}

Console.ReadLine();
