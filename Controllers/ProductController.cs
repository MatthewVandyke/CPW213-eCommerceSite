using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers
{
	public class ProductController : Controller
	{
		private readonly ProductContext _context;
		public ProductController(ProductContext context)
		{
			_context = context;
		}

		/// <summary>
		/// Displays a view that lists all products
		/// </summary>
		public async Task<IActionResult> Index()
		{
			// Get all products from database
			List<Product> products = await _context.Products.ToListAsync();

			// Send list of products to view to be displayed
			return View(products);
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Add(Product p)
		{
			if (ModelState.IsValid)
			{
				// Add to DB
				_context.Products.Add(p);
				await _context.SaveChangesAsync();

				TempData["Message"] = $"{p.Title} was added successfully";

				// redirect to catalog page
				return RedirectToAction("Index");
			}

			return View();
		}
	}
}
