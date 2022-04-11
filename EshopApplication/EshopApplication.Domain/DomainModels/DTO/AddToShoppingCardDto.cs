using EshopApplication.Domain.DomainModels.Domain;
using System;

namespace EshopApplication.Domain.DomainModels.DTO
{
    public class AddToShoppingCardDto
    {
        public Product SelectedProduct { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
