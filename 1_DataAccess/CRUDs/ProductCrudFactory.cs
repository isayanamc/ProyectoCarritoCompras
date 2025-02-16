using DTO;
using System;
using DataAccess.DAOs;
using System.Collections.Generic;


namespace DataAccess.CRUDs
{
    public class ProductCrudFactory : CrudFactory
    {

        private static readonly SqlDao _sqlDao = SqlDao.GetInstance();

        public override void Create(BaseDTO dto)
        {
            var product = dto as Product;
            var sqlOperation = new SqlOperation() { ProcedureName = "CRE_PRODUCT_PR" };

            sqlOperation.AddStringParameter("P_PRODUCT_CODE", product.ProductCode);
            sqlOperation.AddStringParameter("P_NAME", product.Name);
            sqlOperation.AddStringParameter("P_CATEGORY", product.Category);
            sqlOperation.AddDoubleParam("P_PRICE", product.Price);
            sqlOperation.AddIntParameter("P_STOCK", product.Stock);

            _sqlDao.ExecuteProcedure(sqlOperation);
        }

        public override void Update(BaseDTO dto)
        {
            var product = dto as Product;

            Console.Write("Nuevo código de producto (o el mismo si no cambia): ");
            string newProductCode = Console.ReadLine();

            var sqlOperation = new SqlOperation() { ProcedureName = "UPD_PRODUCT_PR" };

            sqlOperation.AddStringParameter("P_OLD_PRODUCT_CODE", product.ProductCode);
            sqlOperation.AddStringParameter("P_NEW_PRODUCT_CODE", newProductCode);
            sqlOperation.AddStringParameter("P_NAME", product.Name);
            sqlOperation.AddStringParameter("P_CATEGORY", product.Category);
            sqlOperation.AddDoubleParam("P_PRICE", product.Price);
            sqlOperation.AddIntParameter("P_STOCK", product.Stock);

            _sqlDao.ExecuteProcedure(sqlOperation);
            product.ProductCode = newProductCode;
        }

        public override void Delete(BaseDTO dto)
        {
            var product = dto as Product;
            var sqlOperation = new SqlOperation() { ProcedureName = "DEL_PRODUCT_PR" };

            sqlOperation.AddStringParameter("P_PRODUCT_CODE", product.ProductCode);

            _sqlDao.ExecuteProcedure(sqlOperation);
        }




        public override T Retrieve<T>(BaseDTO dto) => throw new NotImplementedException();
        public override T RetrieveById<T>(int id) => throw new NotImplementedException();
        public override List<T> RetrieveAll<T>() => throw new NotImplementedException();
    }
}
