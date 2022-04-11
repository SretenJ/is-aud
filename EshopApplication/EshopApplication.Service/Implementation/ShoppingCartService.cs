using EshopApplication.Domain.DomainModels.Domain;
using EshopApplication.Domain.DomainModels.DTO;
using EshopApplication.Domain.DomainModels.Relations;
using EshopApplication.Repository.Interface;
using EshopApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EshopApplication.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepositorty;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Order> _orderRepositorty;
        private readonly IRepository<ProductInOrder> _productInOrderRepositorty;

        public ShoppingCartService(IUserRepository userRepository, IRepository<ShoppingCart> shoppingCartRepositorty, IRepository<Order> orderRepositorty, IRepository<ProductInOrder> productInOrderRepositorty)
        {
            _shoppingCartRepositorty = shoppingCartRepositorty;
            _userRepository = userRepository;
            _orderRepositorty = orderRepositorty;
            _productInOrderRepositorty = productInOrderRepositorty;
        }

        public bool deleteProductFromShoppingCart(string userId, Guid id)
        {
            var user = _userRepository.Get(userId);
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                var userShoppingCart = user.UserCart;
                var itemToDelete = userShoppingCart.ProductInShoppingCarts.Where(z => z.ProductId.Equals(id)).FirstOrDefault();

                userShoppingCart.ProductInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepositorty.Update(userShoppingCart);
                return true;
            }
            else
            {
                return false;
            }
        }

        public ShoppingCartDTO getShoppingCartInfo(string userId)
        {
            var user = this._userRepository.Get(userId);

            var userShoppingCart = user.UserCart;

            var AllProducts = userShoppingCart.ProductInShoppingCarts.ToList();

            var allProductPrice = AllProducts.Select(z => new
            {
                ProductPrice = z.Product.ProductPrice,
                Quanitity = z.Quantity
            }).ToList();
            var totalPrice = 0;


            foreach (var item in allProductPrice)
            {
                totalPrice += item.Quanitity * item.ProductPrice;
            }


            ShoppingCartDTO scDto = new ShoppingCartDTO
            {
                ProductInShoppingCarts = AllProducts,
                TotalPrice = totalPrice
            };
            return scDto;
        }

        public bool orderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

               

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepositorty.Insert(order);

                List<ProductInOrder> productInOrders = new List<ProductInOrder>();

                var result = userShoppingCart.ProductInShoppingCarts.Select(z => new ProductInOrder
                {
                    Id = Guid.NewGuid(),
                    ProductId = z.Product.Id,
                    SelectedProduct = z.Product,
                    OrderId = order.Id,
                    UserOrder = order
                }).ToList();

                productInOrders.AddRange(result);              
                foreach (var item in productInOrders)
                {
                    this._productInOrderRepositorty.Insert(item);
                }

                loggedInUser.UserCart.ProductInShoppingCarts.Clear();

                this._userRepository.Update(loggedInUser);

                return true;
            }
            return false;
        }
    }
}
