using EshopApplication.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopApplication.Models.DTO
{
    public class ShoppingCartDTO
    {
        public List<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public double TotalPrice { get; set; }
    }
}
