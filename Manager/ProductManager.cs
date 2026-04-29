using WebApisforstorebySuchi.Common.Objects;
using WebApisforstorebySuchi.Interface;
using WebApisforstorebySuchi.DAO;

namespace WebApisforstorebySuchi.Manager
{
    public class ProductManager : IProductManager
    {
        private readonly ProductDAO _productDAO;

        public ProductManager(ProductDAO productDAO)
        {
            _productDAO = productDAO;
        }

        //  GET ALL PRODUCTS
        public List<ProductDTO> GetAllProducts()
        {
            return _productDAO.GetAllProducts();
        }

        //  ADD PRODUCT
        public int AddProduct(ProductDTO product)
        {
            return _productDAO.InsertProduct(product);
        }

         // UPDATE PRODUCT
        public bool UpdateProduct(ProductDTO product)
        {
            return _productDAO.UpdateProduct(product);
        }

        //  DELETE PRODUCT
        public bool DeleteProduct(int productId)
        {
            return _productDAO.DeleteProduct(productId);
        }
    }
}