using eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Data
{
	public static class ProductDb
	{
		/// <summary>
		/// Returns the total count of products
		/// </summary>
		/// <param name="_context">Database context to use</param>
		public static async Task<int> GetTotalProductsAsync(ProductContext _context)
		{
			return await _context.Products.CountAsync();
		}
		/// <summary>
		/// Get a page worth of products
		/// </summary>
		/// <param name="_context">Database context to use</param>
		/// <param name="pageSize">Number of products per page</param>
		/// <param name="pageNum">Page of products to return</param>
		/// <returns></returns>
		public static async Task<List<Product>> GetProductsAsync(ProductContext _context, int pageSize, int pageNum)
		{
			return await _context
					.Products
					.OrderBy(p => p.Title)
					.Skip(pageSize * (pageNum - 1))  // Skip must be before Take()
					.Take(pageSize)
					.ToListAsync();
		}

		public static async Task<Product> AddProductAsync(ProductContext _context, Product p)
		{
			_context.Products.Add(p);
			await _context.SaveChangesAsync();
			return p;
		}

		public static async Task<Product> GetProductAsync(ProductContext _context, int id)
		{
			return await _context
							.Products
							.Where(p => p.ProductId == id)
							.SingleAsync();
		}

		public static async Task<Product> UpdateProductAsync(ProductContext _context, Product p)
		{
			_context.Products.Update(p);
			await _context.SaveChangesAsync();
			return p;
		}

		public static async Task DeleteProductAsync(ProductContext _context, Product p)
		{
			_context.Products.Remove(p);
			await _context.SaveChangesAsync();
		}
	}
}
