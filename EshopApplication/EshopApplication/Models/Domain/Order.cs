using EshopApplication.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopApplication.Models.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public EshopApplicationUser User { get; set; }
        public virtual ICollection<ProductInOrder> Products { get; set; }
    }   
}
