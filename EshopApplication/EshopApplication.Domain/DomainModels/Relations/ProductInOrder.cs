using EshopApplication.Domain.DomainModels.Domain;
using System;

namespace EshopApplication.Domain.DomainModels.Relations
{
    public class ProductInOrder :BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product SelectedProduct { get; set; }
        public Guid OrderId { get; set; }
        public Order UserOrder { get; set; }

    }
}
