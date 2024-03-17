
namespace Services
{
    public interface ISearchService
    {
        object Search(string searchTerm);
    }

    public class SearchService : ISearchService
    {
        private readonly ProductService _productService;
        private readonly OrderService _orderService;
        private readonly ShipmentService _shipmentService;

        public SearchService(ProductService productService, OrderService orderService, ShipmentService shipmentService)
        {
            _productService = productService;
            _orderService = orderService;
            _shipmentService = shipmentService;
        }

        public object Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm) || searchTerm.Length < 2)
            {
                return null; // Consider appropriate error handling or result indication
            }

            var prefix = searchTerm.Substring(0, 1).ToUpper();
            if (!int.TryParse(searchTerm.Substring(1), out int id))
            {
                return null; // Consider appropriate error handling or result indication
            }

            switch (prefix)
            {
                case "I":
                    return _productService.GetProduct(id);
                case "O":
                    return _orderService.GetOrder(id); // Assuming similar implementation for orders
                case "S":
                    return _shipmentService.GetShipment(id); // Assuming similar implementation for shipments
                default:
                    return null; // Consider appropriate error handling or result indication
            }
        }
    }
}
