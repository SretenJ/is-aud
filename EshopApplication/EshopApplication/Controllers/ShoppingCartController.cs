using EshopApplication.Data;
using EshopApplication.Models.Domain;
using EshopApplication.Models.DTO;
using EshopApplication.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EshopApplication.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<EshopApplicationUser> _usermanager;

        public ShoppingCartController(ApplicationDbContext context, UserManager<EshopApplicationUser> userManager)
        {
            _usermanager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInUser = await _context.Users.Where(z => z.Id == userId)
                .Include(z=>z.UserCart)
                .Include(z=>z.UserCart.ProductInShoppingCarts)
                .Include("UserCart.ProductInShoppingCarts.Product")
                .FirstOrDefaultAsync();

            var userShoppingCart = loggedInUser.UserCart;
            var productPrice = userShoppingCart.ProductInShoppingCarts
                .Select(z => new
                {
                    ProductPrice = z.Product.ProductPrice,
                    Quantity = z.Quantity
                }).ToList();
            double totalPrice = 0;
            foreach (var item in productPrice)
            {
                totalPrice += item.ProductPrice * item.Quantity;
            }
            ShoppingCartDTO shoppingCartDTOItem = new ShoppingCartDTO
            {
                ProductInShoppingCarts = userShoppingCart.ProductInShoppingCarts.ToList(),
                TotalPrice = totalPrice
            };
           /* var allProducts = userShoppingCart.ProductInShoppingCarts.Select(
                z => z.Product).ToList();*/
            return View(shoppingCartDTOItem);
        }
        public async Task<IActionResult> DeleteProductFromShoppingCart(Guid productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var loggedInUser = await _context.Users
                .Where(z => z.Id == userId)
                .Include(z => z.UserCart)
                .Include(z => z.UserCart.ProductInShoppingCarts)
                .Include("UserCart.ProductInShoppingCarts.Product")
                .FirstOrDefaultAsync();

            var userShoppingCart=loggedInUser.UserCart;

            var productToDelete = userShoppingCart.ProductInShoppingCarts
                .Where(z => z.ProductId == productId)
                .FirstOrDefault();
            // TODO: Pomeshani se productId i shopping cart AddToshoopingcart post metod vo productcontrolerot 
            userShoppingCart.ProductInShoppingCarts.Remove(productToDelete);

            _context.Update(loggedInUser.UserCart);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "ShoppingCart");
        }
        public async Task<IActionResult> OrderNow()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var loggedInUser = await _context.Users
               .Where(z => z.Id == userId)
               .Include(z => z.UserCart)
               .Include(z => z.UserCart.ProductInShoppingCarts)
               .Include("UserCart.ProductInShoppingCarts.Product")
               .FirstOrDefaultAsync();

            var userShoppingCart = loggedInUser.UserCart;
            Order OrderItem = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                User = loggedInUser
            };

            _context.Add(OrderItem);
            await _context.SaveChangesAsync();

            List<ProductInOrder> productInOrders = new List<ProductInOrder>();

            productInOrders = userShoppingCart.ProductInShoppingCarts
                .Select(z => new ProductInOrder
                {
                    OrderId = OrderItem.Id,
                    ProductId = z.Product.Id,
                    SelectedProduct = z.Product,
                    UserOrder = OrderItem
                }).ToList();

            foreach (var item in productInOrders)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
            }

            loggedInUser.UserCart.ProductInShoppingCarts.Clear();
            _context.Update(loggedInUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
