using DTO;
using DataAccess.CRUDs;
using System.Collections.Generic;

namespace CoreApp
{
    public class ProductManager
    {
        private readonly ProductCrudFactory pCrud = new ProductCrudFactory();

        public void Create(Product product)
        {
            if (product.Price <= 0)
                throw new Exception("El precio debe ser mayor a 0.");
            if (product.Stock < 0)
                throw new Exception("El stock no puede ser negativo.");

            pCrud.Create(product);
        }

        public Product RetrieveById(int id)
        {
            return pCrud.RetrieveById<Product>(id);
        }

        public List<Product> RetrieveAll()
        {
            return pCrud.RetrieveAll<Product>();
        }

        public void Update(Product product)
        {
            pCrud.Update(product);
        }

    public void Delete(int productId)
    {
        var existingProduct = RetrieveById(productId);
        if (existingProduct != null)
        {
            pCrud.Delete(existingProduct);   
        }
        else
        {
            throw new Exception("Product not found");
        }
    }



    }
}
