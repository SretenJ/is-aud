using EshopApplication.Domain.DomainModels.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EshopApplication.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<EshopApplicationUser> GetAll();
        EshopApplicationUser Get(string id);
        void Insert(EshopApplicationUser entity);
        void Update(EshopApplicationUser entity);
        void Delete(EshopApplicationUser entity);
    }
}
