using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop2.Models
{
    public class HomeToCart
    {
        public IEnumerable<BookDetail> BookDetail { get; set; }

        public IEnumerable<CartDetail> CartDetail { get; set; }
    }
}