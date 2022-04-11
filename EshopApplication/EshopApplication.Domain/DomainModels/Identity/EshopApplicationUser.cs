

using EshopApplication.Domain.DomainModels.Domain;
using Microsoft.AspNetCore.Identity;

namespace EshopApplication.Domain.DomainModels.Identity
{
    public class EshopApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public virtual ShoppingCart UserCart { get; set; }

    }
}
