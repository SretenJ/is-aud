using EshopApplication.Domain.DomainModels.Identity;
using EshopApplication.Domain.DomainModels.Relations;
using System;
using System.Collections.Generic;

namespace EshopApplication.Domain.DomainModels.Domain
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public EshopApplicationUser User { get; set; }
        public virtual ICollection<ProductInOrder> Products { get; set; }
    }   
}
