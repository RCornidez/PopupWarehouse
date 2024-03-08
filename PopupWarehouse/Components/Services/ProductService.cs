using Models;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace Services
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        // Create
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        // Read
        public Product GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        // Update
        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        // Delete
        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }

        // List All
        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }
    }
}
