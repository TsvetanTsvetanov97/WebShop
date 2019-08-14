using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data.Models
{
    public class ShopUser : IdentityUser
    {
        public ShopUser()
        {

        }
        
        public string FullName { get; set; }
    }
}
