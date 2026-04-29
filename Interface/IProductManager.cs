using WebApisforstorebySuchi.Common.Objects;

namespace WebApisforstorebySuchi.Interface
{
    public interface IProductManager
    {
        List<ProductDTO> GetAllProducts();
        int AddProduct(ProductDTO product);
        bool UpdateProduct(ProductDTO product);
        bool DeleteProduct(int productId);
    }
}