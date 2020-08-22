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
		/// Displays a view that lists a page of products
		/// </summary>
		public async Task<IActionResult> Index(int? id)
		{
			int pageNum = id ?? 1;
			const int PageSize = 3;
			ViewData["CurrentPage"] = pageNum;

			int numProducts = await _context.Products.CountAsync();

			int totalPages = (int)Math.Ceiling((double)numProducts / PageSize);

			ViewData["MaxPage"] = totalPages;

			// Get all products from database
			List<Product> products = await _context
										.Products
										.OrderBy(p => p.Title)
										.Skip(PageSize * (pageNum - 1))  // Skip must be before Take()
										.Take(PageSize)
										.ToListAsync();

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

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			Product p = await _context
							.Products
							.Where(p => p.ProductId == id)
							.SingleAsync();
			return View(p);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Product p)
		{
			if (ModelState.IsValid)
			{
				_context.Entry(p).State = EntityState.Modified;
				await _context.SaveChangesAsync();

				ViewBag.Message = "Product updated successfully";
			}

			return View(p);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			Product p = await _context
							.Products
							.Where(p => p.ProductId == id)
							.SingleAsync();

			return View(p);
		}

		[HttpPost]
		[ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			Product p = await _context
							.Products
							.Where(p => p.ProductId == id)
							.SingleAsync();

			_context.Entry(p).State = EntityState.Deleted;
			await _context.SaveChangesAsync();

			TempData["Message"] = $"{p.Title} was deleted successfully";

			return RedirectToAction("Index");
		}
	}
}
