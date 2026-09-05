using Business.Photon.Core;
using Data.Photon.Models.Models;

namespace Photon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            PhotonCore photon = new PhotonCore();

            SeedCustomers(photon);
            SeedAddresses(photon);
            SeedProducts(photon);
            SeedOrders(photon);
            SeedOrderItems(photon);

            RunRetrieveExamples(photon);
            RunUpdateExample(photon);
            RunRemoveExample(photon);

            Console.ReadKey();
        }

        #region Private Methods
        /// <summary>
        /// Adds example customer data to Photon.
        /// </summary>
        private static void SeedCustomers(
            PhotonCore photon)
        {
            photon.Add(new Customer
            {
                CustomerId = 1,
                FirstName = "Eric",
                LastName = "Reyneke",
                Email = "eric.reyneke@example.com",
                CreatedDate = new DateTime(2026, 1, 15)
            });

            photon.Add(new Customer
            {
                CustomerId = 2,
                FirstName = "John",
                LastName = "Smith",
                Email = "john.smith@example.com",
                CreatedDate = new DateTime(2026, 2, 10)
            });

            photon.Add(new Customer
            {
                CustomerId = 3,
                FirstName = "Sarah",
                LastName = "Williams",
                Email = "sarah.williams@example.com",
                CreatedDate = new DateTime(2026, 3, 5)
            });

            photon.Add(new Customer
            {
                CustomerId = 4,
                FirstName = "Michael",
                LastName = "Brown",
                Email = "michael.brown@example.com",
                CreatedDate = new DateTime(2026, 4, 21)
            });
        }

        /// <summary>
        /// Adds example address data to Photon.
        /// </summary>
        private static void SeedAddresses(
            PhotonCore photon)
        {
            photon.Add(new Address
            {
                AddressId = 1,
                CustomerId = 1,
                AddressLine1 = "12 Example Street",
                City = "Pretoria",
                Province = "Gauteng",
                PostalCode = "0081",
                Country = "South Africa"
            });

            photon.Add(new Address
            {
                AddressId = 2,
                CustomerId = 2,
                AddressLine1 = "45 Main Road",
                City = "Johannesburg",
                Province = "Gauteng",
                PostalCode = "2000",
                Country = "South Africa"
            });

            photon.Add(new Address
            {
                AddressId = 3,
                CustomerId = 3,
                AddressLine1 = "8 Ocean View",
                City = "Cape Town",
                Province = "Western Cape",
                PostalCode = "8001",
                Country = "South Africa"
            });

            photon.Add(new Address
            {
                AddressId = 4,
                CustomerId = 4,
                AddressLine1 = "101 Market Street",
                City = "Durban",
                Province = "KwaZulu-Natal",
                PostalCode = "4001",
                Country = "South Africa"
            });
        }

        /// <summary>
        /// Adds example product data to Photon.
        /// </summary>
        private static void SeedProducts(
            PhotonCore photon)
        {
            photon.Add(new Product
            {
                ProductId = 1,
                Name = "RTX 9070 XT",
                Description = "High performance graphics card.",
                Price = 14999.99M,
                StockQuantity = 10,
                IsActive = true
            });

            photon.Add(new Product
            {
                ProductId = 2,
                Name = "Ryzen 7 5700X",
                Description = "Eight core desktop processor.",
                Price = 3999.99M,
                StockQuantity = 25,
                IsActive = true
            });

            photon.Add(new Product
            {
                ProductId = 3,
                Name = "32GB DDR4 RAM",
                Description = "Dual-channel desktop memory kit.",
                Price = 1799.99M,
                StockQuantity = 50,
                IsActive = true
            });

            photon.Add(new Product
            {
                ProductId = 4,
                Name = "1TB NVMe SSD",
                Description = "High-speed NVMe solid state drive.",
                Price = 1299.99M,
                StockQuantity = 0,
                IsActive = false
            });
        }

        /// <summary>
        /// Adds example order data to Photon.
        /// </summary>
        private static void SeedOrders(
            PhotonCore photon)
        {
            photon.Add(new Order
            {
                OrderId = 1,
                CustomerId = 1,
                OrderDate = new DateTime(2026, 8, 1),
                TotalAmount = 3599.98M,
                Status = "Completed"
            });

            photon.Add(new Order
            {
                OrderId = 2,
                CustomerId = 2,
                OrderDate = new DateTime(2026, 8, 18),
                TotalAmount = 14999.99M,
                Status = "Processing"
            });

            photon.Add(new Order
            {
                OrderId = 3,
                CustomerId = 1,
                OrderDate = new DateTime(2026, 9, 2),
                TotalAmount = 1299.99M,
                Status = "Processing"
            });

            photon.Add(new Order
            {
                OrderId = 4,
                CustomerId = 3,
                OrderDate = new DateTime(2026, 9, 4),
                TotalAmount = 5799.98M,
                Status = "Completed"
            });
        }

        /// <summary>
        /// Adds example order item data to Photon.
        /// </summary>
        private static void SeedOrderItems(
            PhotonCore photon)
        {
            photon.Add(new OrderItem
            {
                OrderItemId = 1,
                OrderId = 1,
                ProductId = 3,
                Quantity = 2,
                UnitPrice = 1799.99M
            });

            photon.Add(new OrderItem
            {
                OrderItemId = 2,
                OrderId = 2,
                ProductId = 1,
                Quantity = 1,
                UnitPrice = 14999.99M
            });

            photon.Add(new OrderItem
            {
                OrderItemId = 3,
                OrderId = 3,
                ProductId = 4,
                Quantity = 1,
                UnitPrice = 1299.99M
            });

            photon.Add(new OrderItem
            {
                OrderItemId = 4,
                OrderId = 4,
                ProductId = 2,
                Quantity = 1,
                UnitPrice = 3999.99M
            });

            photon.Add(new OrderItem
            {
                OrderItemId = 5,
                OrderId = 4,
                ProductId = 3,
                Quantity = 1,
                UnitPrice = 1799.99M
            });
        }

        /// <summary>
        /// Runs example retrieval operations against Photon.
        /// </summary>
        private static void RunRetrieveExamples(
            PhotonCore photon)
        {
            Customer customer =
                photon.Retrieve<Customer>(1);

            Console.WriteLine(
                $"Retrieved Customer: {customer.FirstName} {customer.LastName}");

            bool productExists =
                photon.ContainsKey<Product>(2);

            Console.WriteLine(
                $"Product 2 Exists: {productExists}");

            bool customerFound =
                photon.TryRetrieve<Customer>(
                    3,
                    out Customer foundCustomer);

            if (customerFound)
                Console.WriteLine(
                    $"TryRetrieve Customer: {foundCustomer.FirstName} {foundCustomer.LastName}");

            IReadOnlyCollection<Product> products =
                photon.RetrieveAll<Product>();

            Console.WriteLine();
            Console.WriteLine("All Products:");

            foreach (Product product in products)
            {
                Console.WriteLine(
                    $"{product.ProductId} - {product.Name} - R{product.Price}");
            }
        }

        /// <summary>
        /// Runs an example update operation against Photon.
        /// </summary>
        private static void RunUpdateExample(
            PhotonCore photon)
        {
            Product product =
                photon.Retrieve<Product>(4);

            product.StockQuantity = 15;
            product.IsActive = true;

            photon.Update(product);

            Product updatedProduct =
                photon.Retrieve<Product>(4);

            Console.WriteLine();
            Console.WriteLine(
                $"Updated Product: {updatedProduct.Name} - Stock: {updatedProduct.StockQuantity} - Active: {updatedProduct.IsActive}");
        }

        /// <summary>
        /// Runs an example remove operation against Photon.
        /// </summary>
        private static void RunRemoveExample(
            PhotonCore photon)
        {
            photon.Remove<Address>(4);

            bool addressExists =
                photon.ContainsKey<Address>(4);

            Console.WriteLine();
            Console.WriteLine(
                $"Address 4 Exists After Removal: {addressExists}");
        }
        #endregion
    }
}