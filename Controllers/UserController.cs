using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers
{
	public class UserController : Controller
	{
		private readonly ProductContext _context;

		public UserController(ProductContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel reg)
		{
			if (ModelState.IsValid)
			{
				// map data to user account instance
				UserAccount acc = new UserAccount()
				{
					DateOfBirth = reg.DateOfBirth,
					Email = reg.Email,
					Password = reg.Password,
					Username = reg.Username
				};

				// add to database
				_context.UserAccounts.Add(acc);
				await _context.SaveChangesAsync();

				// redirect to home page
				return RedirectToAction("Index", "Home");
			}
			return View(reg);
		}

		public IActionResult Login()
		{
			if (HttpContext.Session.GetInt32("UserId").HasValue)
			{
				return RedirectToAction("Index", "Home");
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			UserAccount account = await _context.UserAccounts
										.Where(u => (u.Username == model.UsernameOrEmail
												|| u.Email == model.UsernameOrEmail)
												&& u.Password == model.Password)
										.SingleOrDefaultAsync();
			if(account == null)
			{
				ModelState.AddModelError(string.Empty, "Credentials were not found");

				return View(model);
			}

			HttpContext.Session.SetInt32("UserId", account.UserId);

			return RedirectToAction("Index", "Home");
		}
	}
}
