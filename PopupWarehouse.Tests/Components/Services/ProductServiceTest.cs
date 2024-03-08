using NUnit.Framework;
using Data;
using Models;
using Services;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private AppDbContext _context;
        private ProductService _productService;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            // Setup DbContext with TestConnection
            _context = new AppDbContext("TestConnection");
            _context.Database.EnsureCreated(); // Ensure the database is created
            _productService = new ProductService(_context);

            // Clear the Products table to start with a clean state
            _context.Products.RemoveRange(_context.Products);
            _context.SaveChanges();
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            _context.Database.EnsureDeleted(); // Delete the database after tests
        }

        [SetUp]
        public void Setup()
        {
            _context.Products.RemoveRange(_context.Products);
            _context.SaveChanges();
        }

        [Test]
        public void ProductService_AddProduct()
        {
            // Arrange
            var product = new Product { Name = "New Product", Quantity = 50, Description = "A new product description" };
            
            // Act
            _productService.AddProduct(product);
            var addedProduct = _context.Products.FirstOrDefault(p => p.Name == "New Product");
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(addedProduct, Is.Not.Null);
                Assert.That(addedProduct.Name, Is.EqualTo("New Product"));
                Assert.That(addedProduct.Quantity, Is.EqualTo(50));
                Assert.That(addedProduct.Description, Is.EqualTo("A new product description"));
            });
        }

        [Test]
        public void ProductService_GetProduct()
        {
            // Arrange
            var product = new Product { Name = "Test Product", Quantity = 30, Description = "Test product description" };
            _context.Products.Add(product);
            _context.SaveChanges();
            
            // Act
            var retrievedProduct = _productService.GetProduct(product.Id);
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(retrievedProduct, Is.Not.Null);
                Assert.That(retrievedProduct.Id, Is.EqualTo(product.Id));
                Assert.That(retrievedProduct.Name, Is.EqualTo("Test Product"));
                Assert.That(retrievedProduct.Quantity, Is.EqualTo(30));
                Assert.That(retrievedProduct.Description, Is.EqualTo("Test product description"));
            });
        }

        [Test]
        public void ProductService_UpdateProduct()
        {
            // Arrange
            var product = new Product { Name = "Initial Product", Quantity = 100, Description = "Initial Description" };
            _context.Products.Add(product);
            _context.SaveChanges();
            product.Name = "Updated Product";
            product.Quantity = 200;
            product.Description = "Updated Description";
            
            // Act
            _productService.UpdateProduct(product);
            
            // Assert
            var updatedProduct = _context.Products.Find(product.Id);
            Assert.Multiple(() =>
            {
                Assert.That(updatedProduct.Name, Is.EqualTo("Updated Product"));
                Assert.That(updatedProduct.Quantity, Is.EqualTo(200));
                Assert.That(updatedProduct.Description, Is.EqualTo("Updated Description"));
            });
        }

        [Test]
        public void ProductService_DeleteProduct()
        {
            // Arrange
            var product = new Product { Name = "Product To Delete", Quantity = 50, Description = "Description of product to delete" };
            _context.Products.Add(product);
            _context.SaveChanges();
            
            // Act
            _productService.DeleteProduct(product.Id);
            
            // Assert
            var deletedProduct = _context.Products.Find(product.Id);
            Assert.That(deletedProduct, Is.Null);
        }

        [Test]
        public void ProductService_GetAllProducts()
        {
            // Arrange
            var product1 = new Product { Name = "Product 1", Quantity = 100, Description = "Description of Product 1" };
            var product2 = new Product { Name = "Product 2", Quantity = 200, Description = "Description of Product 2" };
            _context.Products.AddRange(product1, product2);
            _context.SaveChanges();
            
            // Act
            var allProducts = _productService.GetAllProducts();
            
            // Assert
            Assert.That(allProducts.Count, Is.EqualTo(2));
            Assert.That(allProducts, Has.One.EqualTo(product1).And.One.EqualTo(product2));
        }

    }
}
