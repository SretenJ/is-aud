using EshopApplication.Domain.DomainModels.Identity;
using EshopApplication.Domain.DomainModels.Relations;
using System;
using System.Collections.Generic;

namespace EshopApplication.Domain.DomainModels.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public virtual ICollection<ProductInShoppingCart> ProductInShoppingCarts{ get; set; }
        public EshopApplicationUser Owner { get; set; }

    }

}
