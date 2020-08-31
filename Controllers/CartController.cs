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
			const string CartCookie = "CartCookie";

			// Get prod from database
			Product p =
				await ProductDb.GetProductAsync(_context, id);

			// Get cart items
			string existingItems = HttpContext.Request.Cookies[CartCookie];

			List<Product> cartProducts = new List<Product>();
			if(existingItems != null)
			{
				cartProducts = JsonConvert.DeserializeObject<List<Product>>(existingItems);
			}

			// Add current product to existing cart
			cartProducts.Add(p);

			// Add products to cart cookie
			string data = JsonConvert.SerializeObject(cartProducts);

			CookieOptions options = new CookieOptions()
			{
				Expires = DateTime.Now.AddYears(1),
				Secure = true,
				IsEssential = true
			};

			HttpContext.Response.Cookies.Append(CartCookie, data, options);

			// Redirect
			return RedirectToAction("Index", "Product");
		}

		public IActionResult Summary()
		{
			string cookieData = HttpContext.Request.Cookies["CartCookie"];

			List<Product> cartProducts = JsonConvert.DeserializeObject<List<Product>>(cookieData);
			return View(cartProducts);
		}
	}
}
