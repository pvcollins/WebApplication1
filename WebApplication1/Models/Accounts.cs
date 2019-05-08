using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Accounts
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int AccountId { get; set; }
        public string ScreenName { get; set; }
    }
}