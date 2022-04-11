using EshopApplication.Domain.DomainModels.Relations;
using System.Collections.Generic;
namespace EshopApplication.Domain.DomainModels.DTO
{
    public class ShoppingCartDTO
    {
        public List<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public double TotalPrice { get; set; }
    }
}
