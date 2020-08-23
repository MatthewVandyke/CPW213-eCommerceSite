﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceSite.Models
{
	public class UserAccount
	{
		[Key]
		public int UserId { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }

		public DateTime? DateOfBirth { get; set; }
	}
}
