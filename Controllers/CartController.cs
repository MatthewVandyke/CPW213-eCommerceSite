using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace eCommerceSite.Controllers
{
	public class CartController : Controller
	{
		private readonly ProductContext _context;

		public CartController(ProductContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Adds a product to the shopping cart
		/// </summary>
		/// <param name="id">The id of the product to add</param>
		public async Task<IActionResult> Add(int id)
		{
			// Get prod from database
			Product p =
				await ProductDb.GetProductAsync(_context, id);

			CookieHelper.AddProductToCart(HttpContext, p);

			// Redirect
			return RedirectToAction("Index", "Product");
		}

		public IActionResult Summary()
		{
			List<Product> cartProducts = CookieHelper.GetCartProducts(HttpContext);
			return View(cartProducts);
		}
	}
}
