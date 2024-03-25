using System.Collections.ObjectModel;
using System.ComponentModel;
using Models;
using Services;

namespace ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        private readonly ProductService _productService;

        public ProductViewModel(ProductService productService)
        {
            _productService = productService;

            var product = new Product { Name = "Very Long New Product", Quantity = 500000000, Description = "A new product description A new product description A new product description A new product description" };

            _productService.AddProduct(product);

            LoadProducts();
        }

        private void LoadProducts()
        {
            Products = new ObservableCollection<Product>(_productService.GetAllProducts());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}