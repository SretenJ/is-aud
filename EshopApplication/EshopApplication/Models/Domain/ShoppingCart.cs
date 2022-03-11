using EshopApplication.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopApplication.Models.Domain
{
    public class ShoppingCart
    {

        public Guid Id { get; set; }
        public string OwnerId { get; set; }
        public virtual ICollection<ProductInShoppingCart> ProductInShoppingCarts{ get; set; }
        public EshopApplicationUser Owner { get; set; }

    }

}
