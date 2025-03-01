using DTO;
using System;
using DataAccess.DAOs;
using System.Collections.Generic;

namespace DataAccess.CRUDs
{
    public class ProductCrudFactory : CrudFactory
    {
        protected readonly SqlDao sqlDao = SqlDao.GetInstance();

        
        public override void Create(BaseDTO dto)
        {
            if (dto is not Product product) 
                throw new ArgumentException("El objeto no es de tipo Product.");

            var sqlOperation = new SqlOperation { ProcedureName = "CRE_PRODUCT_PR" };

            sqlOperation.AddStringParameter("P_PRODUCT_CODE", product.ProductCode);
            sqlOperation.AddStringParameter("P_NAME", product.Name);
            sqlOperation.AddStringParameter("P_CATEGORY", product.Category);
            sqlOperation.AddDoubleParam("P_PRICE", product.Price);
            sqlOperation.AddIntParameter("P_STOCK", product.Stock);

            sqlDao.ExecuteProcedure(sqlOperation);
        }


        public override void Update(BaseDTO dto)
        {
            if (dto is not Product product)
                throw new ArgumentException("El objeto no es de tipo Product.");

            var sqlOperation = new SqlOperation { ProcedureName = "UPD_PRODUCT_PR" };

            sqlOperation.AddStringParameter("P_OLD_PRODUCT_CODE", product.ProductCode);
            sqlOperation.AddStringParameter("P_NEW_PRODUCT_CODE", product.ProductCode); 
            sqlOperation.AddStringParameter("P_NAME", product.Name);
            sqlOperation.AddStringParameter("P_CATEGORY", product.Category);
            sqlOperation.AddDoubleParam("P_PRICE", product.Price);
            sqlOperation.AddIntParameter("P_STOCK", product.Stock);

            sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO dto)
        {
            if (dto is not Product product)
            {
                Console.WriteLine("❌ Error: El DTO proporcionado no es un producto.");
                return;
            }

            var sqlOperation = new SqlOperation { ProcedureName = "DEL_PRODUCT_PR" };
            sqlOperation.AddIntParameter("P_PRODUCT_ID", product.Id);

            sqlDao.ExecuteProcedure(sqlOperation);
            Console.WriteLine("✅ Producto eliminado exitosamente.");
        }

        public override T Retrieve<T>(BaseDTO dto)
        {
            throw new NotImplementedException("Retrieve<T> aún no está implementado.");
        }

       
        public override T RetrieveById<T>(int id)
        {
            var sqlOperation = new SqlOperation { ProcedureName = "GET_PRODUCT_BY_ID_PR" };
            sqlOperation.AddIntParameter("@ID", id);

            var result = sqlDao.ExecuteQueryProcedure(sqlOperation);
            if (result.Count == 0)
                return default!;

            var row = result[0];

            var product = new Product
            {
                Id = Convert.ToInt32(row["Id"]),
                Name = row["Name"].ToString(),
                Category = row["Category"].ToString(),
                Price = Convert.ToDouble(row["Price"]),
                Stock = Convert.ToInt32(row["Stock"]),
                ProductCode = row["ProductCode"].ToString()
            };

            if (product is T castedProduct)
                return castedProduct;

            throw new InvalidCastException($"Error de conversión de tipo en RetrieveById. {typeof(T)} no es compatible con Product.");
        }

        
        public override List<T> RetrieveAll<T>()
        {
            var sqlOperation = new SqlOperation { ProcedureName = "RET_ALL_PRODUCTS_PR" };
            var result = sqlDao.ExecuteQueryProcedure(sqlOperation);

            var productList = new List<T>();
            foreach (var row in result)
            {
                var product = new Product
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    Category = row["Category"].ToString(),
                    Price = Convert.ToDouble(row["Price"]),
                    Stock = Convert.ToInt32(row["Stock"]),
                    ProductCode = row["ProductCode"].ToString()
                };

                if (product is T castedProduct)
                    productList.Add(castedProduct);
                else
                    throw new InvalidCastException($"Error de conversión de tipo en RetrieveAll. {typeof(T)} no es compatible con Product.");
            }

            return productList;
        }
    }
}
