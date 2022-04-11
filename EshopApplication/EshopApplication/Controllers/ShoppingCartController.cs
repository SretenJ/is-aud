

using EshopApplication.Domain.DomainModels.Identity;

using EshopApplication.Service.Interface;
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
        private readonly UserManager<EshopApplicationUser> _usermanager;
        private readonly IShoppingCartService _shoppingCartService;
        public ShoppingCartController(IShoppingCartService shoppingCartService, UserManager<EshopApplicationUser> userManager)
        {
            _usermanager = userManager;
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(this._shoppingCartService.getShoppingCartInfo(userId));
        }

        public IActionResult DeleteProductFromShoppingCart(Guid productId)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            this._shoppingCartService.deleteProductFromShoppingCart(userId, productId);
            return RedirectToAction("Index", "ShoppingCart");
        }
        public IActionResult OrderNow()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this._shoppingCartService.orderNow(userId);
            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
