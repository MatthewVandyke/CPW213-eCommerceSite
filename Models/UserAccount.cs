using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
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

	public class RegisterViewModel
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		[StringLength(200)]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[Compare(nameof(Email))]
		[Display(Name = "Confirm Email")]
		public string ConfirmEmail { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[StringLength(120, MinimumLength = 6, ErrorMessage = "Password must be between {2} an {1} characters")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		[Display(Name = "Confirm Password")]
		public string ConfirmPassword { get; set; }

		[DataType(DataType.Date)] // Time ignored
		public DateTime? DateOfBirth { get; set; }
	}
}
