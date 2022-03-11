﻿using EshopApplication.Models.Domain;
using EshopApplication.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EshopApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<EshopApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ShoppingCart>()
                .Property(z => z.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<ProductInShoppingCart>()
                .HasKey(z => new { z.ProductId, z.ShoppingCartId });

            builder.Entity<ProductInShoppingCart>()
                .HasOne(z => z.Product)
                .WithMany(t => t.ProductInShoppingCarts)
                .HasForeignKey(z => z.ProductId);

            builder.Entity<ProductInShoppingCart>()
               .HasOne(z => z.ShoppingCart)
               .WithMany(z => z.ProductInShoppingCarts)
               .HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<ShoppingCart>()
                .HasOne<EshopApplicationUser>(z => z.Owner)
                .WithOne(z => z.UserCart)
                .HasForeignKey <ShoppingCart>(z => z.OwnerId);

            builder.Entity<ProductInOrder>()
                .HasKey(pisc => new { pisc.ProductId, pisc.OrderId });

            builder.Entity<ProductInOrder>()
                .HasOne(z => z.SelectedProduct)
                .WithMany(t => t.Orders)
                .HasForeignKey(z => z.ProductId);

            builder.Entity<ProductInOrder>()
                .HasOne(z => z.UserOrder)
                .WithMany(t => t.Products)
                .HasForeignKey(z => z.OrderId);
        }
    }
}