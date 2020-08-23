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
		public string Email { get; set; }

		[Required]
		[DataType(DataType.EmailAddress)]
		[Compare(nameof(Email))]
		public string ConfirmEmail { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string ConfirmPassword { get; set; }

		[DataType(DataType.Date)] // Time ignored
		public DateTime? DateOfBirth { get; set; }
	}
}
