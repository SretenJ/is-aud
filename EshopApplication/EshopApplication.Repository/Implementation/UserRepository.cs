using EshopApplication.Domain.DomainModels.Identity;
using EshopApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EshopApplication.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<EshopApplicationUser> entities;
        public UserRepository(ApplicationDbContext _context)
        {
            this.context = _context;
            entities = context.Set<EshopApplicationUser>();
        }
        public IEnumerable<EshopApplicationUser> GetAll()
        {
            return entities.AsEnumerable();
        }
        public void Update(EshopApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
        public void Delete(EshopApplicationUser entity)
        {
            entities.Remove(entity);
            context.SaveChanges();
        }
        public EshopApplicationUser Get(string id)
        {
            return entities
                .Include(z => z.UserCart)
                .Include("UserCart.ProductInShoppingCarts")
                .Include("UserCart.ProductInShoppingCarts.Product")
                .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(EshopApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }
    }
}
