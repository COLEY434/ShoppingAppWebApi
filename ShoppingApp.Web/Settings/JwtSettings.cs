using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApp.Web.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Key { get; set; }
        public int ExpiryTimeInMins { get; set; }
    }
}
