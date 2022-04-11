using EshopApplication.Domain.DomainModels.Domain;
using System;

namespace EshopApplication.Domain.DomainModels.Relations
{
    public class ProductInShoppingCart : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int Quantity { get; set; }

    }
}
