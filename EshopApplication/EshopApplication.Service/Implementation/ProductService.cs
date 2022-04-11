using EshopApplication.Domain.DomainModels.Domain;
using EshopApplication.Domain.DomainModels.DTO;
using EshopApplication.Domain.DomainModels.Relations;
using EshopApplication.Repository.Interface;
using EshopApplication.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EshopApplication.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IRepository<Product> productRepository, IUserRepository userRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository, ILogger<ProductService> logger)
        {
            _userRepository = userRepository;
            _productRepository = productRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
            _logger = logger;
        }

        public bool AddToShoppingCart(AddToShoppingCardDto item, string userID)
        {
            var user = this._userRepository.Get(userID);
            var userShoppingCard = user.UserCart;
            if (item.ProductId != null && userShoppingCard != null)
            {
                var product = this.GetDetailsForProduct(item.ProductId);

                if (product != null)
                {
                    ProductInShoppingCart itemToAdd = new ProductInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        Product = product,
                        ProductId = product.Id,
                        ShoppingCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };
                    this._productInShoppingCartRepository.Insert(itemToAdd);
                    _logger.LogInformation("Product was added to cart");
                    return true;
                }
                return false;
            }
            _logger.LogInformation("Something was wrong, ProductId or UserShoppingCart may be unavailbale");
            return false;
        }

        public void CreateNewProduct(Product p)
        {
            this._productRepository.Insert(p);
        }

        public void DeleteProduct(Guid id)
        {
            var product = this.GetDetailsForProduct(id);
            this._productRepository.Delete(product);
        }

        public List<Product> GetAllProducts()
        {
            _logger.LogInformation("GetAllProducts");
            return this._productRepository.GetAll().ToList();
        }

        public Product GetDetailsForProduct(Guid? id)
        {
            return this._productRepository.Get(id);
        }

        public AddToShoppingCardDto GetShoppingCardInfo(Guid? id)
        {
            var product = this.GetDetailsForProduct(id);
            AddToShoppingCardDto model = new AddToShoppingCardDto
            {
                SelectedProduct = product,
                ProductId = product.Id,
                Quantity = 1
            };
            return model;
        }

        public void UpdateExistingProduct(Product p)
        {
            this._productRepository.Update(p);
        }
    }
}
