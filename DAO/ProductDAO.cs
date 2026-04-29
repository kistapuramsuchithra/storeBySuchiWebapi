using Dapper;
using System.Data;
using WebApisforstorebySuchi.Common;
using WebApisforstorebySuchi.Common.Objects;

namespace WebApisforstorebySuchi.DAO
{
    public class ProductDAO : BaseDAO
    {
        public ProductDAO(IConfiguration configuration) : base(configuration)
        {
        }

        //  GET ALL PRODUCTS WITH IMAGES
        public List<ProductDTO> GetAllProducts()
        {
            var productDict = new Dictionary<int, ProductDTO>();

            string query = @"
                SELECT p.Id, p.Name, p.Price, p.OldPrice, p.Category, p.Description,
                       pi.ImageUrl
                FROM Products p
                LEFT JOIN ProductImages pi ON p.Id = pi.ProductId";

            using (var connection = CreateConnection())
            {
                var result = connection.Query<ProductDTO, string, ProductDTO>(
                    query,
                    (product, image) =>
                    {
                        if (!productDict.TryGetValue(product.Id, out var existing))
                        {
                            existing = product;
                            existing.Images = new List<string>();
                            productDict.Add(existing.Id, existing);
                        }

                        if (!string.IsNullOrEmpty(image))
                        {
                            existing.Images.Add(image);
                        }

                        return existing;
                    },
                    splitOn: "ImageUrl"
                ).ToList();
            }

            return productDict.Values.ToList();
        }

        //  INSERT PRODUCT + IMAGES
        public int InsertProduct(ProductDTO product)
        {
            string productQuery = @"
                INSERT INTO Products (Name, Price, OldPrice, Category, Description)
                VALUES (@Name, @Price, @OldPrice, @Category, @Description);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = CreateConnection())
            {
                var productId = connection.ExecuteScalar<int>(productQuery, product);

                if (product.Images != null && product.Images.Any())
                {
                    string imageQuery = @"
                        INSERT INTO ProductImages (ProductId, ImageUrl)
                        VALUES (@ProductId, @ImageUrl)";

                    foreach (var img in product.Images)
                    {
                        connection.Execute(imageQuery, new
                        {
                            ProductId = productId,
                            ImageUrl = img
                        });
                    }
                }

                return productId;
            }
        }

        //  DELETE PRODUCT
        public bool DeleteProduct(int productId)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Execute("DELETE FROM ProductImages WHERE ProductId = @Id", new { Id = productId });
                    connection.Execute("DELETE FROM Products WHERE Id = @Id", new { Id = productId });
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        //  UPDATE PRODUCT
        public bool UpdateProduct(ProductDTO product)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    string updateQuery = @"
                        UPDATE Products
                        SET Name = @Name,
                            Price = @Price,
                            OldPrice = @OldPrice,
                            Category = @Category,
                            Description = @Description
                        WHERE Id = @Id";

                    connection.Execute(updateQuery, product);

                    // 🔁 Replace Images (simple approach)
                    connection.Execute("DELETE FROM ProductImages WHERE ProductId = @Id", new { Id = product.Id });

                    if (product.Images != null && product.Images.Any())
                    {
                        string imageQuery = @"
                            INSERT INTO ProductImages (ProductId, ImageUrl)
                            VALUES (@ProductId, @ImageUrl)";

                        foreach (var img in product.Images)
                        {
                            connection.Execute(imageQuery, new
                            {
                                ProductId = product.Id,
                                ImageUrl = img
                            });
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}