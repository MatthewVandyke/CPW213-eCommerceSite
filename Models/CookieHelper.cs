using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace eCommerceSite.Models
{
	public static class CookieHelper
	{
		public const string CartCookie = "CartCookie";

		/// <summary>
		/// Returns the current list of cart products. If cart is empty an
		/// empty list will be returned
		/// </summary>
		/// <param name="httpContext"></param>
		public static List<Product> GetCartProducts(HttpContext httpContext)
		{
			string existingItems = httpContext.Request.Cookies[CartCookie];

			List<Product> cartProducts = new List<Product>();
			if(existingItems != null)
			{
				cartProducts = JsonConvert.DeserializeObject<List<Product>>(existingItems);
			}

			return cartProducts;
		}

		public static void AddProductToCart(HttpContext httpContext, Product p)
		{
			List<Product> cartProducts = GetCartProducts(httpContext);
			cartProducts.Add(p);

			string data = JsonConvert.SerializeObject(cartProducts);
			CookieOptions options = new CookieOptions()
			{
				Expires = DateTime.Now.AddYears(1),
				Secure = true,
				IsEssential = true
			};

			httpContext.Response.Cookies.Append(CartCookie, data, options);
		}

		public static int GetNumberOfCartProducts(HttpContext httpContext)
		{
			return GetCartProducts(httpContext).Count;
		}
	}
}
