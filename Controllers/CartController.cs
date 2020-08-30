using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceSite.Controllers
{
	public class CartController : Controller
	{
		public IActionResult Add(int id) // Id of the product to add
		{
			// Get prod from database
			// Add prodcut to cart cookie

			// Redirect
			return View();
		}

		public IActionResult Summary()
		{
			// Display all products in cart cookie
			return View();
		}
	}
}
