using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassManWebCore.Models
{
    public class PasswordData
    {
		public int ID { get; set; }
		public string Title { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Webpage { get; set; }
		public string Note { get; set; }

		public string DBUser { get; set; }
	}
}
