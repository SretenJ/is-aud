using EshopApplication.Domain.DomainModels.Domain;
using EshopApplication.Domain.DomainModels.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EshopApplication.Service.Interface
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        Product GetDetailsForProduct(Guid? id);
        void CreateNewProduct(Product p);
        void UpdateExistingProduct(Product p);
        AddToShoppingCardDto GetShoppingCardInfo(Guid? id);
        void DeleteProduct(Guid id);
        bool AddToShoppingCart(AddToShoppingCardDto item, string userID);
    }
}
