using Data;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
public class SupplierService
{
    private readonly AppDbContext _context;

    public SupplierService(AppDbContext context)
    {
        _context = context;
    }

    // Create
    public void AddSupplier(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        _context.SaveChanges();
    }

    // Read
    public Supplier GetSupplier(int id)
    {
        return _context.Suppliers.Find(id);
    }

    // Update
    public void UpdateSupplier(Supplier supplier)
    {
        _context.Suppliers.Update(supplier);
        _context.SaveChanges();
    }

    // Delete
    public void DeleteSupplier(int id)
    {
        var supplier = _context.Suppliers.Find(id);
        if (supplier != null)
        {
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
        }
    }

    // List All
    public List<Supplier> GetAllSuppliers()
    {
        return _context.Suppliers.ToList();
    }
}
}
