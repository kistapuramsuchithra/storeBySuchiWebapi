//namespace WebApisforstorebySuchi.Interface
//{
//    public interface IProductDAO
//    {
//    }
//}
using WebApisforstorebySuchi.Common.Objects;
namespace WebApisforstorebySuchi.Interface
{
    public interface IProductDAO
    {
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<int> InsertProduct(ProductDTO product);
    }
}