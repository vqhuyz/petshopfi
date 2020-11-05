using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Models
{
    public class Target
    {
		public Target(string date, decimal? money)
		{
			this.Date = date;
			this.Money = money;
		}

		public string Date = "";

		public Nullable<decimal> Money = null;
	}
}