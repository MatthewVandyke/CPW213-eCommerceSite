using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models
{
	/// <summary>
	/// A product to sell
	/// </summary>
	public class Product
	{
		[Key] // Make Primary Key
		public int ProductId { get; set; }

		/// <summary>
		/// Consumer facing name of product
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// The retail price of the product
		/// </summary>
		public double Price { get; set; }

		/// <summary>
		/// Category product falls under
		/// </summary>
		public string Category { get; set; }
	}
}
